using System.Data;

namespace sciendo.preferences.service.Logic
{
    public class Repository : IRepository, IDisposable
    {
        private readonly IDbConnection connection;

        public Repository(IDbConnection connection) 
        {
            this.connection = connection;
            this.connection.Open();
        }
        public void Dispose()
        {
            this.connection.Close();
        }

        public IEnumerable<string> GetAll()
        {
            var command = connection.CreateCommand();
            command.CommandText = "select distinct artist from songs";
            var reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                yield return reader.GetString(0);
            }

        }
    }
}
