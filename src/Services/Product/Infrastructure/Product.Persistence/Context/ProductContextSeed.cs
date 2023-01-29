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
                await productContext.SaveChangesAsync();
                logger.LogInformation("data seed section configured");
            }
        }

        #region Seed Data Methods
        public static IEnumerable<ProductEntity> GetPreconfiguredProducts()
        {
            return new List<ProductEntity>
            {
                new ProductEntity {Count=100,CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A12",Id="1",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A12",SecondDescription="This Is ThirdDescription A12",ThirdDescription="This Is ThirdDescription A12",Title="A12",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A13",Id="2",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A13",SecondDescription="This Is ThirdDescription A13",ThirdDescription="This Is ThirdDescription A12",Title="A13",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A14",Id="3",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A14",SecondDescription="This Is ThirdDescription A14",ThirdDescription="This Is ThirdDescription A12",Title="A14",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A15",Id="4",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A15",SecondDescription="This Is ThirdDescription A15",ThirdDescription="This Is ThirdDescription A12",Title="A15",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A16",Id="5",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A16",SecondDescription="This Is ThirdDescription A16",ThirdDescription="This Is ThirdDescription A12",Title="A16",UserId="123451234512345@ExampleId"},
                new ProductEntity {Count=100,CreateDate=DateTime.Now,FirstDescription="This Is ThirdDescription A17",Id="6",IsActive=true,IsDelete=false, ModifiedDate =DateTime.Now,Price=500000,ProductName="A17",SecondDescription="This Is ThirdDescription A17",ThirdDescription="This Is ThirdDescription A12",Title="A17",UserId="123451234512345@ExampleId"},
            };
        }
        #endregion
    }
}
