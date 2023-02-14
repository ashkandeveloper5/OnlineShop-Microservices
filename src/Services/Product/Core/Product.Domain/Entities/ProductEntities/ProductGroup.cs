using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities.ProductEntities
{
    public class ProductGroup:BaseEntity
    {
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupTitle { get; set; }

        [Display(Name = "گروه اصلی")]
        public string? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public List<ProductGroup> ProductGroups { get; set; }

        [InverseProperty("Groups")]
        public List<ProductEntity> Groups { get; set; }

        [InverseProperty("SubGroups")]
        public List<ProductEntity> SubGroups { get; set; }
    }
}