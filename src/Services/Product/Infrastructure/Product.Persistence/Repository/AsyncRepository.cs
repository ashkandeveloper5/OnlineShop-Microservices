using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Entities.ProductEntities;
using Product.Domain.Interfaces.BaseInterfaces;
using Product.Persistence.Context;
using Product.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Persistence.Repository
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ProductContext _context;
        private DbSet<TEntity> _query;

        public AsyncRepository(ProductContext Context)
        {
            _context = Context;
            _query = _context.Set<TEntity>();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _query.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            return await _query.FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _query.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _query.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
