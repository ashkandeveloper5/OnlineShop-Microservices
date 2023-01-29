using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.DTOs.OrderDtos
{
    internal class Order
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }

        public bool IsFinally { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
