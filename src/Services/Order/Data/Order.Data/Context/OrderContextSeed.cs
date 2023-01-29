using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Data.Context
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!await orderContext.Orders.AnyAsync())
            {
                await orderContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("data seed section configured");
            }
        }

        #region Seed Data Methods
        public static IEnumerable<OrderEntity> GetPreconfiguredOrders()
        {
            return new List<OrderEntity>
            {
                new OrderEntity {Id="1",CreateDate=DateTime.Now, UserId="123451234512345@ExampleId", BankName="Saderat", City="Tehran", Country="Iran", EmailAddress="Example@gmail.com", FirstName="Example", LastName="Example", PaymentMethod=1, RefCode="1" , TotalPrice=2000000},
            };
        }
        #endregion
    }
}
