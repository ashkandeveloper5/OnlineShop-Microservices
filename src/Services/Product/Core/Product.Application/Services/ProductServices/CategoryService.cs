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
    public class CategoryService:ICategoryService
    {
        protected readonly IAsyncRepository<ProductGroup> _categoryRepository;

        public CategoryService(IAsyncRepository<ProductGroup> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyList<ProductGroup>> GetAllAsync()
        {
            var result = await _categoryRepository.GetAllAsync();
            return result;
        }

        public async Task<IReadOnlyList<ProductGroup>> GetAsync(Expression<Func<ProductGroup, bool>> predicate)
        {
            return await _categoryRepository.GetAsync(predicate);
        }

        public virtual async Task<ProductGroup> GetByIdAsync(string id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<ProductGroup> AddAsync(ProductGroup entity)
        {
            return await _categoryRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(ProductGroup entity)
        {
            await _categoryRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(ProductGroup entity)
        {
            await _categoryRepository.DeleteAsync(entity);
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }
    }
}
