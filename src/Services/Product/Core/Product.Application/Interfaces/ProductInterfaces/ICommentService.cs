using Product.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Interfaces.ProductInterfaces
{
    public interface ICommentService
    {
        Task<IReadOnlyList<ProductComment>> GetAllAsync();
        Task<IReadOnlyList<ProductComment>> GetAsync(Expression<Func<ProductComment, bool>> predicate);
        Task<ProductComment> GetByIdAsync(string id);
        Task<ProductComment> AddAsync(ProductComment entity);
        Task UpdateAsync(ProductComment entity);
        Task DeleteAsync(ProductComment entity);
    }
}
