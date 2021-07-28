using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Business.ModelsDto;

namespace Shop.Business.IServices
{
    public interface IProductService
    {
        public Task<List<PlatformTypeEnum>> GetTopPlatforms();

        public Task<List<ProductDto>> GetByName(string name, int limit);
    }
}
