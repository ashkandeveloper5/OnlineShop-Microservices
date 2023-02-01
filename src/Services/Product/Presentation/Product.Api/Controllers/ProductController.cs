using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.UOW;
using Product.Common.Access;
using Product.Common.DTOs.ProductDtos;
using Product.Domain.Entities.ProductEntities;


namespace Product.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class ProductController : ControllerBase
    {
        #region Constractor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GetProduct
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _unitOfWork.ProductService.GetAllAsync();
            var response = new List<ProductDto>();
            foreach (var item in products)
            {
                response.Add(_mapper.Map<ProductDto>(item));
            }
            return Ok(response);
        }

        [HttpGet("GetProduct/{productId}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string productId)
        {
            var product = _unitOfWork.ProductService.GetAsync(product => product.Id == productId).Result[0];
            return Ok(_mapper.Map<ProductDto>(product));
        }
        #endregion

        #region CreateProduct
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ProductEntity>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var result = await _unitOfWork.ProductService.AddAsync(_mapper.Map<ProductEntity>(createProductDto));
            return Ok(result);
        }
        #endregion

        #region UpdateProduct
        [HttpPut("UpdateProduct/{productId}")]
        public async Task<ActionResult<ProductEntity>> UpdateProduct(string productId, [FromBody] UpdateProductDto updateProductDto)
        {
            return Ok(_unitOfWork.ProductService.UpdateAsync(_mapper.Map<ProductEntity>(updateProductDto)));
        }
        #endregion

        #region DeleteProduct
        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<ActionResult> DeleteProduct(string productId)
        {
            ProductEntity productEntity = _unitOfWork.ProductService.GetAsync(product => product.Id == productId).Result[0];
            return Ok(_unitOfWork.ProductService.DeleteAsync(productEntity));
        }

        [HttpDelete("DeleteProduct/{productId}/{count}")]
        public async Task<ActionResult> ProductPurchase(string productId, long count)
        {
            ProductEntity productEntity = _unitOfWork.ProductService.GetAsync(p => p.Id == productId).Result[0];
            productEntity.Count -= count;
            await _unitOfWork.ProductService.UpdateAsync(productEntity);
            return Ok();
        }
        #endregion
    }
}
