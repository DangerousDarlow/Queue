using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Queue.Model;
using Queue.Repositories;

namespace Queue.Services
{
    public interface IConstraintsService
    {
        Task<IEnumerable<Constraint>> GetAll(Guid queueId);
        Task<Constraint> Get(Guid constraintId);
        Task<Constraint> Create(Guid queueId, string name);
        Task Delete(Guid constraintId);
    }

    public class ConstraintsService : IConstraintsService
    {
        public ConstraintsService(
            IConstraintsRepository constraintsRepository,
            IQueueRepository queueRepository,
            ISequence sequence)
        {
            ConstraintsRepository = constraintsRepository;
            QueueRepository = queueRepository;
            Sequence = sequence;
        }

        private IConstraintsRepository ConstraintsRepository { get; }
        private IQueueRepository QueueRepository { get; }
        private ISequence Sequence { get; }

        public Task<IEnumerable<Constraint>> GetAll(Guid queueId)
        {
            return ConstraintsRepository.GetAll(queueId);
        }

        public Task<Constraint> Get(Guid constraintId)
        {
            return ConstraintsRepository.Get(constraintId);
        }

        public async Task<Constraint> Create(Guid queueId, string name)
        {
            var masks = await ConstraintsRepository.GetMasks(queueId);
            var queue = await QueueRepository.Get(queueId);
            var constraint = new Constraint(Guid.NewGuid(), queueId, Sequence.FirstNotIn(masks), name);
            await ConstraintsRepository.Create(queueId, constraint);
            return constraint;
        }

        public Task Delete(Guid constraintId)
        {
            return ConstraintsRepository.Delete(constraintId);
        }
    }
}