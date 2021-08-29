using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shop.Business.IServices;
using Shop.WebAPI.Auth;

namespace Shop.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, IAuthManager authManager) : base(authManager)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder(Guid? orderId, Guid productId, int amount)
        {
            var userId = await GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var orderDto = await _orderService.GetOrder(orderId.GetValueOrDefault());
            if (orderDto == null)
            {
                orderDto = await _orderService.CreateOrder(userId, productId, amount);
                if (orderDto == null)
                {
                    return BadRequest("Order not found.");
                }
            }
            else
            {
                orderDto = await _orderService.AddProductToOrder(orderDto.OrderId, productId, amount);
            }

            return StatusCode(201, orderDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid? orderId)
        {
            var userId = await GetCurrentUserId();
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

            var productDtosList = orderDto.Products.Select(o => new { ProductId = o.ProductId, Amount = o.Amount }).ToList();

            return Ok(productDtosList);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteProductsFromOrder(Guid orderId, [FromForm] List<Guid> productsList)
        {
            await _orderService.RemoveProductFromOrder(orderId, productsList);

            return StatusCode(204);
        }

        [Authorize]
        [HttpPost("buy")]
        public async Task<IActionResult> BuyOrder(Guid orderId)
        {
            await _orderService.BuyOrder(orderId);

            return StatusCode(204);
        }
    }
}