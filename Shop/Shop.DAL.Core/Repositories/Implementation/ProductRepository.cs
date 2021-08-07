using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Core.Entities;

namespace Shop.DAL.Core.Repositories.Implementation
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(ShopContext context) : base(context)
        {
        }

        /* public override async Task<Product> GetByID(Guid id)
         {
             var product = await _dbSet.FindAsync(id);
             if (product.IsDeleted)
             {
                 return null;
             }

             return product;
         }

         public override async Task<IEnumerable<Product>> GetAll()
         {
             return await _dbSet.AsNoTracking().Where(p => !p.IsDeleted).ToListAsync();
         }

         public override IQueryable<Product> Get()
         {
            return _dbSet.AsNoTracking().Where(p => !p.IsDeleted);
         }*/
    }
}