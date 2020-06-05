using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Constraint = Queue.Model.Constraint;

namespace Queue.Repositories
{
    public interface IConstraintsRepository
    {
        Task<IEnumerable<Constraint>> GetAll(Guid queue);
        Task<IEnumerable<long>> GetMasks(Guid queue);
        Task Create(Guid queue, Constraint constraint);
        Task Delete(Guid id);
    }

    public class ConstraintsRepository : IConstraintsRepository
    {
        public ConstraintsRepository(MySqlConnection connection)
        {
            Connection = connection;
        }

        private MySqlConnection Connection { get; }

        public async Task<IEnumerable<Constraint>> GetAll(Guid queue)
        {
            await OpenConnectionIfNotOpen();

            const string query =
                "SELECT BIN_TO_UUID(id) AS id, BIN_TO_UUID(queue) AS queue, mask, name FROM constraints WHERE queue = UUID_TO_BIN(@queue)";

            return (await Connection.QueryAsync<Constraint>(query, new {queue})).ToList();
        }

        public async Task<IEnumerable<long>> GetMasks(Guid queue)
        {
            await OpenConnectionIfNotOpen();

            const string query = "SELECT mask FROM queue.constraints WHERE queue = UUID_TO_BIN(@queue) ORDER BY mask";

            return await Connection.QueryAsync<long>(query, new {queue});
        }

        public async Task Create(Guid queue, Constraint constraint)
        {
            await OpenConnectionIfNotOpen();

            const string query =
                "INSERT INTO constraints(id, queue, mask, name) VALUES(UUID_TO_BIN(@id), UUID_TO_BIN(@queue), @mask, @name)";

            await Connection.ExecuteAsync(query, constraint);
        }

        public async Task Delete(Guid id)
        {
            await OpenConnectionIfNotOpen();

            const string query = "DELETE FROM constraints WHERE id = UUID_TO_BIN(@id)";

            await Connection.ExecuteAsync(query, new {id});
        }

        private async Task OpenConnectionIfNotOpen()
        {
            if (Connection.State != ConnectionState.Open)
                await Connection.OpenAsync();
        }
    }
}