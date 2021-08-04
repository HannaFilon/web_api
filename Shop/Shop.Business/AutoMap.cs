using AutoMapper;
using Shop.Business.Models;
using Shop.Business.ModelsDto;
using Shop.DAL.Core.Entities;

namespace Shop.Business
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.UserName) ? source.UserName : destination.UserName))
                .ForMember(dest => dest.AddressDelivery, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.AddressDelivery) ? source.AddressDelivery : destination.AddressDelivery))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.PhoneNumber) ? source.PhoneNumber : destination.PhoneNumber));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(source => source.Platform))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(source => source.DateCreated))
                .ForMember(dest => dest.TotalRating, opt => opt.MapFrom(source => source.TotalRating))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(source => source.Genre))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(source => source.Rating))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(source => source.Logo))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(source => source.Background))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(source => source.Price))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(source => source.Count));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.Id.ToString()) ? source.Id : destination.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.Name) ? source.Name : destination.Name))
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(
                    (source, destination) => source.Platform != default ? (int)source.Platform : destination.Platform))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.DateCreated.ToShortDateString()) ? source.DateCreated : destination.DateCreated))
                .ForMember(dest => dest.TotalRating, opt => opt.MapFrom(
                    (source, destination) => !source.TotalRating.Equals(default) ? source.TotalRating : destination.TotalRating))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.Genre) ? source.Genre : destination.Genre))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(
                    (source, destination) => source.Rating != default ? (int)source.Rating : destination.Rating))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.Logo) ? source.Logo : destination.Logo))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.Background) ? source.Background : destination.Background))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(
                    (source, destination) => !source.Price.Equals(default) ? source.Price : destination.Price))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(
                    (source, destination) => source.Count != default ? source.Count : destination.Count));

            CreateMap<ProductDto, ResponseStuffModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(source => source.Platform))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(source => source.DateCreated))
                .ForMember(dest => dest.TotalRating, opt => opt.MapFrom(source => source.TotalRating))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(source => source.Genre))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(source => source.Rating))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(source => source.Logo))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(source => source.Background))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(source => source.Price))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(source => source.Count));

            CreateMap<StuffModel, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.Id.ToString()) ? source.Id : destination.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.Name) ? source.Name : destination.Name))
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(
                    (source, destination) => source.Platform != null ? (int)source.Platform : destination.Platform))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(
                    (source, destination) => source.DateCreated ?? destination.DateCreated))
                .ForMember(dest => dest.TotalRating, opt => opt.MapFrom(
                    (source, destination) => source.TotalRating ?? destination.TotalRating))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.Genre) ? source.Genre : destination.Genre))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(
                     (source, destination) => source.Rating != null ? (int)source.Rating : destination.Rating))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(source => ToString()))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(source => ToString()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(
                    (source, destination) => source.Price ?? destination.Price))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(
                    (source, destination) => source.Count ?? destination.Count));
        }
    }
}
