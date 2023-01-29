using Account.Api.GrpcServices;
using Account.Core.Common.Enums;
using Account.Core.Entities.User;
using Account.Service.DTOs.OrderDtos;
using Account.Service.DTOs.UserDto;
using Account.Service.UOW;
using AutoMapper;
using EventBus.Message.Events.CheckoutEvents;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpPut("Update")]
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
        public async Task<ActionResult<IEnumerable<User>>> GetProducts()
        {
            var result = await _productGrpcService.GetProductsUserAsync(User.Identity.Name);
            return Ok(result);
        }

        [HttpGet("GetProductById/{productId}")]
        public async Task<ActionResult<IEnumerable<User>>> GetProductById(string productId)
        {
            var result = await _productGrpcService.GetProductsUserAsync(productId);
            return Ok(result);
        }
        #endregion
        #region Checkout
        [HttpGet("GetOrderForCheckout/{orderId}")]
        public async Task<ActionResult> GetOrderForCheckout(string orderId)
        {
            //var request = new HttpRequestMessage(HttpMethod.Get,"http://localhost:5001/api/v1/Basket/GetBasketForFinalRegistration/");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5001/api/v1/Basket/GetBasketForFinalRegistration/"+orderId);
            response.EnsureSuccessStatusCode();
            //return Ok(JsonSerializer.Deserialize<Order>(response.Content.ReadAsStringAsync()));
            return Ok(response.Content.ReadAsStringAsync().Result);
        }

        [HttpPost("BasketCheckout")]
        public async Task<ActionResult> BasketCheckout([FromBody] BasketCheckoutEvent basketCheckoutEvent)
        {
            await _publishEndpoint.Publish(basketCheckoutEvent);
            return Ok();
        }
        #endregion
        #region Basket

        #endregion
    }
}
