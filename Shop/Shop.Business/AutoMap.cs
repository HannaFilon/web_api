using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source =>source.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(source => source.Platform))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(source => source.DateCreated))
                .ForMember(dest => dest.TotalRating, opt => opt.MapFrom(source => source.TotalRating));
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(
                    (source,destination) => !string.IsNullOrEmpty(source.Id.ToString()) ? source.Id : destination.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(
                        (source, destination) => !string.IsNullOrEmpty(source.Name) ? source.Name : destination.Name))
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(source => source.Platform))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(
                    (source, destination) => !string.IsNullOrEmpty(source.DateCreated.ToShortDateString()) ? source.DateCreated : destination.DateCreated))
                .ForMember(dest => dest.TotalRating, opt => opt.MapFrom(source => source.TotalRating));


        }
    }
}
