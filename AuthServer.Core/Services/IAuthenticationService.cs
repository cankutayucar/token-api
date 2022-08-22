using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.DTOs;
using SharedLibrary.DTOs;

namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto login);
        Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<ResponseDto<NoContentDto>> RevokeRefreshTokenAsync(string refreshToken);
        ResponseDto<ClientTokenDto> CreateTokenByClientAsync(ClientLoginDto clientLoginDto);
    }
}
