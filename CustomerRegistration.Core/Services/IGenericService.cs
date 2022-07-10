using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.Services
{
    public interface IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> GetAllAsync(int page, int pageSize);
        Task<Response<TDto>> CreateAsync(TDto entity);
        Task<Response<TDto>> Update(int id, TDto entity);
        Task<Response<TDto>> RemoveAsync(int id);
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);


    }
}
