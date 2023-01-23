using Account.Core.Entities.User;
using Account.Repository.DataTransfer.Interfaces;
using Account.Service.Interfaces.UserInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.Services.UserServices
{
    public class UserService:IUserService
    {
        private readonly IAsyncRepository<User> _asyncRepository;
        public UserService(IAsyncRepository<User> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        public async Task<User> AddAsync(User entity)
        {
            return await _asyncRepository.AddAsync(entity);
        }

        public async Task<bool> ChangeToken(string userId, string newToken)
        {
            try
            {
                _asyncRepository.GetAsync(u => u.Id == userId).Result[0].Token = newToken;
                _asyncRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task DeleteAsync(User entity)
        {
            await _asyncRepository.DeleteAsync(entity);
        }

        public void Dispose()
        {
            _asyncRepository?.Dispose();
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await _asyncRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<User>> GetAsync(Expression<Func<User, bool>> predicate)
        {
            return await _asyncRepository.GetAsync(predicate);
        }

        public async Task<IReadOnlyList<User>> GetAsync(string userEmail)
        {
            return await _asyncRepository.GetAsync(u=>u.Email==userEmail);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _asyncRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(User entity)
        {
            await _asyncRepository.UpdateAsync(entity);
        }
    }
}
