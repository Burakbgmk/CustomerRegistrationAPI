using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Core.Repositories;
using CustomerRegistration.Core.Services;
using CustomerRegistration.Core.UnitOfWork;
using CustomerRegistration.Service.Mappers;
using SharedLibrary.DTOs;

namespace CustomerRegistration.Service.Services
{
    public class CustomerService : GenericService<Customer, CustomerDto>, ICustomerService<Customer, CustomerDto>
    {
        private readonly ICustomerRepository<Customer, CustomerDto> _customerRepository;
        public CustomerService(ICustomerRepository<Customer, CustomerDto> customerRepository, IUnitOfWork unitOfWork) : base(customerRepository, unitOfWork)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Response<IEnumerable<CustomerDto>>> GetCustomersWithSamePhoneNumber()
        {
            var customers = await _customerRepository.GetRepeated();
            if (customers.Count() < 2)
                return Response<IEnumerable<CustomerDto>>.Fail("There is no customers with same number!",404,true);
            var response = ObjectMapper.Mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Response<IEnumerable<CustomerDto>>.Success(response, 200);
        }
    }
}
