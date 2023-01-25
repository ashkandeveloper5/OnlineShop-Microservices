using Basket.Core.Entities.Basket;
using Basket.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BasketCheckout = Basket.Core.Entities.Basket.BasketCheckout;

namespace Basket.Application.Interfaces.BasketInterfaces
{
    public interface IBasketService : IDisposable
    {
        #region Order
        Task<IReadOnlyList<OrderDetail>> GetAllOrderDetailAsync();
        Task<IReadOnlyList<Order>> GetAllOrderAsync();
        Task<IReadOnlyList<OrderDetail>> GetOrderDetailAsync(Expression<Func<OrderDetail, bool>> predicate);
        Task<IReadOnlyList<Order>> GetOrderAsync(Expression<Func<Order, bool>> predicate);
        Task<OrderDetail> GetByIdAsync(string id);
        Task<OrderDetail> AddAsync(OrderDetail entity,string userId);
        Task UpdateAsync(OrderDetail entity);
        Task DeleteAsync(OrderDetail entity);
        Task<BasketCheckout> AddCkeckout(BasketCheckout basketCheckout);
        #endregion
    }
}
