using Shop.DAL.Core.Entities;

namespace Shop.DAL.Core.Repositories.Implementation
{
    public class ProductRatingRepository: Repository<ProductRating>
    {
        public ProductRatingRepository(ShopContext context) : base(context)
        {
        }
    }
}