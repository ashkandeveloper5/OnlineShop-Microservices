using Product.Application.Interfaces.ProductInterfaces;
using Product.Domain.Entities.ProductEntities;
using Product.Domain.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services.ProductServices
{
    public class CommentService:ICommentService
    {
        protected readonly IAsyncRepository<ProductComment> _commentRepository;

        public CommentService(IAsyncRepository<ProductComment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IReadOnlyList<ProductComment>> GetAllAsync()
        {
            var result = await _commentRepository.GetAllAsync();
            return result;
        }

        public async Task<IReadOnlyList<ProductComment>> GetAsync(Expression<Func<ProductComment, bool>> predicate)
        {
            return await _commentRepository.GetAsync(predicate);
        }

        public virtual async Task<ProductComment> GetByIdAsync(string id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<ProductComment> AddAsync(ProductComment entity)
        {
            return await _commentRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(ProductComment entity)
        {
            await _commentRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(ProductComment entity)
        {
            await _commentRepository.DeleteAsync(entity);
        }

        public void Dispose()
        {
            _commentRepository?.Dispose();
        }
    }
}
