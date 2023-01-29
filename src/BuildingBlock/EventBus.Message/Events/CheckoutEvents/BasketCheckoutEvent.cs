using EventBus.Message.Events.BaseEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Message.Events.CheckoutEvents
{
    public class BasketCheckoutEvent:IntegrationBaseEvent
    {
        public string OrderId { get; set; }
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
