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
        public Task<IEnumerable<ProductDto>> GetDeleted();
        public Task<ProductDto[]> GetProducts(ParametersList parametersList);

        public Task<ProductDto> CreateProduct(StuffModel stuffModel);
        public Task<ProductDto> UpdateProduct(StuffModel stuffModel);
        public Task<RatingModel> EditRating(Guid productId, int rating, string userId);
        public Task SoftDeleteProduct(Guid productId);
    }
}