using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;

namespace CustomerRegistration.Core.Services.AuthServices
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);
    }
}
