using Shop.DAL.Core.Entities;

namespace Shop.DAL.Core.Repositories.Implementation
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(ShopContext context) : base(context)
        {
        }
    }


}