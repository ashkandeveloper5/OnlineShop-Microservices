using Account.Core.Entities.BaseEntity;
using Account.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Entities.Access
{
    public class UserRole:BaseEntity.BaseEntity
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        #region RelationShip
        [ForeignKey(nameof(UserId))]
        public User.User User{ get; set; }
        [ForeignKey(nameof(RoleId))]
        public Access.Role Role{ get; set; }
        #endregion
    }
}
