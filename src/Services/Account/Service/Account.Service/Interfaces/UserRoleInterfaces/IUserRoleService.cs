using Account.Core.Entities.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.Interfaces.UserRoleInterfaces
{
    public interface IUserRoleService:IDisposable
    {
        #region UserRole
        Task<IReadOnlyList<UserRole>> GetAllAsync();
        Task<IReadOnlyList<UserRole>> GetAsync(Expression<Func<UserRole, bool>> predicate);
        Task<IReadOnlyList<Role>> GetUserRolesAsync(string userEmail);
        Task<UserRole> GetByIdAsync(string id);
        Task<UserRole> AddAsync(UserRole entity);
        Task UpdateAsync(UserRole entity);
        Task DeleteAsync(UserRole entity);
        #endregion
    }
}
