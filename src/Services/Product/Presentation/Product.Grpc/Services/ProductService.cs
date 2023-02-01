using Grpc.Core;
using Product.Application.Interfaces.ProductInterfaces;
using Product.Application.UOW;
using Product.Grpc.Protos;
using Google.Protobuf.Collections;
using Product.Domain.Entities.ProductEntities;
using AutoMapper;

namespace Product.Grpc.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public override async Task<GetAllProductsListResponse> GetUserProducts(GetAllProductsListRequest request, ServerCallContext context)
        {
            var products = _unitOfWork.ProductService.GetAsync(p => p.UserId == request.UserId).Result;
            if (products == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product With User Id {request.UserId} Is Not Found"));
            }
            var result = new GetAllProductsListResponse();
            
            foreach (var item in products)
            {
                result.Items.Add(_mapper.Map<GetAllProductsResponse>(item));
            }
            //foreach (var item in products)
            //{
            //    result.Items.Add(_mapper.Map<GetAllProductsResponse>(item));
            //    result.Items.Add(new GetAllProductsResponse
            //    {
            //        Id = item.Id,
            //        Count = item.Count,
            //        FirstDescription = item.FirstDescription,
            //        Price = long.Parse(item.Price.ToString()),
            //        ProductName = item.ProductName,
            //        SecondDescription = item.SecondDescription,
            //        ThirdDescription = item.ThirdDescription,
            //        Title = item.Title,
            //    });
            //}
            return result;
        }

        public override async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            var getProduct = await _unitOfWork.ProductService.GetByIdAsync(request.ProductId);
            return new GetProductByIdResponse
            {
                Id = getProduct.Id,
                Count = getProduct.Count,
                FirstDescription = getProduct.FirstDescription,
                Price = (long)getProduct.Price,
                ProductName = getProduct.ProductName,
                SecondDescription = getProduct.SecondDescription,
                ThirdDescription = getProduct.ThirdDescription,
                Title = getProduct.Title,
            };
        }

        public override async Task<ProductPurchaseMessageResponse> ProductPurchase(ProductPurchaseMessageRequest request, ServerCallContext context)
        {
            ProductEntity productEntity = _unitOfWork.ProductService.GetAsync(p => p.Id == request.ProductId).Result[0];
            if(productEntity.Count>=1)
            {
                productEntity.Count -= request.Count;
                await _unitOfWork.ProductService.UpdateAsync(productEntity);
                return new ProductPurchaseMessageResponse()
                {
                    Count = productEntity.Count,
                    StatusCode = 200,
                };
            }
            else
            {
                return new ProductPurchaseMessageResponse()
                {
                    Count = productEntity.Count,
                    StatusCode = 400,
                };
            }
        }

        public override async Task<AddToProductExistResponse> AddToProductExist(AddToProductExistRequest request, ServerCallContext context)
        {
            ProductEntity productEntity = _unitOfWork.ProductService.GetAsync(p => p.Id == request.ProductId).Result[0];
            productEntity.Count += request.Count;
            await _unitOfWork.ProductService.UpdateAsync(productEntity);
            return new AddToProductExistResponse()
            {
                Count = productEntity.Count,
            };
        }
    }
}
