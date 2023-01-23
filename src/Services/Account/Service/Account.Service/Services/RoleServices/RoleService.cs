using Account.Core.Entities.Access;
using Account.Core.Entities.User;
using Account.Repository.DataTransfer.Interfaces;
using Account.Service.Interfaces.RoleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.Services.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly IAsyncRepository<User> _asyncUserRepository;
        private readonly IAsyncRepository<Role> _asyncRoleRepository;
        private readonly IAsyncRepository<UserRole> _asyncUserRoleRepository;
        public RoleService(IAsyncRepository<User> asyncUserRepository, IAsyncRepository<Role> asyncRoleRepository, IAsyncRepository<UserRole> asyncUserRoleRepository)
        {
            _asyncUserRepository = asyncUserRepository;
            _asyncRoleRepository = asyncRoleRepository;
            _asyncUserRoleRepository = asyncUserRoleRepository;
        }

        public async Task<Role> AddAsync(Role entity)
        {
            return await _asyncRoleRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(Role entity)
        {
            await _asyncRoleRepository.DeleteAsync(entity);
        }

        public void Dispose()
        {
            _asyncRoleRepository?.Dispose();
            _asyncUserRepository?.Dispose();
            _asyncUserRoleRepository?.Dispose();
        }

        public async Task<IReadOnlyList<Role>> GetAllAsync()
        {
            return await _asyncRoleRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<Role>> GetAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _asyncRoleRepository.GetAsync(predicate);
        }

        public async Task<Role> GetByIdAsync(string id)
        {
            return await _asyncRoleRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Role entity)
        {
            await _asyncRoleRepository.UpdateAsync(entity);
        }

        public async Task<IReadOnlyList<Role>> GetUserRolesAsync(string userEmail)
        {
            var result=new List<Role>();
            User user = _asyncUserRepository.GetAsync(u => u.Email == userEmail).Result[0];
            List<Role> roles = _asyncRoleRepository.GetAllAsync().Result.ToList();
            List<UserRole> userRoles = _asyncUserRoleRepository.GetAllAsync().Result.ToList();
            List<string> roless=userRoles.ToList().Where(u => u.UserId == user.Id).Select(r=>r.RoleId).ToList();
            for (int i = 0; i < roles.Count; i++)
            {
                for (int j = 0; j < roles.Count; j++)
                {
                    if (roles[i].Id == roless[j]) result.Add(roles[i]);
                }
            }
            return result;
        }
    }
}
