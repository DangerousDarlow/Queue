using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Queue.Services;
using Constraint = Queue.Model.Constraint;

namespace Queue.Repositories
{
    public interface IConstraintsRepository
    {
        Task<IEnumerable<Constraint>> Get(Guid queue);
        Task<Constraint> Create(Guid queue, string name);
        Task Delete(Guid id);
    }

    public class ConstraintsRepository : IConstraintsRepository
    {
        public ConstraintsRepository(MySqlConnection connection, IPrimeNumbers primeNumbers)
        {
            Connection = connection;
            PrimeNumbers = primeNumbers;
        }

        private MySqlConnection Connection { get; }
        private IPrimeNumbers PrimeNumbers { get; }

        public async Task<IEnumerable<Constraint>> Get(Guid queue)
        {
            await OpenConnectionIfNotOpen();

            const string query =
                "SELECT BIN_TO_UUID(id) AS id, BIN_TO_UUID(queue) AS queue, prime, name FROM constraints WHERE queue = UUID_TO_BIN(@queue)";

            return (await Connection.QueryAsync<Constraint>(query, new {queue})).ToList();
        }

        public async Task<Constraint> Create(Guid queue, string name)
        {
            await OpenConnectionIfNotOpen();

            var primes = await GetPrimesForQueue(queue);

            var constraint = new Constraint(Guid.NewGuid(), queue, PrimeNumbers.FirstPrimeNotIn(primes), name);

            const string query =
                "INSERT INTO constraints(id, queue, prime, name) VALUES(UUID_TO_BIN(@id), UUID_TO_BIN(@queue), @prime, @name)";

            await Connection.ExecuteAsync(query, constraint);

            return constraint;
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

        private Task<IEnumerable<long>> GetPrimesForQueue(Guid queue)
        {
            const string query = "SELECT prime FROM queue.constraints WHERE queue = UUID_TO_BIN(@queue) ORDER BY prime";
            return Connection.QueryAsync<long>(query, new {queue});
        }
    }
}