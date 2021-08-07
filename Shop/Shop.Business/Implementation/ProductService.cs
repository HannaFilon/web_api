using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
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

        public async Task<IEnumerable<ProductDto>> GetDeleted()
        {
            var productsList = await _unitOfWork.ProductRepository.Get()
                .IgnoreQueryFilters().Where(p => p.IsDeleted).ToListAsync();
            var productsDtoList = _mapper.Map<List<ProductDto>>(productsList);

            return productsDtoList;
        }

        public async Task<ProductDto[]> GetProducts(ParametersList listParameters)
        {
            var filteredProducts = GetFilteredList(listParameters.Genre, listParameters.AgeRatig);
            if (filteredProducts == null)
            {
                return null;
            }

            if (listParameters.TotalRatingOrder != null && listParameters.PriceOrder != null)
            {
                throw new Exception("Can't order list by two parameters.");
            }

            if (listParameters.TotalRatingOrder == OrderBy.Asc)
            {
                filteredProducts = filteredProducts.OrderBy(p => p.TotalRating);
            }
            else if (listParameters.TotalRatingOrder == OrderBy.Desc)
            {
                filteredProducts = filteredProducts.OrderByDescending(p => p.TotalRating);
            }

            if (listParameters.PriceOrder == OrderBy.Asc)
            {
                filteredProducts = filteredProducts.OrderBy(p => p.Price);
            }
            else if (listParameters.PriceOrder == OrderBy.Desc)
            {
                filteredProducts = filteredProducts.OrderByDescending(p => p.Price);
            }

            var productsArray = await filteredProducts.ToArrayAsync();
            var productsDtoArray = _mapper.Map<ProductDto[]>(productsArray);

            return productsDtoArray;
        }

        private IQueryable<Product> GetFilteredList(string genreFilter, AgeRatingEnum? ageRatingFilter)
        {
            if (string.IsNullOrEmpty(genreFilter) && ageRatingFilter == null)
            {
                return null;
            }

            var products = _unitOfWork.ProductRepository.Get();
            if (string.IsNullOrEmpty(genreFilter) && ageRatingFilter != null)
            {
                products = products.Where(p => p.AgeRating == (int)ageRatingFilter);
            }

            if (!string.IsNullOrEmpty(genreFilter) && ageRatingFilter == null)
            {
                products = products.Where(p => p.Genre == genreFilter);
            }

            if (!string.IsNullOrEmpty(genreFilter) && ageRatingFilter != null)
            {
                products = products.Where(p => p.AgeRating == (int)ageRatingFilter).Where(p => p.Genre == genreFilter);
            }

            return products;
        }

        public async Task<ProductDto> CreateProduct(StuffModel stuffModel)
        {
            var product = _mapper.Map<Product>(stuffModel);
            if (stuffModel.Rating.HasValue)
            {
                product.TotalRating = stuffModel.Rating.Value;
            }

            await SaveImages(stuffModel, product);
            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChanges();
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<ProductDto> UpdateProduct(StuffModel stuffModel)
        {
            var product = await _unitOfWork.ProductRepository.Get().Where(p => p.Name =="Minecraft").FirstOrDefaultAsync();
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
            return null;
        }

        public async Task<RatingModel> EditRating(Guid productId, int rating, string userId)
        {
            var product =
                await _unitOfWork.ProductRepository.Get().Where(p => p.Id == productId).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Game not found.");
            }

            var productRating = new ProductRating()
            {
                ProductId = productId,
                UserId = Guid.Parse(userId),
                Rating = rating
            };

            await _unitOfWork.ProductRatingRepository.Add(productRating);
            var ratingSum = product.Ratings.Sum(p => p.Rating);
            var count = product.Ratings.Count;
            var ratingModel = new RatingModel()
            {
                ProductId = productId,
                TotalRating = (float) ratingSum / count
            };

            product.TotalRating = ratingModel.TotalRating;
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChanges();

            return ratingModel;
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
            var saveLogoImageTask = SaveImage(stuffModel.Logo, "Images/Logo");
            var saveBackgroundImageTask = SaveImage(stuffModel.Background, "Images/Background");

            await Task.WhenAll(saveLogoImageTask, saveBackgroundImageTask);

            product.Logo = saveLogoImageTask.Result;
            product.Background = saveBackgroundImageTask.Result;
        }

        private async Task<string> SaveImage(IFormFile imageFile, string folder)
        {
            string imageUrl = null;

            if (imageFile == null || imageFile.Length == 0)
            {
                return imageUrl;
            }

            using (var ms = new MemoryStream())
            {
                await imageFile.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var uploadParams = new ImageUploadParams
                {
                    Folder = folder,
                    File = new FileDescription(imageFile.FileName, ms),
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                imageUrl = uploadResult.Url?.AbsoluteUri;
            }

            return imageUrl;
        }
    }
}