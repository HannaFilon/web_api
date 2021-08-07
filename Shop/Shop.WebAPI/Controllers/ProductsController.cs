using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.IServices;
using Shop.Business.Models;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] ParametersList parametersList)
        {
            if (parametersList == null)
            {
                return BadRequest("No query parameters given.");
            }

            var productsArray = await _productService.GetProducts(parametersList);
            if (productsArray == null)
            {
                return BadRequest("Wrong filtering or sorting parameters.");
            }

            return Ok(productsArray);
        }
    }
}