using Account.Core.Common.Enums;
using Account.Core.Entities.User;
using Account.Service.DTOs.UserDto;
using Account.Service.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController,Authorize(Roles =Roles.User)]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IUnitOfWork unitOfWork, ILogger<AccountController> logger,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<AccountController>
        [HttpGet("Get/{userId}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(string userId)
        {
            return Ok(await _unitOfWork.UserService.GetByIdAsync(userId));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<IEnumerable<User>>> UpdateUser([FromBody]UpdateUserInformation updateUserInformation)
        {
            return Ok(_unitOfWork.UserService.UpdateAsync(_mapper.Map<User>(updateUserInformation)));
        }

        [HttpDelete("DeleteAccount/{userId}")]
        public async Task<ActionResult<IEnumerable<User>>> DeleteAccount(string userId)
        {
            return Ok(_unitOfWork.UserService.DeleteAsync(_unitOfWork.UserService.GetAsync(u => u.Id == userId).Result[0]));
        }
    }
}
