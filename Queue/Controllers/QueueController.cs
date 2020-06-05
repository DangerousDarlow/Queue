using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Queue.Model;
using Queue.Repositories;

namespace Queue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        public QueueController(IQueueRepository queueRepository)
        {
            QueueRepository = queueRepository;
        }

        public IQueueRepository QueueRepository { get; }

        [HttpGet]
        [Route("/queues")]
        public async Task<IEnumerable<Model.Queue>> GetAll()
        {
            return await QueueRepository.GetAll();
        }

        [HttpPost]
        public async Task<Model.Queue> Create([FromForm] Guid id, [FromForm] QueueMaskType type)
        {
            var queue = new Model.Queue(id, type);
            await QueueRepository.Create(queue);
            return queue;
        }
    }
}