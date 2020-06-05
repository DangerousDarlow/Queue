using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace Queue.Repositories
{
    public interface IQueueRepository
    {
        Task<IEnumerable<Model.Queue>> GetAll();
        Task<Model.Queue> Get(Guid queueId);
        Task Create(Model.Queue queue);
    }

    public class QueueRepository : Repository, IQueueRepository
    {
        public QueueRepository(MySqlConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Model.Queue>> GetAll()
        {
            await OpenConnectionIfNotOpen();

            const string query = "SELECT BIN_TO_UUID(id) AS queueId, type FROM queues";

            return await Connection.QueryAsync<Model.Queue>(query);
        }

        public async Task<Model.Queue> Get(Guid queueId)
        {
            await OpenConnectionIfNotOpen();

            const string query = "SELECT BIN_TO_UUID(id) AS queueId, type FROM queues WHERE id = UUID_TO_BIN(@queueId)";

            return await Connection.QuerySingleAsync<Model.Queue>(query, new {queueId});
        }

        public async Task Create(Model.Queue queue)
        {
            await OpenConnectionIfNotOpen();

            const string query = "INSERT INTO queues(id, type) VALUES(UUID_TO_BIN(@queueId), @type)";

            await Connection.ExecuteAsync(query, queue);
        }
    }
}