using Account.Service.Interfaces.RoleInterfaces;
using Account.Service.Interfaces.UserInterfaces;
using Account.Service.Interfaces.UserRoleInterfaces;
using Account.Service.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IUserService UserService { get; }
        IRoleService RoleService { get; }
        IUserRoleService UserRoleService { get; }
    }
}
