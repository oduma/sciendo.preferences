namespace sciendo.preferences.service.Logic
{
    public class LastFmGetter : ILastFmGetter
    {
        public string[] GetSimilarArtists(string artist)
        {
            return new string[] { "abc", "def", "feg", "ghi", "hjk" }; 
        }
    }
}
