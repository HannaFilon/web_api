using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Business.IServices;
using Shop.Business.ModelsDto;
using Shop.DAL.Core.UnitOfWork;

namespace Shop.Business.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PlatformTypeEnum>> GetTopPlatforms()
        {
            var platformsList = await _unitOfWork.ProductRepository.Get()
                .GroupBy(p => p.Platform)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(l => new { Platform = l.Key, Date = l.Select(p => p.DateCreated).Max() })
                .OrderBy(l => l.Date)
                .ToArrayAsync();
            var topPlatforms = _mapper.Map<List<PlatformTypeEnum>>(platformsList);
            
            return topPlatforms;
        }

        public async Task<List<ProductDto>> GetByName(string name, int limit)
        {
            var gamesList = await _unitOfWork.ProductRepository.Get()
                .Where(x => x.Name == name)
                .Take(limit)
                .ToListAsync();
            var gamesDtoList = _mapper.Map<List<ProductDto>>(gamesList);
            
            return gamesDtoList;
        }
    }
}
