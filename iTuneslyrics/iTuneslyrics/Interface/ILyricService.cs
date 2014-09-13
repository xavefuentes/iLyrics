namespace iTuneslyrics.Interface {
    public interface ILyricService {
        SearchResult Exists(string artist, string song);
        SearchResult GetLyrics(string artist, string song, out string lyrics);

    }
}