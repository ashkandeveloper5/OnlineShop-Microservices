using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Persistence.Context
{
    public class ProductContextSeed
    {
        public static async Task SeedAsync(ProductContext productContext, ILogger<ProductContextSeed> logger)
        {
            if (!await productContext.Products.AnyAsync())
            {
                await productContext.Products.AddRangeAsync(GetPreconfiguredProducts());
                await productContext.ProductGroups.AddRangeAsync(GetPreconfiguredProductGroups());
                await productContext.ProductComments.AddRangeAsync(GetPreconfiguredProductComments());
                await productContext.SaveChangesAsync();
                logger.LogInformation("data seed section configured");
            }
        }

        #region Seed Data Methods
        public static IEnumerable<ProductGroup> GetPreconfiguredProductGroups()
        {
            return new List<ProductGroup>
            {
                new ProductGroup{CreateDate=DateTime.Now,GroupTitle="Mobile",Id="1",IsActive=true,IsDelete=false,ModifiedDate=DateTime.Now,},
                new ProductGroup{CreateDate=DateTime.Now,GroupTitle="Samsung",Id="2",IsActive=true,IsDelete=false,ModifiedDate=DateTime.Now,ParentId="1",},
            };
        }
        public static IEnumerable<ProductEntity> GetPreconfiguredProducts()
        {
            return new List<ProductEntity>
            {
                new ProductEntity {Count=100,GroupId="1",SubGroup="2",ProductImage="1.jpg",CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A12",Id="1",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A12",SecondDescription="This Is ThirdDescription A12",ThirdDescription="This Is ThirdDescription A12",Title="A12",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,GroupId="1",SubGroup="2",ProductImage="2.jpg",CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A13",Id="2",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A13",SecondDescription="This Is ThirdDescription A13",ThirdDescription="This Is ThirdDescription A12",Title="A13",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,GroupId="1",SubGroup="2",ProductImage="3.jpg",CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A14",Id="3",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A14",SecondDescription="This Is ThirdDescription A14",ThirdDescription="This Is ThirdDescription A12",Title="A14",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,GroupId="1",SubGroup="2",ProductImage="4.jpg",CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A15",Id="4",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A15",SecondDescription="This Is ThirdDescription A15",ThirdDescription="This Is ThirdDescription A12",Title="A15",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,GroupId="1",SubGroup="2",ProductImage="5.jpg",CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A16",Id="5",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A16",SecondDescription="This Is ThirdDescription A16",ThirdDescription="This Is ThirdDescription A12",Title="A16",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,GroupId="1",SubGroup="2",ProductImage="6.jpg",CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A17",Id="6",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A17",SecondDescription="This Is ThirdDescription A17",ThirdDescription="This Is ThirdDescription A12",Title="A17",UserId="123451234512345@ExampleId"},
            };
        }
        public static IEnumerable<ProductComment> GetPreconfiguredProductComments()
        {
            return new List<ProductComment>
            {
                new ProductComment{Id="1",CreateDate=DateTime.Now,ModifiedDate=DateTime.Now,Comment="This is comment",IsActive=true,IsDelete=false,ProductId="1",UserId="123451234512345@ExampleId"},
            };
        }
        #endregion
    }
}
