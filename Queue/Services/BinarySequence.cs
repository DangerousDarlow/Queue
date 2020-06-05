using System;
using System.Collections.Generic;
using System.Linq;
using Queue.Model;

namespace Queue.Services
{
    public class BinarySequence : ISequence
    {
        public QueueMaskType Type { get; } = QueueMaskType.Binary;

        public long FirstNotIn(IEnumerable<long> exclude)
        {
            var excludeList = exclude.ToList();
            for (var value = 1L; value < long.MaxValue; value *= 2)
                if (!excludeList.Contains(value))
                    return value;

            throw new Exception("Unable to find sequence value");
        }
    }
}