using Account.Api.GrpcServices;
using Account.Core.Common.Enums;
using Account.Core.Entities.User;
using Account.Service.DTOs.OrderDtos;
using Account.Service.DTOs.UserDto;
using Account.Service.UOW;
using AutoMapper;
using Basket.Application.DTOs.BasketDtos;
using Basket.Core.Entities.Order;
using EventBus.Message.Events.CheckoutEvents;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;


namespace Account.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize(Roles = Roles.User)]
    public class AccountController : ControllerBase
    {
        #region Constractor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductGrpcService _productGrpcService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint, ILogger<AccountController> logger, IMapper mapper, ProductGrpcService productGrpcService, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _productGrpcService = productGrpcService;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Account
        [HttpGet("GetUser/{userId}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(string userId)
        {
            return Ok(await _unitOfWork.UserService.GetByIdAsync(userId));
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<IEnumerable<User>>> UpdateUser([FromBody] UpdateUserInformation updateUserInformation)
        {
            return Ok(_unitOfWork.UserService.UpdateAsync(_mapper.Map<Core.Entities.User.User>(updateUserInformation)));
        }

        [HttpDelete("DeleteAccount/{userId}")]
        public async Task<ActionResult<IEnumerable<User>>> DeleteAccount(string userId)
        {
            return Ok(_unitOfWork.UserService.DeleteAsync(_unitOfWork.UserService.GetAsync(u => u.Id == userId).Result[0]));
        }
        #endregion

        #region Prodcut
        [HttpGet("GetProducts")]
        [Authorize(Roles =Roles.Seller)]
        public async Task<ActionResult<IEnumerable<User>>> GetProducts()
        {
                var result = await _productGrpcService.GetProductsUserAsync(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                return Ok(result);
        }

        [HttpGet("GetProductById/{productId}")]
        [Authorize(Roles =Roles.Seller)]
        public async Task<ActionResult<IEnumerable<User>>> GetProductById(string productId)
        {
            var result = await _productGrpcService.GetProductByIdAsync(productId);
            return Ok(result);
        }
        #endregion

        #region Checkout
        [HttpPost("BasketCheckout")]
        public async Task<ActionResult> BasketCheckout([FromBody] BasketCheckoutEvent basketCheckoutEvent)
        {
            await _publishEndpoint.Publish(basketCheckoutEvent);
            return Ok();
        }
        #endregion

        #region Basket
        [HttpGet("GetOrderForCheckout/{orderId}")]
        public async Task<ActionResult> GetOrderForCheckout(string orderId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5001/api/v1/Basket/GetBasketForFinalRegistration/" + orderId);
            response.EnsureSuccessStatusCode();
            return Ok(response.Content.ReadAsStringAsync().Result);
        }

        [HttpGet("GetActiveOrder")]
        public async Task<ActionResult<GetOrderDto>> GetActiveOrder()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5001/api/v1/Basket/GetActiveOrder/" + User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            response.EnsureSuccessStatusCode();
            return Ok(response.Content.ReadAsStringAsync().Result);
        }
        #endregion
    }
}
