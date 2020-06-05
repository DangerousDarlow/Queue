using System;

namespace Queue.Model
{
    public class Constraint
    {
        // default constructor needed for dapper
        public Constraint()
        {
        }

        public Constraint(Guid id, Guid queue, long mask, string name)
        {
            Id = id;
            Queue = queue;
            Mask = mask;
            Name = name;
        }

        public Guid Id { get; }

        public Guid Queue { get; }

        public long Mask { get; }

        public string Name { get; }
    }
}