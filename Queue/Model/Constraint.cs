using System;

namespace Queue.Model
{
    public class Constraint
    {
        public Constraint()
        {
        }

        public Constraint(Guid id, Guid queue, long prime, string name)
        {
            Id = id;
            Queue = queue;
            Prime = prime;
            Name = name;
        }

        public Guid Id { get; }

        public Guid Queue { get; }

        public long Prime { get; }

        public string Name { get; }
    }
}