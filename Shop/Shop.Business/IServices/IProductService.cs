using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Business.ModelsDto;
using Shop.DAL.Core.Entities;

namespace Shop.Business.IServices
{
    public interface IProductService
    {
        public List<PlatformTypeEnum> GetTopPlatforms();

        public Task<List<Product>> GetByName(string name, int limit);
    }
}
