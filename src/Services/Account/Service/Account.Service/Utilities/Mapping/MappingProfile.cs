using Account.Core.Entities.User;
using Account.Service.DTOs;
using Account.Service.DTOs.UserDto;
using AutoMapper;
using Basket.Application.DTOs.BasketDtos;
using Product.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.Utilities.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<ProductDto, GetAllProductsListResponse>().ReverseMap();
            CreateMap<ProductDto, GetProductByIdResponse>().ReverseMap();
            CreateMap<ProductDto, GetAllProductsResponse>().ReverseMap();
        }
    }
}
