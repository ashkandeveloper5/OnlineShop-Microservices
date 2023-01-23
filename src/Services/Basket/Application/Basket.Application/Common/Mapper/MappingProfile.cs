using AutoMapper;
using Basket.Application.DTOs.BasketDtos;
using Basket.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Common.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, AddToBasketDto>().ReverseMap();
        }
    }
}
