using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Entities.Access
{
    public class Role:BaseEntity.BaseEntity
    {
        public string RoleTitle { get; set; }

        #region RelationShip
        public IList<UserRole> UserRoles { get; set; }
        #endregion
    }
}
