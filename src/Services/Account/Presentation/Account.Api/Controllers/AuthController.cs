using Account.Core.Common.PasswordEncoder;
using Account.Core.Entities.User;
using Account.Service.DTOs.UserDto;
using Account.Service.JWT;
using Account.Service.UOW;
using Account.Service.Utilities.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Account.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Constractor
        private readonly IJwtToken _jwtToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IJwtToken jwtToken, IUnitOfWork unitOfWork, ILogger<AuthController> logger, IMapper mapper)
        {
            _jwtToken = jwtToken;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                await _unitOfWork.UserService.AddAsync(new User
                {
                    Email = registerUserDto.Email,
                    Password = PasswordEncoder.EncodePasswordMd5(registerUserDto.Password),
                    UserName = registerUserDto.UserName,
                });
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserDto loginUserDto)
        {
            var newToken = _jwtToken.GenerateToken(loginUserDto).Result;
            return Ok(newToken);
        }
        #endregion

        #region Logout
        [HttpPost("Logout"), Authorize(Roles = Roles.NormalUser)]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
        #endregion
    }
}
