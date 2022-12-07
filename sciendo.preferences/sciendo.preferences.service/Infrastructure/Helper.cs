namespace sciendo.preferences.service.Infrastructure
{
    public static class Helper
    {
        public static bool AllowMyClient(string host)
        {
            if (host.Contains("localhost"))
                return true;
            return false;
        }

    }
}
