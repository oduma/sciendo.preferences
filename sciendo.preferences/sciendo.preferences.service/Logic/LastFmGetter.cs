using Sciendo.Last.Fm;

namespace sciendo.preferences.service.Logic
{
    public class LastFmGetter : ILastFmGetter
    {
        private readonly ILogger<ILastFmGetter> logger;
        private readonly IUrlProvider urlProvider;
        private const string methodName = "artist.getsimilar";
        private const string userName = "scentmaster";
        private const string artistParameterName = "artist";

        public LastFmGetter(ILogger<ILastFmGetter> logger, IUrlProvider urlProvider)
        {
            this.logger = logger;
            this.urlProvider = urlProvider;
        }
        public string[] GetSimilarArtists(string artist)
        {
            var result= new List<string>();
            for (int i = 1; i < 10; i++)
                result.Add(
                    urlProvider.GetUrl(methodName, userName, i, artistParameterName + "=" + artist)
                    .ToString());
            return result.ToArray(); 
        }
    }
}
