using System;

namespace Queue.Model
{
    public class Queue
    {
        public Queue(Guid queueId, QueueMaskType type)
        {
            QueueId = queueId;
            Type = type;
        }

        public Guid QueueId { get; }
        public QueueMaskType Type { get; }
    }
}