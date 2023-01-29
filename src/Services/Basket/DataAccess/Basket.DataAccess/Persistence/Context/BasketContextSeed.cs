using Basket.Core.Entities.Basket;
using Basket.Core.Entities.Order;
using Basket.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.DataAccess.Persistence.Context
{
    public class BasketContextSeed
    {
        public static async Task SeedAsync(BasketContext basketContext, ILogger<BasketContextSeed> logger)
        {
            if (!await basketContext.Orders.AnyAsync())
            {
                await basketContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
                await basketContext.OrderDetails.AddRangeAsync(GetPreconfiguredOrderDetails());
                await basketContext.BasketCheckouts.AddRangeAsync(GetPreconfiguredUserBasketCheckout());
                await basketContext.SaveChangesAsync();
                logger.LogInformation("data seed section configured");
            }
        }

        #region Seed Data Methods
        public static IEnumerable<Order> GetPreconfiguredOrders()
        {
            var order= new List<Order>
            {
                new Order {Id="1",CreateDate=DateTime.Now,IsFinally=true,UserId="123451234512345@ExampleId"},
            };
            return order;
        }
        public static IEnumerable<OrderDetail> GetPreconfiguredOrderDetails()
        {
            return new List<OrderDetail>
            {
                new OrderDetail {CreateDate=DateTime.Now,Id="1",Color=ColorEnums.Color.Green,OrderId="1", Price=500000,ProductId="1",Quantity=1,ProductName="A12"},
                new OrderDetail {CreateDate=DateTime.Now,Id="2",Color=ColorEnums.Color.Green,OrderId="1", Price=500000,ProductId="2",Quantity=2,ProductName="A13"},
                new OrderDetail {CreateDate=DateTime.Now,Id="3",Color=ColorEnums.Color.Green,OrderId="1", Price=500000,ProductId="3",Quantity=1,ProductName="A14"},
            };
        }
        public static IEnumerable<BasketCheckout> GetPreconfiguredUserBasketCheckout()
        {
            return new List<BasketCheckout>
            {
                new BasketCheckout {Id="1",CreateDate=DateTime.Now, UserId="123451234512345@ExampleId", BankName="Saderat", City="Tehran", Country="Iran", EmailAddress="Example@gmail.com", FirstName="Example", LastName="Example", PaymentMethod=1, RefCode="1" , TotalPrice=2000000},
            };
        }
        #endregion
    }
}
