using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.Business.ModelsDto;
using Shop.DAL.Core.Entities;
using Shop.DAL.Core.UnitOfWork;

namespace Shop.Business.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, Cloudinary cloudinary)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<List<PlatformTypeEnum>> GetTopPlatforms()
        {
            var platformsList = await _unitOfWork.ProductRepository.Get()
                .GroupBy(p => p.Platform)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(l => new { Platform = l.Key, Date = l.Select(p => p.DateCreated).Max() })
                .OrderBy(l => l.Date)
                .Select(l => l.Platform)
                .ToArrayAsync();
            var topPlatforms = _mapper.Map<List<PlatformTypeEnum>>(platformsList);

            return topPlatforms;
        }

        public async Task<List<ProductDto>> GetByName(string name, int limit)
        {
            var gamesList = await _unitOfWork.ProductRepository.Get()
                .Where(p => p.Name == name)
                .Take(limit)
                .ToListAsync();
            var gamesDtoList = _mapper.Map<List<ProductDto>>(gamesList);

            return gamesDtoList;
        }

        public async Task<ProductDto> GetProductInfo(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByID(productId);
            if (product == null)
            {
                throw new Exception("Game not found.");
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<ProductDto> CreateProduct(StuffModel stuffModel)
        {
            var product = _mapper.Map<Product>(stuffModel);
            await SaveImages(stuffModel, product);
            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChanges();
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<ProductDto> UpdateProduct(StuffModel stuffModel)
        {
            var product = await _unitOfWork.ProductRepository.Get().Where(p => p.Id == stuffModel.Id).SingleOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Game not found.");
            }

            _mapper.Map(stuffModel, product);
            await SaveImages(stuffModel, product);
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChanges();
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task SoftDeleteProduct(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByID(productId);
            if (product == null)
            {
                throw new Exception("Game not found.");
            }

            product.IsDeleted = true;
            _unitOfWork.ProductRepository.Update(product);

            await _unitOfWork.SaveChanges();
        }

        private async Task SaveImages(StuffModel stuffModel, Product product)
        {
            if (stuffModel.Logo is { Length: > 0 })
            {
                byte[] fileStream = null;
                await using (var ms = new MemoryStream())
                {
                    await stuffModel.Logo.CopyToAsync(ms);
                    fileStream = ms.ToArray();
                }

                ImageUploadResult result = null;
                await using (var ms = new MemoryStream(fileStream))
                {
                    ImageUploadParams uploadParams = new ImageUploadParams
                    {
                        Folder = "Images/Logo",
                        File = new FileDescription(stuffModel.Logo.FileName, ms),
                    };

                    result = await _cloudinary.UploadAsync(uploadParams);
                }

                var logoUrl = result.Url.AbsoluteUri;
                product.Logo = logoUrl;
            }

            if (stuffModel.Background is { Length: > 0 })
            {
                byte[] fileStream = null;
                await using (var ms = new MemoryStream())
                {
                    await stuffModel.Background.CopyToAsync(ms);
                    fileStream = ms.ToArray();
                }

                ImageUploadResult result = null;
                await using (var ms = new MemoryStream(fileStream))
                {
                    ImageUploadParams uploadParams = new ImageUploadParams
                    {
                        Folder = "Images/Background",
                        File = new FileDescription(stuffModel.Background.FileName, ms),
                    };

                    result = await _cloudinary.UploadAsync(uploadParams);
                }

                product.Background = result.Url.AbsoluteUri;
            }
        }
    }
}
