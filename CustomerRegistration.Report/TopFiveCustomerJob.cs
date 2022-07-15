using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Data;
using CustomerRegistration.Report.Dtos;
using CustomerRegistration.Report.Services;
using CustomerRegistration.Report.Services.Abstractions;
using CustomerRegistration.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CustomerRegistration.Report
{
    [DisallowConcurrentExecution]
    public class TopFiveCustomerJob : IJob
    {
        private readonly IExcelService<TopFiveCustomersByActivityDto> _excelService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IServiceProvider _serviceProvider;
        public TopFiveCustomerJob(IExcelService<TopFiveCustomersByActivityDto> excelService, IServiceProvider serviceProvider, IEmailSenderService emailSenderService)
        {
            _excelService = excelService;
            _serviceProvider = serviceProvider;
            _emailSenderService = emailSenderService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await SaveAsExcel();
            await UpdateToDatabase();
            await SendMailToReceivers();
            Console.WriteLine($"-Top 5 Customer by Activies- report is send! {DateTime.Now:U}");
        }
        

        private async Task<List<Customer>> GetTopFiveCustomers(AppDbContext dbContext)
        {
            var _commercialSet = dbContext.Set<CommercialActivity>();
            var entities = await _commercialSet.ToListAsync();
            var topFiveIdList = entities.GroupBy(x => x.CustomerId).OrderByDescending(y => y.Count()).Take(5).SelectMany(z => z.Select(x => x.CustomerId)).Distinct().ToList();
            var _customerSet = dbContext.Set<Customer>();
            var topFiveCustomers = new List<Customer>();
            foreach (var item in topFiveIdList)
            {
                topFiveCustomers.Add(await _customerSet.FindAsync(item));
            }
            return topFiveCustomers;
        }

        private async Task SaveAsExcel()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var topFiveCustomers = await GetTopFiveCustomers(dbContext);
                var topFiveDto = new List<TopFiveCustomersByActivityDto>();
                foreach (var customer in topFiveCustomers)
                {
                    topFiveDto.Add(new TopFiveCustomersByActivityDto { FirstName = customer.FirstName, LastName = customer.LastName, ActivityCount = customer.CommercialActivities.Count() });
                }

                await _excelService.ProcessExcel(topFiveDto, "Top 5 Customers by Commercial Activity");
            }
        }

        private async Task UpdateToDatabase()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var topFiveCustomers = await GetTopFiveCustomers(dbContext);
                var _reportDetailSet = dbContext.Set<TopFiveCustomersByActivityReportDetail>();
                var _reportSet = dbContext.Set<TopFiveCustomersByActivityReport>();
                var report = new TopFiveCustomersByActivityReport();
                await _reportSet.AddAsync(report);
                foreach (var customer in topFiveCustomers)
                {
                    await _reportDetailSet.AddAsync(new TopFiveCustomersByActivityReportDetail { FirstName = customer.FirstName, LastName = customer.LastName, ActivityCount = customer.CommercialActivities.Count(), ReportId = report.Id, Report = report });
                }
                await dbContext.SaveChangesAsync();
            }
        }


        private async Task SendMailToReceivers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var entities = await dbContext.Set<UserApp>().ToListAsync();
                //I decided to give admin roles whose username starts with "Admin_" and set the claims accordingly..
                var receivers = entities.Where(g => g.UserName.StartsWith("Admin_")).Select(x => x.Email).ToList();
                foreach (var receiver in receivers)
                    await _emailSenderService.EmailSend(receiver);
            }
        }
    }
}
