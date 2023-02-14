using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities.ProductEntities
{
    public class ProductComment:BaseEntity
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
    }
}
