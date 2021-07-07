using Microsoft.AspNetCore.Mvc;
using Serilog;
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
            Log.Information("Hello World!");

            return Ok(JsonSerializer.Serialize("Hello World!"));
        }
    }
}
