using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.DTOs;
using SharedLibrary.DTOs;

namespace AuthServer.Core.Services
{
    public interface IUserService
    {
        Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<ResponseDto<UserAppDto>> GetUserByName(string userName);
    }
}
