using Microsoft.AspNetCore.Mvc;

namespace Queue.Controllers
{
    [ApiController]
    [Route("queue")]
    public class QueueController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "hello";
        }
    }
}