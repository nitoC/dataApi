using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dataApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello world");
        }
    }
}
