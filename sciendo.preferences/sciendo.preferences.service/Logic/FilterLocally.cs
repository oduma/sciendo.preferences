namespace sciendo.preferences.service.Logic
{
    public class FilterLocally : IFilterLocally
    {
        private readonly ILogger<FilterLocally> logger;
        private readonly IRepository repository;

        public FilterLocally(ILogger<FilterLocally> logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }
        public string[] GetFiltered(string[] artists)
        {
            var localArtists = repository.GetAll();
            var query = from artist in artists
                        join localArtist in localArtists
                        on artist equals localArtist
                        select artist;
            return query.ToArray();

        }
    }
}
