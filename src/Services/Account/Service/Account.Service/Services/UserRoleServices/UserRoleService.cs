using Account.Core.Entities.Access;
using Account.Core.Entities.User;
using Account.Repository.DataTransfer.Interfaces;
using Account.Service.Interfaces.RoleInterfaces;
using Account.Service.Interfaces.UserRoleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.Services.UserRoleServices
{
    public class UserRoleService:IUserRoleService
    {
        private readonly IAsyncRepository<User> _asyncUserRepository;
        private readonly IAsyncRepository<Role> _asyncRoleRepository;
        private readonly IAsyncRepository<UserRole> _asyncUserRoleRepository;
        public UserRoleService(IAsyncRepository<User> asyncUserRepository, IAsyncRepository<Role> asyncRoleRepository, IAsyncRepository<UserRole> asyncUserRoleRepository)
        {
            _asyncUserRepository = asyncUserRepository;
            _asyncRoleRepository = asyncRoleRepository;
            _asyncUserRoleRepository = asyncUserRoleRepository;
        }

        public async Task<UserRole> AddAsync(UserRole entity)
        {
            return await _asyncUserRoleRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(UserRole entity)
        {
            await _asyncUserRoleRepository.DeleteAsync(entity);
        }

        public void Dispose()
        {
            _asyncRoleRepository?.Dispose();
            _asyncUserRepository?.Dispose();
            _asyncUserRoleRepository?.Dispose();
        }

        public async Task<IReadOnlyList<UserRole>> GetAllAsync()
        {
            return await _asyncUserRoleRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<UserRole>> GetAsync(Expression<Func<UserRole, bool>> predicate)
        {
            return await _asyncUserRoleRepository.GetAsync(predicate);
        }

        public async Task<UserRole> GetByIdAsync(string id)
        {
            return await _asyncUserRoleRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UserRole entity)
        {
            await _asyncUserRoleRepository.UpdateAsync(entity);
        }

        public async Task<IReadOnlyList<Role>> GetUserRolesAsync(string userEmail)
        {
            User user = _asyncUserRepository.GetAsync(u => u.Email == userEmail).Result[0];
            List<Role> roles = _asyncRoleRepository.GetAllAsync().Result.ToList();
            List<UserRole> userRoles = _asyncUserRoleRepository.GetAllAsync().Result.ToList();
            List<string> roless = userRoles.ToList().Where(u => u.UserId == user.Id).Select(r => r.Id).ToList();
            foreach (var item in roles)
            {
                foreach (var item2 in roless)
                {
                    if (item.Id != item2) roles.Remove(item);
                }
            }
            return roles;
        }
    }
}
