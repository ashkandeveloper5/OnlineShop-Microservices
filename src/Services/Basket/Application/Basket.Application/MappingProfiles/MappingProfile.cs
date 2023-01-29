using AutoMapper;
using Basket.Application.DTOs.BasketDtos;
using Basket.Core.Entities.Basket;
using Basket.Core.Entities.Order;
using EventBus.Message.Events.CheckoutEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<GetOrderForCheckout,BasketCheckout>().ReverseMap();
            CreateMap<BasketCheckoutEvent,BasketCheckout>().ReverseMap();
            CreateMap<GetOrderDto,Order>().ReverseMap();
        }
    }
}
