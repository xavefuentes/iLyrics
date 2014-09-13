using System;
using System.Windows.Forms;
using iTunesLib;
using iTuneslyrics.Interface;

namespace iTuneslyrics {
    class LyricsUpdater {
        private const string MSG_PROCESSING = "Processing...";
        private const string MSG_SKIPPED = "Skipped";
        private const string STR_TRUE = "true";
        private const string STR_FALSE = "false";
        private readonly IITTrackCollection _selectedTracks;
        private readonly FrmResult _form;
        private readonly Boolean _overwrite;
        private readonly ILyricService _lyricService;

        public LyricsUpdater(IITTrackCollection selectedTracks, ILyricService lyricService, Boolean overwrite, FrmResult form) {
            _selectedTracks = selectedTracks;
            _lyricService = lyricService;
            _overwrite = overwrite;
            _form = form;
        }

        public void UpdateLyrics() {
            for (int i = 1; i <= _selectedTracks.Count; i++) {
                var currentTrack = (IITFileOrCDTrack)_selectedTracks[i];

                String artist = currentTrack.Artist;
                String song = currentTrack.Name;

                if (!string.IsNullOrEmpty(currentTrack.Location) &&
                    !string.IsNullOrEmpty(artist) && !string.IsNullOrEmpty(song)) {
                    String[] row = { song, artist, MSG_PROCESSING };
                    var index = (int)_form.Invoke(_form.RowAdded, new Object[] { row });

                    if (currentTrack.Lyrics != null && !_overwrite) {
                        _form.Invoke(_form.RowUpdated, new Object[] { index, MSG_SKIPPED });
                        continue;
                    }

                    try {

                        var result = FetchLyrics(currentTrack, artist, song, index);

                        if (result == SearchResult.NotFound) {
                            //remove parenthesis (and any thing inside) and retry;
                            var parenStartIndex = song.IndexOf('(');
                            var parenEndIndex = song.IndexOf(')');

                            if (parenStartIndex > -1 && parenEndIndex > -1) {
                                song = song.Remove(parenStartIndex, parenEndIndex - parenStartIndex).Trim();

                                FetchLyrics(currentTrack, artist, song, index);
                            }
                        }
                        
                        if ( result == SearchResult.Found) {
                            _form.Invoke(_form.RowUpdated, new Object[] {index, STR_TRUE});
                        }else {
                            _form.Invoke(_form.RowUpdated, new Object[] {index, STR_FALSE});
                        }
                    }
                    catch (Exception e) {
                        _form.Invoke(_form.RowUpdated, new Object[] { index, e.Message });
                    }
                }
            }
            MessageBox.Show("Completed");
        }

        private SearchResult FetchLyrics(IITFileOrCDTrack currentTrack, string artist, string song, int index) {

            if (_lyricService.Exists(artist, song) == SearchResult.NotFound)
                return SearchResult.NotFound;

            string lyrics;
            var result = _lyricService.GetLyrics(artist, song, out lyrics);

            if (result == SearchResult.Found) {
                currentTrack.Lyrics = lyrics;
            }

            return result;
        }
    }
}