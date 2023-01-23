using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Product.Domain.Entities.ProductEntities;
using System.Threading.Tasks;

namespace Product.Application.Interfaces.ProductInterfaces
{
    public interface IProductService:IDisposable
    {
        Task<IReadOnlyList<ProductEntity>> GetAllAsync();
        Task<IReadOnlyList<ProductEntity>> GetAsync(Expression<Func<ProductEntity, bool>> predicate);
        Task<ProductEntity> GetByIdAsync(string id);
        Task<ProductEntity> AddAsync(ProductEntity entity);
        Task UpdateAsync(ProductEntity entity);
        Task DeleteAsync(ProductEntity entity);
    }
}
