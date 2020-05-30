using System;

namespace Queue.Model
{
    public class Constraint
    {
        public Guid Id { get; }

        public Guid Queue { get; }

        public int Prime { get; }

        public string Name { get; }
    }
}