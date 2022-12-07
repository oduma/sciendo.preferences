using Sciendo.Last.Fm;

namespace sciendo.preferences.service.Logic
{
    public class LastFmGetter : ILastFmGetter
    {
        private readonly ILogger<ILastFmGetter> logger;
        private readonly IContentProvider<Temperatures> contentProvider;
        private const string methodName = "artist.getsimilar";
        private const string userName = "scentmaster";
        private const string artistParameterName = "artist";

        public LastFmGetter(ILogger<ILastFmGetter> logger, IContentProvider<Temperatures> contentProvider)
        {
            this.logger = logger;
            this.contentProvider = contentProvider;
        }
        public string[] GetSimilarArtists(string artist)
        {
            var temperatures = contentProvider.GetContent(methodName, userName, 1, artistParameterName + "=" + artist);
            if(temperatures.SimilarArtists!=null && temperatures.SimilarArtists.Artist.Any())
                return temperatures.SimilarArtists.Artist.Select(a => a.Name).ToArray();
            return new string[] {};
        }
    }
}
