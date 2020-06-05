using System;

namespace Queue.Model
{
    public class Queue
    {
        public Queue(Guid id, QueueMaskType type)
        {
            Id = id;
            Type = type;
        }

        public Guid Id { get; }
        public QueueMaskType Type { get; }
    }

    public enum QueueMaskType
    {
        Binary,
        Prime
    }
}