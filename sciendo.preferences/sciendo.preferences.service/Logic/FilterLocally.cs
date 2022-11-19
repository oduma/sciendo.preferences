namespace sciendo.preferences.service.Logic
{
    public class FilterLocally : IFilterLocally
    {
        public string[] GetFiltered(string[] artists)
        {
            return new string[] { "feg", "ghi", "hjk" };

        }
    }
}
