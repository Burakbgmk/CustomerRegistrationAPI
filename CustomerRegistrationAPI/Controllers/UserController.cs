using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var response = await _userService.CreateUserAsync(createUserDto);
            return ActionResultInstance(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser(string userName)
        {
            var response = await _userService.GetUserByNameAsync(userName);
            return ActionResultInstance(response);
        }
    }
}
