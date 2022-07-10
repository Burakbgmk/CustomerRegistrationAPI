using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.Repositories
{
    public interface ICustomerRepository<TEntity, TDto> : IGenericRepository<TEntity, TDto> where TEntity : Customer where TDto : CustomerDto
    {
        Task<IEnumerable<TEntity>> GetRepeated();

    }
}
