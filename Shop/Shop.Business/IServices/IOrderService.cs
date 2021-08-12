using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Business.ModelsDto;

namespace Shop.Business.IServices
{
    public interface IOrderService
    {
        public Task<OrderDto> GetOrder(Guid orderDto);
        public Task<List<OrderDto>> GetUserOrders(string userId);
        public Task<List<ProductDto>> GetOrderProducts(Guid orderId);
        public Task<OrderDto> CreateOrder(string userId);
        public Task<OrderDto> AddProductToOrder(Guid orderId, Guid productId, int amount);
        public Task RemoveProductFromOrder(Guid orderId, List<Guid> products);
        public Task BuyOrder(Guid orderId);
    }
}