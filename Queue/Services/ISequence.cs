using System.Collections.Generic;

namespace Queue.Services
{
    public interface ISequence
    {
        long FirstNotIn(IEnumerable<long> exclude);
    }
}