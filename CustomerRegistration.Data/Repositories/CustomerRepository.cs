using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer, CustomerDto>, ICustomerRepository<Customer, CustomerDto>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Customer> _dbSet;
        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Customer>();
        }
        public async Task<IEnumerable<Customer>> GetRepeated()
        {
            var customers = await _dbSet.ToListAsync();
            var repeated = customers.GroupBy(x => x.Phone)
              .Where(y => y.Count() > 1)
              .SelectMany(z => z);
            return repeated;
        }
    }
}
