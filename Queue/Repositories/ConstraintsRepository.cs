using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Queue.Model;

namespace Queue.Repositories
{
    public interface IConstraintsRepository
    {
        Task<IEnumerable<Constraint>> GetAll(Guid queueId);
        Task<Constraint> Get(Guid constraintId);
        Task<IEnumerable<long>> GetMasks(Guid queueId);
        Task Create(Guid queueId, Constraint constraint);
        Task Delete(Guid constraintId);
    }

    public class ConstraintsRepository : Repository, IConstraintsRepository
    {
        public ConstraintsRepository(MySqlConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Constraint>> GetAll(Guid queueId)
        {
            await OpenConnectionIfNotOpen();

            const string query =
                "SELECT BIN_TO_UUID(id) AS constraintId, BIN_TO_UUID(queue) AS queueId, mask, name FROM constraints WHERE queue = UUID_TO_BIN(@queueId)";

            return await Connection.QueryAsync<Constraint>(query, new {queueId});
        }

        public async Task<Constraint> Get(Guid constraintId)
        {
            await OpenConnectionIfNotOpen();

            const string query =
                "SELECT BIN_TO_UUID(id) AS constraintId, BIN_TO_UUID(queue) AS queueId, mask, name FROM constraints WHERE id = @constraintId";

            return await Connection.ExecuteScalarAsync<Constraint>(query, new {constraintId});
        }

        public async Task<IEnumerable<long>> GetMasks(Guid queueId)
        {
            await OpenConnectionIfNotOpen();

            const string query = "SELECT mask FROM queue.constraints WHERE queue = UUID_TO_BIN(@queueId) ORDER BY mask";

            return await Connection.QueryAsync<long>(query, new {queueId});
        }

        public async Task Create(Guid queueId, Constraint constraint)
        {
            await OpenConnectionIfNotOpen();

            const string query =
                "INSERT INTO constraints(id, queue, mask, name) VALUES(UUID_TO_BIN(@constraintId), UUID_TO_BIN(@queueId), @mask, @name)";

            await Connection.ExecuteAsync(query, constraint);
        }

        public async Task Delete(Guid constraintId)
        {
            await OpenConnectionIfNotOpen();

            const string query = "DELETE FROM constraints WHERE id = UUID_TO_BIN(@constraintId)";

            await Connection.ExecuteAsync(query, new {constraintId});
        }
    }
}