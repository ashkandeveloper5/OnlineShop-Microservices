using Account.Core.Common.Enums;
using Account.Core.Entities.Access;
using Account.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Repository.Context
{
    public class AccountContextSeed
    {
        public static async Task SeedAsync(AccountContext accountContext, ILogger<AccountContextSeed> logger)
        {
            if (!await accountContext.Roles.AnyAsync())
            {
                await accountContext.Roles.AddRangeAsync(GetPreconfiguredRoles());
                await accountContext.Users.AddRangeAsync(GetPreconfiguredUsers());
                await accountContext.UserRoles.AddRangeAsync(GetPreconfiguredUserRoles());
                await accountContext.SaveChangesAsync();
                logger.LogInformation("data seed section configured");
            }
        }

        #region Seed Data Methods
        public static IEnumerable<Role> GetPreconfiguredRoles()
        {
            return new List<Role>
            {
                new Role {Id="123451234512345role1",IsActive=true,RoleTitle=Roles.Admin},
                new Role {Id="123451234512345role2",IsActive=true,RoleTitle=Roles.RolesAdmin},
                new Role {Id="123451234512345role3",IsActive=true,RoleTitle=Roles.UsersAdmin},
                new Role {Id="123451234512345role4",IsActive=true,RoleTitle=Roles.Seller},
                new Role {Id="123451234512345role5",IsActive=true,RoleTitle=Roles.User},
                new Role {Id="123451234512345role6",IsActive=true,RoleTitle=Roles.NormalUser},
            };
        }
        public static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>
            {
                new User {Id="123451234512345@ExampleId",Email="Example@gmail.com",IsActive=true,FirstName="Exaple",UserName="Example",LastName="Example",Password="8C-FA-22-82-B1-7D-E0-A5-98-C0-10-F5-F0-10-9E-7D"},
            };
        }
        public static IEnumerable<UserRole> GetPreconfiguredUserRoles()
        {
            return new List<UserRole>
            {
                new UserRole {Id=Guid.NewGuid().ToString(),UserId="123451234512345@ExampleId",RoleId="123451234512345role1"},
                new UserRole {Id=Guid.NewGuid().ToString(),UserId="123451234512345@ExampleId",RoleId="123451234512345role2"},
                new UserRole {Id=Guid.NewGuid().ToString(),UserId="123451234512345@ExampleId",RoleId="123451234512345role3"},
                new UserRole {Id=Guid.NewGuid().ToString(),UserId="123451234512345@ExampleId",RoleId="123451234512345role4"},
                new UserRole {Id=Guid.NewGuid().ToString(),UserId="123451234512345@ExampleId",RoleId="123451234512345role5"},
                new UserRole {Id=Guid.NewGuid().ToString(),UserId="123451234512345@ExampleId",RoleId="123451234512345role6"},
            };
        }
        #endregion
    }
}
