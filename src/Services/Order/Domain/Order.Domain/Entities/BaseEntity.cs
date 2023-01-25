using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
