using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
