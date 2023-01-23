using Account.Core.Entities.Access;
using Account.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.Interfaces.RoleInterfaces
{
    public interface IRoleService:IDisposable
    {
        #region Role
        Task<IReadOnlyList<Role>> GetAllAsync();
        Task<IReadOnlyList<Role>> GetAsync(Expression<Func<Role, bool>> predicate);
        Task<IReadOnlyList<Role>> GetUserRolesAsync(string userEmail);
        Task<Role> GetByIdAsync(string id);
        Task<Role> AddAsync(Role entity);
        Task UpdateAsync(Role entity);
        Task DeleteAsync(Role entity);
        #endregion
    }
}
