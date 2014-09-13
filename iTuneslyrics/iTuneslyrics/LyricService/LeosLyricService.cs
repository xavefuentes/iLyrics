using System;
using System.Web;
using System.Xml;
using iTuneslyrics.Interface;

namespace iTuneslyrics.LyricService {
    public class LeosLyricService : ILyricService {
        private const string LYRIC_PATH = "/leoslyrics/lyric/text";
        private const string LYRIC_SEARCH_RESULT_PATH = "/leoslyrics/searchResults/result";
        private const string SONG_ID = "hid";
        private const string LYRIC_URL_FORMAT = "{0}hid={1}";
        private const string NEW_LINE_STRING = "\r\r\n";
        private const string SEARCH_URL_FORMAT = "{0}artist={1}&songtitle={2}";
        private readonly string _authId;
        private readonly string _searchApi;
        private readonly string _lyricsApi;

        public LeosLyricService(string authId) {
            _authId = authId;
            _searchApi =  String.Format("http://api.leoslyrics.com/api_search.php?auth={0}&", _authId);
            _lyricsApi = String.Format("http://api.leoslyrics.com/api_lyrics.php?auth={0}&", _authId);
        }

        public SearchResult Exists(string artist, string song) {
            var results = Search(artist, song);
            var resultNodes = results.SelectNodes(LYRIC_SEARCH_RESULT_PATH);

            if (resultNodes == null || resultNodes.Count == 0) 
                return SearchResult.NotFound;
            
            if (resultNodes.Count > 1) return  SearchResult.MultipleFound;

            return SearchResult.Found;
        }

        public SearchResult GetLyrics(string artist, string song, out string lyrics) {

            lyrics = String.Empty;

            var searchResult = Search(artist, song);
            var resultNodes = searchResult.SelectNodes(LYRIC_SEARCH_RESULT_PATH);

            if (resultNodes == null || resultNodes.Count == 0) return SearchResult.NotFound;
            if (resultNodes.Count > 1) return SearchResult.MultipleFound;

            var songId = searchResult.SelectSingleNode(LYRIC_SEARCH_RESULT_PATH).Attributes[SONG_ID].Value;

            var lyricUrl = String.Format(LYRIC_URL_FORMAT, _lyricsApi, HttpUtility.UrlEncode(songId));
            var lyricResults = new XmlDocument();
            lyricResults.Load(lyricUrl);

            var lyricNode = lyricResults.SelectSingleNode(LYRIC_PATH);

            if (lyricNode == null) return SearchResult.Error;

            lyrics = lyricNode.InnerText.Replace(NEW_LINE_STRING, Environment.NewLine);

            return SearchResult.Found;
        }

        private XmlDocument Search(string artist, string song) {
            var doc = new XmlDocument();
            string searchUrl = String.Format(SEARCH_URL_FORMAT, _searchApi,  HttpUtility.UrlEncode(artist),HttpUtility.UrlEncode(song));
            doc.Load(new XmlTextReader(searchUrl));

            return doc;
        }

    }
}