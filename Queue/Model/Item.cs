using System;
using System.Collections.Generic;

namespace Queue.Model
{
    public class Item
    {
        public Guid Id { get; }
        
        public IEnumerable<Constraint> Constraints { get; }
    }
}