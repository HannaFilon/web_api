using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.DAL.Core.Entities;

namespace Shop.DAL.Core.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public List<int> GetTopPlatforms();

        public Task<List<Product>> GetByName(string name, int limit);
    }
}