namespace sciendo.preferences.service.Logic
{
    public interface ILastFmGetter
    {
        string[] GetSimilarArtists(string artist);
    }
}
