using Account.Service.DTOs;
using Product.Grpc.Protos;
using System.Threading.Tasks;

namespace Account.Api.GrpcServices
{
    public class ProductGrpcService
    {
        private readonly ProductProtoService.ProductProtoServiceClient _service;

        public ProductGrpcService(ProductProtoService.ProductProtoServiceClient service)
        {
            _service = service;
        }

        public async Task<ProductDto> GetProductByIdAsync(string productId)
        {
            var result = await _service.GetProductByIdAsync(new GetProductByIdRequest { ProductId = productId });
            return new ProductDto()
            {
                Count = result.Count,
                FirstDescription = result.FirstDescription,
                Id = result.Id,
                Price = (decimal)result.Price,
                ProductName = result.ProductName,
                SecondDescription = result.SecondDescription,
                ThirdDescription = result.ThirdDescription,
                Title = result.Title,
            };
        }

        public async Task<IEnumerable<ProductDto>> GetProductsUserAsync(string userId)
        {
            var result = await _service.GetUserProductsAsync(new GetAllProductsListRequest { UserId = userId });
            var products=new List<ProductDto>();
            foreach (var item in result.Items)
            {
                products.Add(new ProductDto
                {
                    Count= item.Count,
                    FirstDescription =item.FirstDescription,
                    Id=item.Id,
                    Price = item.Price,
                    ProductName= item.ProductName,
                    SecondDescription= item.SecondDescription,
                    ThirdDescription=item.ThirdDescription,
                    Title=item.Title,
                });
            }
            return products;
        }

        public async Task<bool> AddToProductExistAsync(string productId,long count)
        {
            var result = await _service.AddToProductExistAsync(new AddToProductExistRequest { ProductId = productId ,Count=count});
            return true;
        }

        public async Task<bool> ProductPurchaseAsync(string productId, long count)
        {
            var result = await _service.ProductPurchaseAsync(new ProductPurchaseMessageRequest { ProductId = productId, Count = count });
            if (result.StatusCode == 400)
            {
                return false;
            }
            return true;
        }
    }
}
