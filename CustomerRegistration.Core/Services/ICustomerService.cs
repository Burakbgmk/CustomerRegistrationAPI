using CustomerRegistration.Core.DTOs;
using SharedLibrary.DTOs;

namespace CustomerRegistration.Core.Services
{
    public interface ICustomerService<TEntity,TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Response<IEnumerable<CustomerDto>>> GetCustomersWithSamePhoneNumber();
    }
}
