using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Access;
using Order.Core.DTOs;
using Order.Core.Interfaces;
using Order.Core.UOW;
using Order.Domain.Entities;


namespace Order.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles =$"{Roles.User}")]
    public class OrderController : ControllerBase
    {
        #region Constractor
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region CheckoutOrder
        [HttpPost("CheckoutOrder")]
        public async Task<ActionResult<GetOrderForCheckout>> CheckoutOrder([FromBody] GetOrderForCheckout getOrderForCheckout)
        {
            var result = _unitOfWork.CheckoutService.AddCheckout(getOrderForCheckout);
            return Ok(result);
        }
        #endregion

        #region GetOrder
        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<GetOrderForCheckout>>> GetAllOrders()
        {
            var result = _unitOfWork.CheckoutService.GetAllCheckout();
            return Ok(result);
        }

        [HttpGet("GetCheckoutOrderById/{orderId}")]
        public async Task<ActionResult<GetOrderForCheckout>> GetCheckoutOrderById(string orderId)
        {
            var result = _unitOfWork.CheckoutService.GetCheckoutById(orderId);
            return Ok(result);
        }
        #endregion

    }
}
