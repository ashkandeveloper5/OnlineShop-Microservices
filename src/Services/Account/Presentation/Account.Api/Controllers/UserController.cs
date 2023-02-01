using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Account.Core.Entities.User;
using Account.Service.DTOs.UserDto;

namespace Account.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{Roles.Admin},{Roles.RolesAdmin}")]
    public class UserController : ControllerBase
    {
        #region Constractor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, ILogger<UserController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region User
        // GET: api/<UserController>
        [HttpGet("GetAllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            return Ok(_unitOfWork.UserService.GetAllAsync().Result);
        }

        // GET api/<UserController>/5
        [HttpGet("GetUserById/{userId}")]
        public async Task<ActionResult<User>> GetUserById(string userId)
        {
            return Ok(_unitOfWork.UserService.GetByIdAsync(userId).Result);
        }

        [HttpPut("UpdateUser/{userId}")]
        public async Task<ActionResult<User>> UpdateUser([FromBody]UpdateUserInformation updateUserInformation,string userId)
        {
            User user = _mapper.Map<User>(updateUserInformation);
            user.Id = userId;
            return Ok(_unitOfWork.UserService.UpdateAsync(user));
        }

        [HttpDelete("DeleteUserById/{userId}")]
        public async Task<ActionResult<User>> DeleteUserById(string userId)
        {
            return Ok(_unitOfWork.UserService.DeleteAsync(_unitOfWork.UserService.GetByIdAsync(userId).Result));
        }
        #endregion

        #region UserRole
        //[HttpGet("GetUserRoles/{userId}")]
        //public async Task<ActionResult<IList<Role>>> GetUserRoles(string userId)
        //{
        //    return _unitOfWork.UserRoleService.GetUserRolesAsync(_unitOfWork.UserService.GetByIdAsync(userId).Result.Email).Result;
        //}
        #endregion
    }
}