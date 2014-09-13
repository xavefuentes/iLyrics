using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using iTuneslyrics.com.wikia.lyrics;
using iTuneslyrics.Interface;


namespace iTuneslyrics.LyricService {
    public class LyricWiki : ILyricService {

        private const string ISO_8859 = "ISO-8859-1";
        private static readonly Regex NewLineEx = new Regex("<br */?>", RegexOptions.Compiled);
        private const string NEW_LINE = "\r\n";
        private const string HTML_TAG_PATTERN = "<.*?>";
        private static readonly Regex LyricBoxEx = new Regex(@"<div class='lyricbox'>.*?</script>(.*?)<!--", RegexOptions.Compiled);
        //private static readonly Regex LyricBoxEx = new Regex(@"<div class='lyricbox'>.*?</div>(.*?)<!--", RegexOptions.Compiled);

        private readonly com.wikia.lyrics.LyricWiki _lyricsWiki = new com.wikia.lyrics.LyricWiki();

        public SearchResult Exists(string artist, string song) {
            if (_lyricsWiki.checkSongExists(artist, song))
                return SearchResult.Found;
            
            return SearchResult.NotFound;
        }

        public SearchResult GetLyrics(string artist, string song, out string lyrics) {
            lyrics = string.Empty;

            LyricsResult result = _lyricsWiki.getSong(artist, song);
            Encoding iso8859 = Encoding.GetEncoding(ISO_8859); // thanks to davidreis

            string url = Encoding.UTF8.GetString(iso8859.GetBytes(result.url));

            if (String.IsNullOrEmpty(result.lyrics)) return SearchResult.NotFound;

            if (!String.IsNullOrEmpty(url)) {
                lyrics = RetrieveSong(url);
                if (String.IsNullOrEmpty(lyrics)) return SearchResult.NotFound;
            }
            else {
                lyrics = result.lyrics;
                lyrics = HttpUtility.HtmlDecode(lyrics);
            }

            return SearchResult.Found;

        }

        private static string RetrieveSong(string url) {
            string lyrics = string.Empty;

            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            string html;

            var response = webRequest.GetResponse();
            Encoding iso8859 = Encoding.GetEncoding(ISO_8859);

            using (var reader = new StreamReader(response.GetResponseStream(), iso8859)) {
                html = reader.ReadToEnd();
            }

            Match m = LyricBoxEx.Match(html);

            if (!m.Success) return string.Empty;

            lyrics = m.Groups[1].Value.Trim();

            lyrics = NewLineEx.Replace(lyrics, NEW_LINE);
            lyrics = Regex.Replace(lyrics, HTML_TAG_PATTERN, string.Empty);
            lyrics = HttpUtility.HtmlDecode(lyrics);

            //hard-coded sleep of 2 seconds to not spam the lyrics wiki :(
            System.Threading.Thread.Sleep(2000);

            return lyrics;
        }
    
    }
}