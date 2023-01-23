using AutoMapper;
using Product.Common.DTOs.ProductDtos;
using Product.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Common.Utilities.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<ProductEntity, CreateProductDto>().ReverseMap();
        }
    }
}
