using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Index()
       {
         return Ok("Welcome to the Dashboard");
       }
    }
}
