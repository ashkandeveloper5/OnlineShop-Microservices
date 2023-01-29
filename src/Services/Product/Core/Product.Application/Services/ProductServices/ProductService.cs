using Product.Application.Interfaces.ProductInterfaces;
using Product.Domain.Interfaces.BaseInterfaces;
using Product.Persistence.Repository;
using System.Linq.Expressions;
using Product.Domain.Interfaces;
using Product.Domain.Entities.ProductEntities;

namespace Product.Application.Services.ProductServices
{
    public class ProductService:IProductService
    {
        protected readonly IAsyncRepository<ProductEntity> _productRepository;

        public ProductService(IAsyncRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyList<ProductEntity>> GetAllAsync()
        {
            var result= await _productRepository.GetAllAsync();
            return result;
        }

        public async Task<IReadOnlyList<ProductEntity>> GetAsync(Expression<Func<ProductEntity, bool>> predicate)
        {
            return await _productRepository.GetAsync(predicate);
        }

        public virtual async Task<ProductEntity> GetByIdAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<ProductEntity> AddAsync(ProductEntity entity)
        {
            return await _productRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(ProductEntity entity)
        {
          await _productRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(ProductEntity entity)
        {
            await _productRepository.DeleteAsync(entity);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
