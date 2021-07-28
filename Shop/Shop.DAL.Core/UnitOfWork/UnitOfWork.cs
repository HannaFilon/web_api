using System;
using System.Threading.Tasks;
using Shop.DAL.Core.Entities;
using Shop.DAL.Core.Repositories.Interfaces;

namespace Shop.DAL.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;
        private readonly IRepository<Product> _productRepository;

        public UnitOfWork(ShopContext context, IRepository<Product> productsRepository) 
        {
            _context = context;
            _productRepository = productsRepository;
        }

        public IRepository<Product> ProductRepository => _productRepository;

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