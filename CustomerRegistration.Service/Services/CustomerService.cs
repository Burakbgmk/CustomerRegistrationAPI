using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Core.Repositories;
using CustomerRegistration.Core.Services;
using CustomerRegistration.Core.UnitOfWork;
using CustomerRegistration.Service.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SharedLibrary.Configuration;
using SharedLibrary.DTOs;
using SharedLibrary.Events;
using SharedLibrary.Services;

namespace CustomerRegistration.Service.Services
{
    public class CustomerService : GenericService<Customer, CustomerDto>, ICustomerService<Customer, CustomerDto>
    {
        private readonly ICustomerRepository<Customer, CustomerDto> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RabbitMqPublisherService _rabbitMqPublisher;
        private readonly ImageRootFile _imageRootOptions;
        public CustomerService(ICustomerRepository<Customer, CustomerDto> customerRepository, IUnitOfWork unitOfWork, RabbitMqPublisherService rabbitMqPublisher, IOptions<ImageRootFile> options) : base(customerRepository, unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _rabbitMqPublisher = rabbitMqPublisher;
            _imageRootOptions = options.Value;
        }

        public async Task<Response<IEnumerable<CustomerDto>>> GetCustomersWithSamePhoneNumber()
        {
            var customers = await _customerRepository.GetRepeated();
            if (customers.Count() < 2)
                return Response<IEnumerable<CustomerDto>>.Fail("There is no customers with same number!",404,true);
            var response = ObjectMapper.Mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Response<IEnumerable<CustomerDto>>.Success(response, 200);
        }


        private async Task<string> ProccesImage(IFormFile imageFile)
        {
            var randomImageName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), _imageRootOptions.Original, randomImageName);
            await using FileStream stream = new(path, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            _rabbitMqPublisher.Publish(new ImageCreatedEvent() { ImageName = randomImageName });
            return randomImageName;
            
        }
        public async Task<Response<CustomerDto>> UploadCustomerPhoto(int id, IFormFile imageFile)
        {
            var entity = await _customerRepository.GetByIdAsync(id);
            if (entity == null)
                return Response<CustomerDto>.Fail("Id is not found!",404,true);
            var image = await ProccesImage(imageFile);
            var dto = ObjectMapper.Mapper.Map<CustomerDto>(entity);
            dto.Photograph = image;
            _customerRepository.Update(entity, dto);
            await _unitOfWork.CommitAsync();
            return Response<CustomerDto>.Success(dto, 200);
        }
    }
}
