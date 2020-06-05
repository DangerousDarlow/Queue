using System.Collections.Generic;
using System.Linq;
using Queue.Model;

namespace Queue.Services
{
    public interface ISequenceSelector
    {
        ISequence Select(QueueMaskType type);
    }

    public class SequenceSelector : ISequenceSelector
    {
        public SequenceSelector(IEnumerable<ISequence> sequences)
        {
            Sequences = sequences.ToDictionary(sequence => sequence.Type);
        }

        private Dictionary<QueueMaskType, ISequence> Sequences { get; }

        public ISequence Select(QueueMaskType type)
        {
            return Sequences[type];
        }
    }
}