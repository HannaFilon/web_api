using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Business.IServices;
using Shop.Business.ModelsDto;
using Shop.DAL.Core.Entities;
using Shop.DAL.Core.UnitOfWork;

namespace Shop.Business.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<OrderDto> GetOrder(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.Get()
                .Include(o=> o.OrderProducts)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }

        public async Task<List<OrderDto>> GetUserOrders(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("");
            }

            var ordersList = await _unitOfWork.OrderRepository.Get()
                .Include(o => o.OrderProducts)
                .Where(o => o.UserId.ToString() == userId)
                .ToListAsync();
            var orderDtosList = _mapper.Map<List<OrderDto>>(ordersList);

            return orderDtosList;
        }

        public async Task<OrderDto> CreateOrder(string userId, Guid productId, int amount)
        {
            if (productId == default || amount == default)
            {
                throw new Exception("Can not create order.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                User = user,
                UserId = user.Id,
                Completed = false,
                OrderProducts = new List<OrderProduct>()
            };

            await _unitOfWork.OrderRepository.Add(order);
            var orderProduct = new OrderProduct()
            {
                Amount = amount,
                ProductId = productId,
                OrderId = order.OrderId
            };

            order.OrderProducts.Add(orderProduct);
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChanges();
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }

        public async Task<OrderDto> AddProductToOrder(Guid orderId, Guid productId, int amount)
        {
            var order = await _unitOfWork.OrderRepository.Get().AsTracking()
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            if (order.Completed)
            {
                throw new Exception("Order is completed, can not add products to order.");
            }

            var product = await _unitOfWork.ProductRepository.GetById(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var amountCheck = await AddAmount(productId, amount);
            if (!amountCheck)
            {
                throw new Exception("Not enough products left.");
            }

            var orderProduct = new OrderProduct()
            {
                Amount = amount,
                ProductId = productId,
                OrderId = orderId
            };

            if (order.OrderProducts.Count != 0)
            {
                var orderProductFound = order.OrderProducts.Select(op => op).FirstOrDefault(op => op.ProductId == productId);
                if (orderProductFound != null)
                {
                    var amountAdd = orderProductFound.Amount;
                    order.OrderProducts.Remove(orderProductFound);
                    _unitOfWork.OrderRepository.Update(order);
                    orderProduct.Amount += amountAdd;
                }
            }

            order.OrderProducts.Add(orderProduct);
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChanges();
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }


        public async Task RemoveProductFromOrder(Guid orderId, List<Guid> productsId)
        {
            var order = await _unitOfWork.OrderRepository.Get().AsTracking()
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            if (order.Completed)
            {
                throw new Exception("Order is completed, can not remove products from order.");
            }

            var products = _unitOfWork.ProductRepository.Get()
                .Select(p => p)
                .Where(p => productsId.Contains(p.Id));
            if (products.Count() != productsId.Count)
            {
                throw new Exception("Wrong parameters. Not all products found.");
            }

            foreach (var p in products)
            {
                var orderProduct = order.OrderProducts
                    .Select(op => op)
                    .FirstOrDefault(op => op.ProductId == p.Id);
                if (orderProduct == null)
                {
                    throw new Exception("No such product in order.");
                }

                await RemoveAmount(orderProduct.ProductId, orderProduct.Amount);
                order.OrderProducts.Remove(orderProduct);
                _unitOfWork.OrderRepository.Update(order);
            }

            await _unitOfWork.SaveChanges();
        }

        public async Task BuyOrder(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.Get()
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o=> o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            order.Completed = true;
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChanges();
        }

        private async Task<bool> AddAmount(Guid productId, int amount)
        {
            var product = await _unitOfWork.ProductRepository.GetById(productId);
            if (product.Count - amount > 0)
            {
                product.Count -= amount;
                _unitOfWork.ProductRepository.Update(product);
                await _unitOfWork.SaveChanges();

                return true;
            }

            return false;
        }

        private async Task RemoveAmount(Guid productId, int amount)
        {
            var product = await _unitOfWork.ProductRepository.GetById(productId);
            product.Count += amount;
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChanges();
        }
    }
}