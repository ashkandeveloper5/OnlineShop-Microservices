using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Interfaces.BaseInterfaces
{
    public interface IAsyncRepository<TEntity>:IDisposable where TEntity : BaseEntity
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> AddAsync(TEntity entity);
        Task SaveChangeAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
