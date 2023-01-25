using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Entities.Basket
{
    public class BasketCheckout:BaseEntity
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }

        // address

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        // payment

        public string? BankName { get; set; }
        public string? RefCode { get; set; }
        public int? PaymentMethod { get; set; }
    }
}
