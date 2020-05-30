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
    public class ConstraintController : ControllerBase
    {
        public ConstraintController(IConstraintsRepository constraintsRepository)
        {
            ConstraintsRepository = constraintsRepository;
        }

        private IConstraintsRepository ConstraintsRepository { get; }

        [HttpGet]
        [Route("/constraints")]
        public async Task<IEnumerable<Constraint>> Get([FromForm] Guid queue)
        {
            return await ConstraintsRepository.Get(queue);
        }

        [HttpPost]
        public async Task<Constraint> Create([FromForm] Guid queue, [FromForm] string name)
        {
            return await ConstraintsRepository.Create(queue, name);
        }

        [HttpDelete]
        public async Task Delete([FromForm] Guid id)
        {
            await ConstraintsRepository.Delete(id);
        }
    }
}