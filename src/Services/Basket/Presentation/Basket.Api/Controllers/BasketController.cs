using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Basket.Application.DTOs.BasketDtos;
using Basket.Application.UOW;
using AutoMapper;
using Basket.Core.Entities.Order;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BasketController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("AddToBasket/{userId}")]
        public async Task<ActionResult> AddToBasket([FromBody] AddToBasketDto addToBasketDto, string userId)
        {
            var result = await _unitOfWork.BasketService.AddAsync(_mapper.Map<OrderDetail>(addToBasketDto), userId);
            return Ok();
        }

        [HttpGet("DeleteOrderDetail/{orderDetailId}")]
        public async Task<ActionResult> DeleteOrderDetail(string orderDetailId)
        {
            await _unitOfWork.BasketService.DeleteAsync(_unitOfWork.BasketService.GetByIdAsync(orderDetailId).Result);
            return Ok();
        }
    }
}
