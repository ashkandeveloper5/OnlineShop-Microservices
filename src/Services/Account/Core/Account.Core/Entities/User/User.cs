using Account.Core.Entities.Access;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Entities.User
{
    public class User : BaseEntity.BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Token { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? BirthDay { get; set; }

        #region RelationShip
        public IList<UserRole> UserRoles { get; set; }
        #endregion
    }
}
