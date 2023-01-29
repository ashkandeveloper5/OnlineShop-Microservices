using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Basket.Application.DTOs.BasketDtos;
using Basket.Application.UOW;
using AutoMapper;
using Basket.Core.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Basket.Core.Entities.Basket;
using System.Text.Json;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BasketController(IUnitOfWork unitOfWork, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var result = await _unitOfWork.BasketService.GetAllOrderAsync();
            return Ok(result);
        }

        [HttpGet("GetAllOrderDetail")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetAllOrderDetails()
        {
            var result = await _unitOfWork.BasketService.GetAllOrderDetailAsync();
            return Ok(result);
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

        [HttpGet("GetBasketForFinalRegistration/{orderId}")]
        public async Task<ActionResult<GetOrderDto>> GetBasketForFinalRegistration(string orderId)
        {
            Order order = _unitOfWork.BasketService.GetOrderAsync(o => o.Id == orderId).Result[0];
            var orderDetails = _unitOfWork.BasketService.GetOrderDetailAsync(o => o.OrderId == order.Id).Result;
            foreach (var item in orderDetails)
            {
                order.TotalPrice += item.Price;
            }
            return Ok(_mapper.Map<GetOrderDto>(order));
        }

        [HttpPost("FinalRegistration/{orderId}")]
        public async Task<ActionResult<Core.Entities.Basket.BasketCheckout>> AddFinalRegistration(string orderId, [FromBody] Core.Entities.Basket.BasketCheckout basketCheckout)
        {
            Order order = _unitOfWork.BasketService.GetOrderAsync(o => o.Id == orderId).Result[0];
            var orderDetails = _unitOfWork.BasketService.GetOrderDetailAsync(o => o.OrderId == order.Id).Result;
            foreach (var item in orderDetails)
            {
                order.TotalPrice += item.Price;
            }
            order.IsFinally = true;
            _unitOfWork.Save();
            var result = _unitOfWork.BasketService.AddCkeckout(basketCheckout).Result;
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5003/api/v1/Order/CheckoutOrder");
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(orderId, JsonSerializer.Serialize(result));
            return Ok(response.Content.ReadAsStringAsync());
        }
    }
}
