using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Common.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public long Count { get; set; }
        public decimal Price { get; set; }
        public string FirstDescription { get; set; }
        public string? SecondDescription { get; set; }
        public string? ThirdDescription { get; set; }
        public string Title { get; set; }
        public string GroupId { get; set; }
        public string? SubGroup { get; set; }
    }
}
