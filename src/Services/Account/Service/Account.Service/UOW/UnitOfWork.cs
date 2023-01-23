using Account.Core.Entities.Access;
using Account.Core.Entities.User;
using Account.Repository.Context;
using Account.Repository.DataTransfer.Repository;
using Account.Service.Interfaces.RoleInterfaces;
using Account.Service.Interfaces.UserInterfaces;
using Account.Service.Interfaces.UserRoleInterfaces;
using Account.Service.Services.RoleServices;
using Account.Service.Services.UserRoleServices;
using Account.Service.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountContext _context;
        public UnitOfWork(AccountContext context)
        {
            _context = context;
        }

        //User Service
        #region UserService
        private IUserService _userService;
        public IUserService UserService
        {
            get
            {
                if (_userService == null)
                {
                    _userService = new UserService(new AsyncRepository<User>(_context));
                }
                return _userService;
            }
        }
        #endregion

        //Role Service
        #region RoleService
        private IRoleService _roleService;
        public IRoleService RoleService
        {
            get
            {
                if (_roleService == null)
                {
                    _roleService = new RoleService(new AsyncRepository<User>(_context), new AsyncRepository<Role>(_context), new AsyncRepository<UserRole>(_context));
                }
                return _roleService;
            }
        }
        #endregion

        //User Role Service
        #region UserRoleService
        private IUserRoleService _userRoleService;
        public IUserRoleService UserRoleService
        {
            get
            {
                if (_userRoleService == null)
                {
                    _userRoleService = new UserRoleService(new AsyncRepository<User>(_context), new AsyncRepository<Role>(_context), new AsyncRepository<UserRole>(_context));
                }
                return _userRoleService;
            }
        }
        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
