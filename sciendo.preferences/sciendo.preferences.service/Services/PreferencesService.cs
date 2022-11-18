using System.Security.Cryptography.Xml;
using Google.Protobuf.Collections;
using sciendo.preferences.service;

namespace sciendo.preferences.service.Services
{
    public class PreferencesService:Preferences.PreferencesBase
    {
        private ILogger<PreferencesService> _logger;

        public PreferencesService(ILogger<PreferencesService> logger)
        {
            _logger = logger;
        }

        public override Task<GetPreferencesResponse> GetPreferences(GetPreferencesRequest request, Grpc.Core.ServerCallContext context) 
        {
            var result = new GetPreferencesResponse(); 
            result.Artists.Add("abc");
            result.Artists.Add("def");
            result.Artists.Add("ghi");
            return Task.FromResult(result);
        }
    }
}
