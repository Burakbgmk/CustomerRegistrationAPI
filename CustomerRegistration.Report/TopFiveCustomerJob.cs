using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Data;
using CustomerRegistration.Report.Services;
using CustomerRegistration.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CustomerRegistration.Report
{
    [DisallowConcurrentExecution]
    public class TopFiveCustomerJob : IJob
    {
        private readonly ExcelService<Customer> _excelService;
        private readonly EmailSenderService _emailSenderService;
        private readonly IServiceProvider _serviceProvider;
        public TopFiveCustomerJob(ExcelService<Customer> excelService, IServiceProvider serviceProvider, EmailSenderService emailSenderService)
        {
            _excelService = excelService;
            _serviceProvider = serviceProvider;
            _emailSenderService = emailSenderService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await GetTopFiveCustomersToExcel();
            await SendMailToReceivers();
            Console.WriteLine($"-Top 5 Customer by Activies- report is send! {DateTime.Now:U}");
        }

        private async Task GetTopFiveCustomersToExcel()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var _commercialSet = dbContext.Set<CommercialActivity>();
                var entities = await _commercialSet.ToListAsync();
                var orderedList = entities.GroupBy(x => x.CustomerId).OrderByDescending(y => y.Count()).Take(5).SelectMany(z => z.Select(x => x.CustomerId)).Distinct().ToList();
                var _customerSet = dbContext.Set<Customer>();
                var topFive = new List<Customer>();
                foreach(var item in orderedList)
                {
                    topFive.Add(await _customerSet.FindAsync(item));
                }

                await _excelService.ProcessExcel(topFive, "Top 5 Customers by Commercial Activity");
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
