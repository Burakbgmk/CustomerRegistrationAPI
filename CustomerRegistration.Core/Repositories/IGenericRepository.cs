using System.Linq.Expressions;

namespace CustomerRegistration.Core.Repositories
{
    public interface IGenericRepository<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(int page, int pageSize);
        Task AddAsync(TEntity entity); 
        void Update(TEntity entity, TDto dto);

        void Delete(TEntity entity);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        
    }
}
