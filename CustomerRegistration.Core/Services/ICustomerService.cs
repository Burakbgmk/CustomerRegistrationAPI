using CustomerRegistration.Core.DTOs;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DTOs;

namespace CustomerRegistration.Core.Services
{
    public interface ICustomerService<TEntity,TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Response<IEnumerable<CustomerDto>>> GetCustomersWithSamePhoneNumber();
        Task<Response<TDto>> UploadCustomerPhoto(int id, IFormFile imageFile);
    }
}
