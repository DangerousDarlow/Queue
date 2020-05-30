using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Queue.Model;

namespace Queue.Repositories
{
    public interface IConstraintsRepository
    {
        Task<IEnumerable<Constraint>> Get(Guid queue);
        Task Add(Guid queue, Constraint constraint);
        Task Delete(Guid id);
    }

    public class ConstraintsRepository : IConstraintsRepository
    {
        public ConstraintsRepository(MySqlConnection connection)
        {
            Connection = connection;
        }

        private MySqlConnection Connection { get; }

        public async Task<IEnumerable<Constraint>> Get(Guid queue)
        {
            const string query =
                "SELECT BIN_TO_UUID(id), BIN_TO_UUID(queue), prime, name FROM constraints WHERE queue = UUID_TO_BIN(@queue)";

            return (await Connection.QueryAsync<Constraint>(query, new {queue})).ToList();
        }

        public async Task Add(Guid queue, Constraint constraint)
        {
            const string query =
                "INSERT INTO constraints(id, queue, prime, name) VALUES(UUID_TO_BIN(@id), UUID_TO_BIN(@queue), @prime, @name)";

            await Connection.ExecuteAsync(query, constraint);
        }

        public async Task Delete(Guid id)
        {
            const string query = "DELETE FROM constraints WHERE id = UUID_TO_BIN(@id)";

            await Connection.ExecuteAsync(query, new {id});
        }
    }
}