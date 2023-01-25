using AutoMapper;
using Order.Core.DTOs;
using Order.Core.Interfaces;
using Order.Domain.Entities;
using Order.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IAsyncRepository<OrderEntity> _orderRepository;
        public CheckoutService(IAsyncRepository<OrderEntity> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderForCheckout> AddCheckout(GetOrderForCheckout orderForCheckout)
        {
            var request = new OrderEntity
            {
                BankName = orderForCheckout.BankName,
                City = orderForCheckout.City,
                Country = orderForCheckout.Country,
                LastName = orderForCheckout.LastName,
                EmailAddress = orderForCheckout.EmailAddress,
                PaymentMethod = orderForCheckout.PaymentMethod,
                UserId = orderForCheckout.UserId,
                TotalPrice = orderForCheckout.TotalPrice,
                RefCode = orderForCheckout.RefCode,
                FirstName = orderForCheckout.FirstName,
            };
           var result=await _orderRepository.AddAsync(request);
            return new GetOrderForCheckout
            {
                BankName = result.BankName,
                City = result.City,
                Country = result.Country,
                LastName = result.LastName,
                EmailAddress = result.EmailAddress,
                PaymentMethod = result.PaymentMethod,
                UserId = result.UserId,
                TotalPrice = result.TotalPrice,
                RefCode = result.RefCode,
                FirstName = result.FirstName,
            };
        }

        public void Dispose()
        {
            _orderRepository?.Dispose();
        }

        public async Task<IEnumerable<GetOrderForCheckout>> GetAllCheckout()
        {
            var result = await _orderRepository.GetAllAsync();
            var response = new List<GetOrderForCheckout>();
            foreach (var item in result)
            {
                response.Add(new GetOrderForCheckout
                {
                    BankName = item.BankName,
                    City = item.City,
                    Country = item.Country,
                    EmailAddress = item.EmailAddress,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PaymentMethod = item.PaymentMethod,
                    RefCode = item.RefCode,
                    TotalPrice = item.TotalPrice,
                    UserId = item.UserId,
                });
            }
            return response;
        }

        public async Task<GetOrderForCheckout> GetCheckoutById(string ckeckoutId)
        {
            var result = await _orderRepository.GetByIdAsync(ckeckoutId);
            return new GetOrderForCheckout
            {
                BankName = result.BankName,
                City = result.City,
                Country = result.Country,
                EmailAddress = result.EmailAddress,
                FirstName = result.FirstName,
                LastName = result.LastName,
                PaymentMethod = result.PaymentMethod,
                RefCode = result.RefCode,
                TotalPrice = result.TotalPrice,
                UserId = result.UserId,
            };
        }
    }
}
