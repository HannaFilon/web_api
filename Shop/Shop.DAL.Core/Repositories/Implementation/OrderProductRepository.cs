using Shop.DAL.Core.Entities;

namespace Shop.DAL.Core.Repositories.Implementation
{
    public class OrderProductRepository: Repository<OrderProduct>
    {
        public OrderProductRepository(ShopContext context) : base(context)
        {
        }
    }
}