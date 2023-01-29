using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
    }
}
