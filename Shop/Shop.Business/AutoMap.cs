using AutoMapper;
using Shop.Business.Models;
using Shop.DAL.Core.Entities;

namespace Shop.Business
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
