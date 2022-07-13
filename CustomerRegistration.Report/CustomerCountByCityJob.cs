using CustomerRegistration.Core.Entities;
using CustomerRegistration.Data;
using CustomerRegistration.Report.Dtos;
using CustomerRegistration.Report.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Report
{
    [DisallowConcurrentExecution]
    public class CustomerCountByCityJob : IJob
    {
        private readonly ExcelService<CustomerCountByCityDto> _excelService;
        private readonly EmailSenderService _emailSenderService;
        private readonly IServiceProvider _serviceProvider;
        public CustomerCountByCityJob(ExcelService<CustomerCountByCityDto> excelService, IServiceProvider serviceProvider, EmailSenderService emailSenderService)
        {
            _excelService = excelService;
            _serviceProvider = serviceProvider;
            _emailSenderService = emailSenderService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await GetCustomerCountsToExcel();
            await SendMailToReceivers();
            Console.WriteLine($"-Customer Counts by City- report is send! {DateTime.Now:U}");
        }

        private async Task GetCustomerCountsToExcel()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var _customerSet = dbContext.Set<Customer>();
                var entities = await _customerSet.ToListAsync();
                var counts = entities.GroupBy(x => x.City).ToList();
                var list = new List<CustomerCountByCityDto>();
                foreach (var count in counts)
                {
                    list.Add(new CustomerCountByCityDto() { City = count.Key, Count = count.Count() });
                }
                await _excelService.ProcessExcel(list, "Customer Counts by Cities");
            }
        }

        private async Task SendMailToReceivers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var entities = await dbContext.Set<UserApp>().ToListAsync();
                var receivers = entities.Select(x => x.Email).ToList();
                foreach(var receiver in receivers)
                    await _emailSenderService.EmailSend(receiver);
            }
        }
    }
}
