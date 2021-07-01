using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok(JsonSerializer.Serialize("Hello World!"));
        }
    }
}
