using AutoMapper;
using Shop.Business.Models;
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
        }
    }
}
