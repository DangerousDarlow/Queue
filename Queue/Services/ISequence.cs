using System.Collections.Generic;
using Queue.Model;

namespace Queue.Services
{
    public interface ISequence
    {
        QueueMaskType Type { get; }
        long FirstNotIn(IEnumerable<long> exclude);
    }
}