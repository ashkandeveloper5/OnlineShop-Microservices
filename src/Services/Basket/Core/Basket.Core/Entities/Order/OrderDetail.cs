using Basket.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Entities.Order
{
    public class OrderDetail:BaseEntity
    {
        public long Quantity { get; set; }

        public ColorEnums.Color Color { get; set; }

        public decimal Price { get; set; }


        [Required]

        public string ProductId { get; set; }

        public string ProductName { get; set; }
        public string OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
    }
}
