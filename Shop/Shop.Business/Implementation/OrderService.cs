using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrder(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }

        public async Task<List<OrderDto>> GetUserOrders(string userIdStr)
        {
            var result = Guid.TryParse(userIdStr, out Guid userId);
            if (!result)
            {
                throw new Exception("Wrong userId.");
            }

            var ordersList = await _unitOfWork.OrderRepository.Get()
                .Where(o => o.UserId == userId).ToListAsync();
            var ordersDtoList = _mapper.Map<List<OrderDto>>(ordersList);

            return ordersDtoList;
        }

        public async Task<List<ProductDto>> GetOrderProducts(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            var productsList = order.Products.Select(p => p);
            var productDtosList = _mapper.Map<List<ProductDto>>(productsList); 

            return productDtosList;
        }
        

        public async Task<OrderDto> CreateOrder(string userId)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Comleted = false,
                Products = new List<Product>()
            };

            await _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveChanges();
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }

        public async Task<OrderDto> AddProductToOrder(Guid orderId, Guid productId, int amount)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            if (order.Comleted)
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

            var orderProduct = new OrderProduct();
            if (order.Products.Contains(product))
            {
                orderProduct = order.OrderProducts.Select(op => op).First(op => op.ProductId == productId);
                var amountAdd = orderProduct.Amount;
                order.OrderProducts.Remove(orderProduct);
                _unitOfWork.OrderRepository.Update(order);
                amount += amountAdd;
            }

            orderProduct.Amount = amount;
            orderProduct.ProductId = productId;
            orderProduct.OrderId = orderId;
            order.OrderProducts.Add(orderProduct);
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChanges();
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }


        public async Task RemoveProductFromOrder(Guid orderId, List<Guid> productIds)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            if (order.Comleted)
            {
                throw new Exception("Order is completed, can not remove products from order.");
            }

            foreach (var productId in productIds)
            {
                var product = await _unitOfWork.ProductRepository.GetById(productId);
                if (product == null && !order.Products.Contains(product))
                {
                    throw new Exception("Product not found");
                }

                var amount = order.OrderProducts.Where(op => op.OrderId == orderId && op.ProductId == productId)
                    .Select(op => op.Amount).FirstOrDefault();
                await RemoveAmount(productId, amount);
                order.Products.Remove(product);
                _unitOfWork.OrderRepository.Update(order);
            }

            await _unitOfWork.SaveChanges();
        }

        public async Task BuyOrder(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            order.Comleted = true;
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