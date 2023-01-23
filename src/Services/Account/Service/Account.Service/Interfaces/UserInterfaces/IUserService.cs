using Account.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.Interfaces.UserInterfaces
{
    public interface IUserService:IDisposable
    {
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<IReadOnlyList<User>> GetAsync(Expression<Func<User, bool>> predicate);
        Task<IReadOnlyList<User>> GetAsync(string userEmail);
        Task<User> GetByIdAsync(string id);
        Task<User> AddAsync(User entity);
        Task<bool> ChangeToken(string userId,string newToken);
        Task UpdateAsync(User entity);
        Task DeleteAsync(User entity);
    }
}
