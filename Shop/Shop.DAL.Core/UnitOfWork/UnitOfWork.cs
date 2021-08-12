using System;
using System.Threading.Tasks;
using Shop.DAL.Core.Entities;
using Shop.DAL.Core.Repositories.Interfaces;

namespace Shop.DAL.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;

        public IRepository<Product> ProductRepository { get; }
        public IRepository<ProductRating> ProductRatingRepository { get; }
        public IRepository<Order> OrderRepository { get; }

        public UnitOfWork(ShopContext context, IRepository<Product> productsRepository, 
            IRepository<ProductRating> productRatingRepository, IRepository<Order> orderRepository)
        {
            _context = context;
            ProductRepository = productsRepository;
            ProductRatingRepository = productRatingRepository;
            OrderRepository = orderRepository;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}