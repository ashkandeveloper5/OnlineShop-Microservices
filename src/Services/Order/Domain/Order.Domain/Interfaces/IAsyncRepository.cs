using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces
{
    public interface IAsyncRepository<T> : IDisposable where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task SaveChangeAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
