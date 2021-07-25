using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Business.IServices;
using Shop.Business.ModelsDto;
using Shop.DAL.Core;
using Shop.DAL.Core.Entities;
using Shop.DAL.Core.Repositories.Interfaces;
using Shop.DAL.Core.UnitOfWork;

namespace Shop.Business.Implementation
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public List<PlatformTypeEnum> GetTopPlatforms()
        {
          var results = _productRepository.GetTopPlatforms();
          var topPlatforms = _mapper.Map<List<PlatformTypeEnum>>(results);
          
          return topPlatforms;
        }

        public async Task<List<Product>> GetByName(string name, int limit)
        {
            var gamesList = await _productRepository.GetByName(name, limit);

            return gamesList;
        }
    }
}
//SELECT Platform, Count(*) As GamesCount FROM Products Group By Platform Order By GamesCount DESC Limit 3;