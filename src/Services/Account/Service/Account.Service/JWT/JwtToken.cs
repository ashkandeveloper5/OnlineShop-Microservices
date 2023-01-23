using Account.Core.Entities.User;
using Account.Service.DTOs.UserDto;
using Account.Service.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Account.Core.Common.PasswordEncoder;

namespace Account.Service.JWT
{
    public class JwtToken : IJwtToken
    {
        private readonly IUnitOfWork _unitOfWork;
        public JwtToken(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<string> GenerateToken(LoginUserDto loginUserDto)
        {
            User user = _unitOfWork.UserService.GetAsync(loginUserDto.Email).Result[0];
            var roles = _unitOfWork.RoleService.GetUserRolesAsync(user.Email).Result;
            if (user != null && user.Email == loginUserDto.Email && user.Password == PasswordEncoder.EncodePasswordMd5(loginUserDto.Password))
            {
                #region Claims
                var authClaims = new List<System.Security.Claims.Claim>
                {
                     new System.Security.Claims.Claim(ClaimTypes.NameIdentifier,user.Id),
                     new System.Security.Claims.Claim(ClaimTypes.Name,user.Email),
                };
                foreach (var userRole in roles)
                {
                    authClaims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, userRole.RoleTitle));
                }
                #endregion
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AccountApiAccountApiAccountApiAccountApi"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567891011121"));
                var encryptionCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes128KW,
                    SecurityAlgorithms.Aes128CbcHmacSha256);


                var descriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Audience = "Account",
                    Issuer = "AccountServer",
                    IssuedAt = DateTime.Now,
                    Expires = DateTime.Now.AddDays(30),
                    NotBefore = DateTime.Now,
                    SigningCredentials = signingCredentials,
                    EncryptingCredentials = encryptionCredentials,
                    CompressionAlgorithm = CompressionAlgorithms.Deflate,
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(descriptor);

                var tokenResult = tokenHandler.WriteToken(securityToken);


                return _unitOfWork.UserService.ChangeToken(user.Id, tokenResult).Result ? Task.FromResult(tokenResult) : null;
            }
            return null;
        }
    }
}
