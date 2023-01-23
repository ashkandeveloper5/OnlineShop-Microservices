using Account.Core.Entities.BaseEntity;
using Account.Repository.Context;
using Account.Repository.DataTransfer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Repository.DataTransfer.Repository
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly AccountContext _context;
        private DbSet<T> _query;

        public AsyncRepository(AccountContext Context)
        {
            _context = Context;
            _query = _context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _query.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _query.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _query.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
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
