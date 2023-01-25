using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Core.DTOs;
using Order.Core.Interfaces;
using Order.Core.UOW;
using Order.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Order.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<OrderController>
        [HttpPost("CheckoutOrder")]
        public async Task<ActionResult<GetOrderForCheckout>> CheckoutOrder([FromBody] GetOrderForCheckout getOrderForCheckout)
        {
            var result = _unitOfWork.CheckoutService.AddCheckout(getOrderForCheckout);
            return Ok(result);
        }

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
    }
}
