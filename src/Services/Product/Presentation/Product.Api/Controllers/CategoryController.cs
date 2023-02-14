using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.Application.UOW;
using Product.Domain.Entities.ProductEntities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Constractor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GetCategory
        [HttpGet("GetAllCategory")]
        public async Task<ActionResult<IEnumerable<ProductGroup>>> GetAllCategory()
        {
            var products = await _unitOfWork.CategoryService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("GetProduct/{categoryId}")]
        public async Task<ActionResult<ProductGroup>> GetCategory(string categoryId)
        {
            var product = _unitOfWork.CategoryService.GetAsync(category => category.Id == categoryId).Result[0];
            return Ok(product);
        }
        #endregion
    }
}
