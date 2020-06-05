using System;

namespace Queue.Model
{
    public class Constraint
    {
        // default constructor needed for dapper
        public Constraint()
        {
        }

        public Constraint(Guid constraintId, Guid queueId, long mask, string name)
        {
            ConstraintId = constraintId;
            QueueId = queueId;
            Mask = mask;
            Name = name;
        }

        public Guid ConstraintId { get; }

        public Guid QueueId { get; }

        public long Mask { get; }

        public string Name { get; }
    }
}