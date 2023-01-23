using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Entities.Order
{
    public class Order:BaseEntity
    {
        public Order()
        {

        }

        public Order(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        public bool IsFinally { get; set; }
        public List<OrderDetail> Items { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                if (Items != null && Items.Any())
                {
                    foreach (OrderDetail item in Items)
                    {
                        totalPrice += item.Price * item.Quantity;
                    }
                }

                return totalPrice;
            }
        }
    }
}
