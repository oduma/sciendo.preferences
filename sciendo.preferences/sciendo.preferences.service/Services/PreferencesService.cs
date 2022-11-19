using System.Security.Cryptography.Xml;
using Google.Protobuf.Collections;
using sciendo.preferences.service;
using sciendo.preferences.service.Logic;

namespace sciendo.preferences.service.Services
{
    public class PreferencesService:Preferences.PreferencesBase
    {
        private ILogger<PreferencesService> _logger;
        private readonly ILastFmGetter lastFmGetter;
        private readonly IFilterLocally filterLocally;

        public PreferencesService(ILogger<PreferencesService> logger,
            ILastFmGetter lastFmGetter, IFilterLocally filterLocally)
        {
            _logger = logger;
            this.lastFmGetter = lastFmGetter;
            this.filterLocally = filterLocally;
        }

        public override Task<GetPreferencesResponse> GetPreferences(GetPreferencesRequest request, Grpc.Core.ServerCallContext context) 
        {
            var filteredSimilars = filterLocally.GetFiltered(
                lastFmGetter.GetSimilarArtists(request.ArtistName));
            var result = new GetPreferencesResponse(); 
            result.Artists.AddRange(filteredSimilars);
            return Task.FromResult(result);
        }
    }
}
