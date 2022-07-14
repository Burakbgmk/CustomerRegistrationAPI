using CustomerRegistration.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.Data
{
    public class AppDbContext : IdentityDbContext<UserApp, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CommercialActivity> CommercialActivities { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<CustomerCountByCityReportDetail> CustomerCountByCityReportDetails { get; set; }
        public DbSet<CustomerCountByCityReport> CustomerCountByCityReports { get; set; }
        public DbSet<TopFiveCustomersByActivityReport> TopFiveCustomersByActivityReports { get; set; }
        public DbSet<Customer> TopFiveCustomersByActivityReportDetails { get; set; }

        

    }
}
