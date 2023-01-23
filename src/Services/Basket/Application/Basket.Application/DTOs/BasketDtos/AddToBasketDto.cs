using Basket.Core.Entities.Order;
using Basket.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.DTOs.BasketDtos
{
    public class AddToBasketDto
    {
        public long Quantity { get; set; }

        public ColorEnums.Color Color { get; set; }

        public decimal Price { get; set; }


        [Required]

        public string ProductId { get; set; }

        public string ProductName { get; set; }

    }
}
