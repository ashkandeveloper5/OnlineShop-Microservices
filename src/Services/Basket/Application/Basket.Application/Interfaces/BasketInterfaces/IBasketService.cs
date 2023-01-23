using Basket.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Interfaces.BasketInterfaces
{
    public interface IBasketService : IDisposable
    {
        #region Order
        Task<IReadOnlyList<OrderDetail>> GetAllAsync();
        Task<IReadOnlyList<OrderDetail>> GetAsync(Expression<Func<OrderDetail, bool>> predicate);
        Task<OrderDetail> GetByIdAsync(string id);
        Task<OrderDetail> AddAsync(OrderDetail entity,string userId);
        Task UpdateAsync(OrderDetail entity);
        Task DeleteAsync(OrderDetail entity);
        #endregion
    }
}
