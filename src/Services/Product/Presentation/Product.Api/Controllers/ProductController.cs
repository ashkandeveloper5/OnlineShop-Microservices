using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.UOW;
using Product.Common.Access;
using Product.Common.DTOs.ProductDtos;
using Product.Domain.Entities.ProductEntities;


namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            return Ok(_mapper.Map<ProductDto>(_unitOfWork.ProductService.GetAllAsync()));
        }

        [HttpGet("GetProduct/{productId}")]
        public async Task<ActionResult<ProductDto>> Get(string id)
        {
            return Ok(_mapper.Map<ProductDto>(await _unitOfWork.ProductService.GetAsync(product => product.Id == id)));
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            return Ok(await _unitOfWork.ProductService.AddAsync(_mapper.Map<ProductEntity>(createProductDto)));
        }

        [HttpPut("UpdateProduct/{productId}")]
        public async Task<ActionResult> UpdateProduct(string productId, [FromBody] UpdateProductDto updateProductDto)
        {
            ProductEntity productEntity = _mapper.Map<ProductEntity>(updateProductDto);
            productEntity.Id=productId;
            return Ok(_unitOfWork.ProductService.UpdateAsync(productEntity));
        }

        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<ActionResult> DeleteProduct(string productId)
        {
            ProductEntity productEntity = _unitOfWork.ProductService.GetAsync(product => product.Id == productId).Result[0];
            return Ok(_unitOfWork.ProductService.DeleteAsync(productEntity));
        }

        [HttpDelete("DeleteProduct/{productId}/{count}")]
        public async Task<ActionResult> ProductPurchase(string productId,long count)
        {
            ProductEntity productEntity = _unitOfWork.ProductService.GetAsync(p => p.Id == productId).Result[0];
            productEntity.Count-=count;
            await _unitOfWork.ProductService.UpdateAsync(productEntity);
            return Ok();
        }
    }
}
