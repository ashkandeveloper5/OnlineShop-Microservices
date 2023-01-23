using Account.Service.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.JWT
{
    public interface IJwtToken
    {
        Task<string> GenerateToken(LoginUserDto loginUserDto);
    }
}
