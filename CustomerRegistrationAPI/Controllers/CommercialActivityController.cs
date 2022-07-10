using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.API.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommercialActivityController : CustomBaseController
    {
        private readonly ICommercialActivityService<CommercialActivity, CommercialActivityDto> _commercialActivityService;
        public CommercialActivityController(ICommercialActivityService<CommercialActivity, CommercialActivityDto> commercialActivityService)
        {
            _commercialActivityService = commercialActivityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _commercialActivityService.GetAllAsync();
            return ActionResultInstance(response);
        }

        [HttpGet("{page},{pageSize}")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var response = await _commercialActivityService.GetAllAsync(page, pageSize);
            return ActionResultInstance(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _commercialActivityService.GetByIdAsync(id);
            return ActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommercialActivityDto dto)
        {
            var response = await _commercialActivityService.CreateAsync(dto);
            return ActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, CommercialActivityDto dto)
        {
            var response = await _commercialActivityService.Update(id, dto);
            return ActionResultInstance(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _commercialActivityService.RemoveAsync(id);
            return ActionResultInstance(response);
        }
    }
}
