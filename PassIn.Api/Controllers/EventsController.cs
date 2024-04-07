using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestEventJson request)
        {
            return Created();
        }
    }
}
