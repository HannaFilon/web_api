using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Shop.Business.IServices;
using Shop.WebAPI.Auth;

namespace Shop.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthManager _authManager;

        public OrdersController(IOrderService orderService, IAuthManager authManager)
        {
            _orderService = orderService;
            _authManager = authManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder(Guid productId, int amount)
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var userId = GetCurrentUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var orderDto = await _orderService.CreateOrder(userId);
            if (orderDto == null)
            {
                return BadRequest("Order not found.");
            }

            orderDto = await _orderService.AddProductToOrder(orderDto.OrderId, productId, amount);

            return StatusCode(201, orderDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProductToOrder(Guid orderId, Guid productId, int amount)
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var userId = GetCurrentUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var orderDto = await _orderService.GetOrder(orderId);
            if (orderDto == null)
            {
                return BadRequest("Order not found.");
            }

            orderDto = await _orderService.AddProductToOrder(orderId, productId, amount);

            return Ok(orderDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid? orderId)
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var userId = GetCurrentUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            if (!orderId.HasValue)
            {
                var orderDtosList = await _orderService.GetUserOrders(userId);

                return Ok(orderDtosList);
            }

            var orderDto = await _orderService.GetOrder(orderId.Value);
            if (orderDto == null)
            {
                return BadRequest("Order not found.");
            }

            var productDtosList = await _orderService.GetOrderProducts(orderId.Value);

            return Ok(productDtosList);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteProductsFromOrder(Guid orderId, List<Guid> productsList)
        {
            await _orderService.RemoveProductFromOrder(orderId, productsList);
           
            return StatusCode(204);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BuyOrder(Guid orderId)
        {
            await _orderService.BuyOrder(orderId);

            return StatusCode(204);
        }

        private string GetCurrentUserId(string token)
        {
            var jwtToken = _authManager.DecodeJwtToken(token);
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            return userId;
        }
    }
}