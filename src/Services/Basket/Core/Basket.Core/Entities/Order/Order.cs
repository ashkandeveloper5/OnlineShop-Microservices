using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Entities.Order
{
    public class Order:BaseEntity
    {
        public string UserId { get; set; }

        public bool IsFinally { get; set; }

        public decimal? TotalPrice { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
