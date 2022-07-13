using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Core.Services;
using CustomerRegistration.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.API.Controllers
{
    [Authorize(Roles ="Admin,Editor")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        private readonly ICustomerService<Customer, CustomerDto> _customerService;
        public CustomerController(ICustomerService<Customer, CustomerDto> customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _customerService.GetAllAsync();
            return ActionResultInstance(response);
        }

        [HttpGet("{page},{pageSize}")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var response = await _customerService.GetAllAsync(page, pageSize);
            return ActionResultInstance(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _customerService.GetByIdAsync(id);
            return ActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto dto)
        {
            var response = await _customerService.CreateAsync(dto);
            return ActionResultInstance(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto dto)
        {
            var response = await _customerService.Update(id, dto);
            return ActionResultInstance(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _customerService.RemoveAsync(id);
            return ActionResultInstance(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/[action]")]
        public async Task<IActionResult> CheckSamePhoneNumber()
        {
            var response = await _customerService.GetCustomersWithSamePhoneNumber();
            return ActionResultInstance(response);
        }

        [AllowAnonymous]
        [HttpPost("/[action]")]
        public async Task<IActionResult> UploadImage([FromRoute] int id, IFormFile imageFile)
        {
            var response = await _customerService.UploadCustomerPhoto(id, imageFile);
            return ActionResultInstance(response);
        }
    }
}
