using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.DTOs
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string? CreateDate { get; set; }
        public string ProductName { get; set; }
        public long Count { get; set; }
        public decimal Price { get; set; }
        public string FirstDescription { get; set; }
        public string? SecondDescription { get; set; }
        public string? ThirdDescription { get; set; }
        public string Title { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsActive { get; set; }
        public string? UserId { get; set; }
    }
}
