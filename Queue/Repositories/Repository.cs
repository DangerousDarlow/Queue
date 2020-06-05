using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Queue.Repositories
{
    public class Repository
    {
        public Repository(MySqlConnection connection)
        {
            Connection = connection;
        }

        protected MySqlConnection Connection { get; }

        protected async Task OpenConnectionIfNotOpen()
        {
            if (Connection.State != ConnectionState.Open)
                await Connection.OpenAsync();
        }
    }
}