using System;
using System.Collections.Generic;
using System.Linq;

namespace Queue.Services
{
    public class PowersOfTwoSequence : ISequence
    {
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