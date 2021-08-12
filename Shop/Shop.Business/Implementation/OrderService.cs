using System.Runtime.Serialization;
using AutoMapper;
using Shop.Business.IServices;
using Shop.DAL.Core.UnitOfWork;

namespace Shop.Business.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}