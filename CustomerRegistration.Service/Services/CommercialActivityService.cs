using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Core.Repositories;
using CustomerRegistration.Core.Services;
using CustomerRegistration.Core.UnitOfWork;

namespace CustomerRegistration.Service.Services
{
    public class CommercialActivityService : GenericService<CommercialActivity, CommercialActivityDto>, ICommercialActivityService<CommercialActivity, CommercialActivityDto>
    {
        public CommercialActivityService(IGenericRepository<CommercialActivity, CommercialActivityDto> commercialActivityRepository, IUnitOfWork unitOfWork) : base(commercialActivityRepository, unitOfWork)
        {
        }
    }
}
