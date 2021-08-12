using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Business.ModelsDto;

namespace Shop.Business.IServices
{
    public interface IOrderService
    {
        public Task CreateOrder();
        public Task<OrderDto> AddProductToOrder(Guid OrderId, Guid ProductId);
        public Task RemoveProductFromOrder(Guid OrderId, Guid ProductID);
        public Task<List<ProductDto>> GetOrder(Guid OrderId);
        public Task<List<OrderDto>> GetOrders(Guid UserId);
        public Task BuyOrder(List<Guid> OrderIds);
    }
}