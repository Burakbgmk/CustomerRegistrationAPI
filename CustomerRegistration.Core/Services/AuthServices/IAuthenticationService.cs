using CustomerRegistration.Core.DTOs;
using SharedLibrary.DTOs;

namespace CustomerRegistration.Core.Services.AuthServices
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);
    }
}
