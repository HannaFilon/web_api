using Shop.DAL.Core.Entities;

namespace Shop.DAL.Core.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(ShopContext context) : base(context)
        {
        }
    }
}