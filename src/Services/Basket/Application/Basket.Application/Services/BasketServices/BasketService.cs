using Basket.Application.Interfaces.BasketInterfaces;
using Basket.Core.Entities.Basket;
using Basket.Core.Entities.Order;
using Basket.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<OrderDetail> _orderDetailRepository;
        private readonly IAsyncRepository<Core.Entities.Basket.BasketCheckout> _basketCheckoutRepository;
        public BasketService(IAsyncRepository<Order> orderRepository, IAsyncRepository<OrderDetail> orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<OrderDetail> AddAsync(OrderDetail entity, string userId)
        {
            if (_orderRepository.GetAllAsync().Result.Any(o => !o.IsFinally))
            {
                var result=new OrderDetail();
                var order = _orderRepository.GetAllAsync().Result.FirstOrDefault(o => !o.IsFinally && o.UserId == userId);
                entity.OrderId = order.Id;
                if (_orderDetailRepository.GetAllAsync().Result.Where(o => o.ProductId == entity.ProductId).Any())
                {
                   _orderDetailRepository.GetAllAsync().Result.FirstOrDefault(o => o.ProductId == entity.ProductId).Quantity += 1;
                    return _orderDetailRepository.GetAsync(o=>o.ProductId==entity.ProductId).Result[0];
                }
                else
                {
                    return await _orderDetailRepository.AddAsync(entity);
                }
            }
            else
            {
                Order order = new Order()
                {
                    UserId = userId,
                    IsFinally = false,
                };
                var resultOrder = await _orderRepository.AddAsync(order);
                await _orderDetailRepository.SaveChangeAsync();
                entity.OrderId = resultOrder.Id;
                var resultOrderDetail = await _orderDetailRepository.AddAsync(entity);
                await _orderDetailRepository.SaveChangeAsync();
                return resultOrderDetail;
            }
        }

        public async Task DeleteAsync(OrderDetail entity)
        {
            var orderCount = _orderDetailRepository.GetAllAsync().Result.Where(o => o.OrderId == entity.OrderId).Count();
            if (orderCount > 1)
            {
                await _orderDetailRepository.DeleteAsync(entity);
                await _orderRepository.DeleteAsync(_orderRepository.GetByIdAsync(entity.OrderId).Result);
                await _orderDetailRepository.SaveChangeAsync();
            }
            await _orderDetailRepository.DeleteAsync(entity);
            await _orderDetailRepository.SaveChangeAsync();
        }

        public void Dispose()
        {
            _orderRepository?.Dispose();
            _orderDetailRepository?.Dispose();
        }

        public async Task<IReadOnlyList<Order>> GetAllOrderAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<OrderDetail>> GetAllOrderDetailAsync()
        {
            return await _orderDetailRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<OrderDetail>> GetOrderDetailAsync(Expression<Func<OrderDetail, bool>> predicate)
        {
            return await _orderDetailRepository.GetAsync(predicate);
        }

        public async Task<IReadOnlyList<Order>> GetOrderAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _orderRepository.GetAsync(predicate);
        }

        public Task<OrderDetail> GetByIdAsync(string id)
        {
            return _orderDetailRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(OrderDetail entity)
        {
            await _orderDetailRepository.UpdateAsync(entity);
        }

        public async Task<Core.Entities.Basket.BasketCheckout> AddCkeckout(Core.Entities.Basket.BasketCheckout basketCheckout)
        {
            await _basketCheckoutRepository.AddAsync(basketCheckout);
            return (Core.Entities.Basket.BasketCheckout)_basketCheckoutRepository.GetAsync(b=>b.Id==basketCheckout.Id).Result;
        }
    }
}
