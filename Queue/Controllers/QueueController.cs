using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Queue.Repositories;

namespace Queue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        public QueueController(IConstraintsRepository constraintsRepository)
        {
            ConstraintsRepository = constraintsRepository;
        }

        private IConstraintsRepository ConstraintsRepository { get; }

        private Guid Queue { get; } = Guid.Parse("15a442bd-a270-11ea-b334-0242ac110002");

        [HttpGet]
        public async Task<string> Get()
        {
            var constraints = await ConstraintsRepository.Get(Queue);
            return constraints.Count().ToString();
        }
    }
}