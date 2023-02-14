using Product.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Interfaces.ProductInterfaces
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<ProductGroup>> GetAllAsync();
        Task<IReadOnlyList<ProductGroup>> GetAsync(Expression<Func<ProductGroup, bool>> predicate);
        Task<ProductGroup> GetByIdAsync(string id);
        Task<ProductGroup> AddAsync(ProductGroup entity);
        Task UpdateAsync(ProductGroup entity);
        Task DeleteAsync(ProductGroup entity);
    }
}
