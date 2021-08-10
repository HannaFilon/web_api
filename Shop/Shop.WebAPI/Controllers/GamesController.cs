using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.WebAPI.Auth;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public GamesController(IProductService productService, IMapper mapper, IAuthManager authManager)
        {
            _productService = productService;
            _mapper = mapper;
            _authManager = authManager;
        }

        [AllowAnonymous]
        [HttpGet("topPlatforms")]
        public async Task<IActionResult> GetTopPlatforms()
        {
            var topPlatforms = (await _productService.GetTopPlatforms())
                .Select(p => p.ToString())
                .Reverse()
                .ToList();

            if (!topPlatforms.Any())
            {
                return StatusCode(404,
                    "No popular game platforms found.");
            }

            return Ok(topPlatforms);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName(string term, string limit)
        {
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("No search parameters given.");
            }

            if (string.IsNullOrEmpty(limit))
            {
                return BadRequest("No limit parameters given.");
            }

            var resultTryParse = int.TryParse(limit, out int limitNumber);
            if (!resultTryParse)
            {
                limitNumber = 5;
            }

            var gamesList = await _productService.GetByName(term, limitNumber);
            if (!gamesList.Any())
            {
                return StatusCode(400,
                    $"No games with name {term} found.");
            }

            return Ok(gamesList);
        }

        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<IActionResult> GetProductInfo(Guid productId)
        {
            if (string.IsNullOrEmpty(productId.ToString()))
            {
                return BadRequest("Bad query parameters.");
            }

            var productDto = await _productService.GetProductInfo(productId);
            if (productDto == null)
            {
                return BadRequest("Game not found.");
            }

            return Ok(productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] StuffModel stuffModel)
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var jwtToken = _authManager.DecodeJwtToken(token);
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            if (stuffModel == null)
            {
                return BadRequest("Not enough info to create a product.");
            }

            var productDto = await _productService.CreateProduct(stuffModel);
            if (stuffModel.Rating.HasValue)
            {
                var result = await EditRating(productDto.Id, stuffModel.Rating.Value, userId);
            }

            if (productDto == null)
            {
                return BadRequest("Product can't be created.");
            }

            var responseStuffModel = _mapper.Map<ResponseStuffModel>(productDto);

            return StatusCode(201, responseStuffModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] StuffModel stuffModel)
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var jwtToken = _authManager.DecodeJwtToken(token);
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            if (stuffModel == null)
            {
                return BadRequest("No info to update.");
            }

            if (stuffModel.Rating.HasValue)
            {
                var result = await EditRating(stuffModel.Id, stuffModel.Rating.Value, userId);
            }

            var productDto = await _productService.UpdateProduct(stuffModel);
            if (productDto == null)
            {
                return BadRequest("Product can't be updated.");
            }

            var responseStuffModel = _mapper.Map<ResponseStuffModel>(productDto);

            return Ok(responseStuffModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await _productService.SoftDeleteProduct(productId);

            return StatusCode(204);
        }

        [HttpPost("rating")]
        private async Task<IActionResult> EditRating([FromBody] Guid productId, int rating, string userId)
        {
            if (productId == default || rating == default || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Not enough info to update rating.");
            }

            var ratingModel = await _productService.EditRating(productId, rating, userId);
            if (ratingModel == null)
            {
                return BadRequest("Can't update TotalRating.");
            }

            return Ok(ratingModel);
        }
    }
}
