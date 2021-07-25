using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Shop.Business.IServices;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public GamesController(IMapper mapper, IProductService productService)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet("topPlatforms")]
        public IActionResult GetTopPlatforms()
        {
            var topPlatforms = _productService.GetTopPlatforms();

            return Ok(topPlatforms);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> GetGamesByName(string name, string limitString)
        {
            var limit = Convert.ToInt32(limitString);
            var gamesList = await _productService.GetByName(name, limit);

            return Ok(gamesList);
        }
    }
}
