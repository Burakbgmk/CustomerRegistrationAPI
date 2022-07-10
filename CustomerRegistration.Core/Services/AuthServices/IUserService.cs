using CustomerRegistration.Core.DTOs;
using SharedLibrary.DTOs;

namespace CustomerRegistration.Core.Services.AuthServices
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
    }
}
