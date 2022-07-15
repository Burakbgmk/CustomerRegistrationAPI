using CustomerRegistration.Core.Entities;
using CustomerRegistration.Data;
using CustomerRegistration.Report.Dtos;
using CustomerRegistration.Report.Services;
using CustomerRegistration.Report.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CustomerRegistration.Report
{
    [DisallowConcurrentExecution]
    public class CustomerCountByCityJob : IJob
    {
        private readonly IExcelService<CustomerCountByCityDto> _excelService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IServiceProvider _serviceProvider;
        public CustomerCountByCityJob(IExcelService<CustomerCountByCityDto> excelService, IServiceProvider serviceProvider, IEmailSenderService emailSenderService)
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
            Console.WriteLine($"-Customer Counts by City- report is send! {DateTime.Now:U}");
        }

        private async Task<List<IGrouping<string?,Customer>>> GetGroupingByCountList(AppDbContext dbContext)
        {
            var _customerSet = dbContext.Set<Customer>();
            var entities = await _customerSet.AsNoTracking().ToListAsync();
            var counts = entities.GroupBy(x => x.City).ToList();
            return counts;
        }

        private async Task SaveAsExcel()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var counts = await GetGroupingByCountList(dbContext);
                var list = new List<CustomerCountByCityDto>();
                foreach (var count in counts)
                {
                    list.Add(new CustomerCountByCityDto() { City = count.Key, Count = count.Count() });
                }
                await _excelService.ProcessExcel(list, "Customer Counts by Cities");
            }
        }

        private async Task UpdateToDatabase()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var counts = await GetGroupingByCountList(dbContext);
                var _reportDetailSet = dbContext.Set<CustomerCountByCityReportDetail>();
                var _reportSet = dbContext.Set<CustomerCountByCityReport>();
                var report = new CustomerCountByCityReport();
                await _reportSet.AddAsync(report);
                foreach (var count in counts)
                {
                    await _reportDetailSet.AddAsync(new CustomerCountByCityReportDetail { City = count.Key, Count = count.Count(), ReportId = report.Id, Report = report });
                }
                await dbContext.SaveChangesAsync();
            }
        }

        private async Task SendMailToReceivers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var entities = await dbContext.Set<UserApp>().AsNoTracking().ToListAsync();
                var receivers = entities.Select(x => x.Email).ToList();
                foreach (var receiver in receivers)
                    await _emailSenderService.EmailSend(receiver);
            }
        }
    }
}
