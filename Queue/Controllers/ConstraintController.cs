using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Queue.Model;
using Queue.Services;

namespace Queue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConstraintController : ControllerBase
    {
        public ConstraintController(IConstraintsService constraintsService)
        {
            ConstraintsService = constraintsService;
        }

        public IConstraintsService ConstraintsService { get; }

        [HttpGet]
        [Route("/constraints")]
        public async Task<IEnumerable<Constraint>> GetAll([FromForm] Guid queue)
        {
            return await ConstraintsService.GetAll(queue);
        }

        [HttpPost]
        public async Task<Constraint> Create([FromForm] Guid queue, [FromForm] string name)
        {
            return await ConstraintsService.Create(queue, name);
        }

        [HttpDelete]
        public async Task Delete([FromForm] Guid id)
        {
            await ConstraintsService.Delete(id);
        }
    }
}