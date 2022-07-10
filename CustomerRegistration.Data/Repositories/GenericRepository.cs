using CustomerRegistration.Core.Entities;
using CustomerRegistration.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomerRegistration.Data.Repositories
{
    public class GenericRepository<TEntity, TDto> : IGenericRepository<TEntity, TDto> where TEntity : BaseEntity where TDto : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int page, int pageSize)
        {
            var entities = _dbSet.AsNoTracking();
            var results = await entities.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return results;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(TEntity entity, TDto dto)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).CurrentValues.SetValues(dto);
        }
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        
    }
}
