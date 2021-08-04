using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Business.Models;
using Shop.Business.ModelsDto;

namespace Shop.Business.IServices
{
    public interface IProductService
    {
        public Task<List<PlatformTypeEnum>> GetTopPlatforms();
        public Task<List<ProductDto>> GetByName(string name, int limit);

        public Task<ProductDto> GetProductInfo(Guid id);
        public Task<ProductDto> CreateProduct(StuffModel stuffModel);
        public Task<ProductDto> UpdateProduct(StuffModel stuffModel);
        public Task SoftDeleteProduct(Guid productId);
    }
}
