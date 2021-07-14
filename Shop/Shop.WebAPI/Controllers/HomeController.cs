using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shop.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            var jsonString = string.Empty;
            await using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, "Hello World!");
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                jsonString = await reader.ReadToEndAsync();
            }
            Log.Information($"HomeController Get Method, writing: {jsonString}");

            return Ok(jsonString);
        }
    }
}
