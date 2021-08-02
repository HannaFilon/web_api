using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.Business.ModelsDto;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public GamesController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("topPlatforms")]
        public async Task<IActionResult> GetTopPlatforms()
        {
            var topPlatforms = (await _productService.GetTopPlatforms())
                .Select(p => p.ToString())
                .Reverse()
                .ToList();

            if (topPlatforms.Any())
            {
                return Ok(topPlatforms);
            }

            return StatusCode(404,
                "No popular game platforms found.");
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
            if (gamesList.Any())
            {
                return Ok(gamesList);
            }

            return StatusCode(400,
                $"No games with name {term} found.");
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
            if (stuffModel == null)
            {
                return BadRequest("Not enough info to create a product.");
            }
            
            var productDto = await _productService.CreateProduct(stuffModel);
            if (productDto == null)
            {
                return BadRequest("Product can't be created.");
            }

            stuffModel = _mapper.Map<StuffModel>(productDto);

            return StatusCode(201, stuffModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] StuffModel stuffModel)
        {
            if (stuffModel == null)
            {
                return BadRequest("No info to update.");
            }

            var productDto = await _productService.UpdateProduct(stuffModel);
            if (productDto == null)
            {
                return BadRequest("Product can't be updated.");
            }

            stuffModel = _mapper.Map<StuffModel>(productDto);

            return Ok(stuffModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await _productService.SoftDeleteProduct(productId);

            return StatusCode(204);
        }
    }
}
