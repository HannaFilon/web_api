using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Shop.Business.IServices;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IProductService _productService;


        public GamesController( IProductService productService)
        {
            _productService = productService;
        }


        [AllowAnonymous]
        [HttpGet("topPlatforms")]
        public async Task<IActionResult> GetTopPlatforms()
        {
            var topPlatforms = await _productService.GetTopPlatforms();
            if (topPlatforms.Any())
            {
                return Ok(topPlatforms);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, 
                "Error retrieving data from the database");
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName(string term, string limitString)
        {
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("No search parameters given.");
            }

            if (string.IsNullOrEmpty(limitString))
            {
                return BadRequest("No limit parameters given.");
            }

            var resultTryParse = int.TryParse(limitString, out int limit);
            if (!resultTryParse)
            {
                limit = 7;
            }

            var gamesList = await _productService.GetByName(term, limit);
            if (gamesList.Any())
            {
                return Ok(gamesList);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, 
                "Error retrieving data from the database");
        }
    }
}
