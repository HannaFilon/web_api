using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Core.Entities;
using Shop.DAL.Core.Repositories.Interfaces;

namespace Shop.DAL.Core.Repositories.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopContext context) : base(context)
        { }

        public List<int> GetTopPlatforms()
        {
            var result = _context.Products.GroupBy(x => x.Platform)
                .OrderByDescending(g => g.Count()).Take(3)
                .Select(l => new { Platform = l.Key, GamesCount = l.Count() });
            var platformsList = new List<int>();
            foreach (var item in result)
            {
                platformsList.Add(item.Platform);
            }

            platformsList.Reverse();

            return platformsList;
        }

        public async Task<List<Product>> GetByName(string name, int limit)
        {
            var gamesList = await _context.Products.Where(x => x.Name == name).Take(limit).AsNoTracking().ToListAsync();
            return gamesList;
        }
    }
}