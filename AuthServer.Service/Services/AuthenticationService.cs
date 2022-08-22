using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.Configurations;
using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.DTOs;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRereshTokenRepository;

        public AuthenticationService(ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRereshTokenRepository, IOptions<List<Client>> options)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRereshTokenRepository = userRereshTokenRepository;
            _clients = options.Value;
        }

        // first create token for user 
        public async Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto login)
        {
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return ResponseDto<TokenDto>.Fail("Email or password is wrong", 401, true);
            }
            if (!await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return ResponseDto<TokenDto>.Fail("Email or password is wrong", 401, true);
            }
            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRereshTokenRepository.Where(rt => rt.UserId == user.Id)
                .SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                await _userRereshTokenRepository.AddAsync(new UserRefreshToken
                {
                    UserId = user.Id,
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
                _userRereshTokenRepository.Update(userRefreshToken);
            }
            await _unitOfWork.CommitAsync();
            return ResponseDto<TokenDto>.Success(token, 201);
        }

        public async Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var checkRefreshToken =
                await _userRereshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (checkRefreshToken == null)
            {
                return ResponseDto<TokenDto>.Fail("Refresh token not found", 404, true);
            }
            var user = await _userManager.FindByIdAsync(checkRefreshToken.UserId);
            if (user == null)
            {
                return ResponseDto<TokenDto>.Fail("user id not found", 404, true);
            }
            var tokenDto = _tokenService.CreateToken(user);
            checkRefreshToken.Code = tokenDto.RefreshToken;
            checkRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            _userRereshTokenRepository.Update(checkRefreshToken);
            await _unitOfWork.CommitAsync();
            return ResponseDto<TokenDto>.Success(tokenDto, 201);
        }

        public async Task<ResponseDto<NoContentDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var checkRefreshToken = await _userRereshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (checkRefreshToken == null)
            {
                return ResponseDto<NoContentDto>.Fail("Refresh token not found", 404, true);
            }
            _userRereshTokenRepository.Delete(checkRefreshToken);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(204);
        }

        // first create token for client 
        public ResponseDto<ClientTokenDto> CreateTokenByClientAsync(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x =>
                x.ClientId == clientLoginDto.ClientId && x.ClientSecret == clientLoginDto.ClientSecret);
            if (client == null)
            {
                return ResponseDto<ClientTokenDto>.Fail("clientId or clientSecret not found", 404, true);
            }
            var token = _tokenService.CreateTokenByClient(client);
            return ResponseDto<ClientTokenDto>.Success(token, 201);
        }
    }
}
