using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _log;

        public HomeController(ILogger logger)
        {
            _log = logger;
        }

        [HttpGet]
        public IActionResult GetInfo()
        {
            _log.Information($"HTTP request: Method - {HttpContext.Request.Method}");

            return Ok(JsonSerializer.Serialize("Hello World!"));
        }
    }
}
