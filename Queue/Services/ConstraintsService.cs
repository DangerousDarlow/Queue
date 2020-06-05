using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Queue.Model;
using Queue.Repositories;

namespace Queue.Services
{
    public interface IConstraintsService
    {
        Task<IEnumerable<Constraint>> GetAll(Guid queue);
        Task<Constraint> Create(Guid queue, string name);
        Task Delete(Guid id);
    }

    public class ConstraintsService : IConstraintsService
    {
        public ConstraintsService(
            IConstraintsRepository constraintsRepository,
            ISequence sequence)
        {
            ConstraintsRepository = constraintsRepository;
            Sequence = sequence;
        }

        private IConstraintsRepository ConstraintsRepository { get; }
        private ISequence Sequence { get; }

        public async Task<Constraint> Create(Guid queue, string name)
        {
            var masks = await ConstraintsRepository.GetMasks(queue);
            var constraint = new Constraint(Guid.NewGuid(), queue, Sequence.FirstNotIn(masks), name);
            await ConstraintsRepository.Create(queue, constraint);
            return constraint;
        }

        public Task<IEnumerable<Constraint>> GetAll(Guid queue)
        {
            return ConstraintsRepository.GetAll(queue);
        }

        public Task Delete(Guid id)
        {
            return ConstraintsRepository.Delete(id);
        }
    }
}