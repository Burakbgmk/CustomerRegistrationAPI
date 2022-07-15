using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using CustomerRegistration.Core.Repositories;
using CustomerRegistration.Core.Services;
using CustomerRegistration.Core.Services.AuthServices;
using CustomerRegistration.Core.UnitOfWork;
using CustomerRegistration.Data;
using CustomerRegistration.Data.Repositories;
using CustomerRegistration.Report;
using CustomerRegistration.Report.Core;
using CustomerRegistration.Report.Services;
using CustomerRegistration.Report.Services.Abstractions;
using CustomerRegistration.Service.Services;
using CustomerRegistration.Service.Services.AuthServices;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using RabbitMQ.Client;
using SharedLibrary.BackgroundServices;
using SharedLibrary.Services;

namespace CustomerRegistration.API
{
    public static class CustomServiceBuilders
    {

        public static void BuildServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.AddScoped(typeof(ICustomerRepository<Customer, CustomerDto>), typeof(CustomerRepository));
            builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            builder.Services.AddScoped(typeof(ICustomerService<Customer, CustomerDto>), typeof(CustomerService));
            builder.Services.AddScoped(typeof(ICommercialActivityService<CommercialActivity, CommercialActivityDto>), typeof(CommercialActivityService));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMq")) });
            builder.Services.AddSingleton<RabbitMqClientService>();
            builder.Services.AddSingleton<RabbitMqPublisherService>();
            builder.Services.AddHostedService<WatermarkImageBackgroundService>();

            builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
            builder.Services.AddSingleton(typeof(IExcelService<>), typeof(ExcelService<>));
            builder.Services.AddSingleton<CustomerCountByCityJob>();
            builder.Services.AddSingleton<TopFiveCustomerJob>();

            builder.Services.AddSingleton(new JobSchedule(
                jobType: typeof(CustomerCountByCityJob),
            //cronExpression: "0/10 * * * * ?"));
            cronExpression: "0 0 12 1 * ?"));// Every month on the first day
            builder.Services.AddSingleton(new JobSchedule(
                jobType: typeof(TopFiveCustomerJob),
            //cronExpression: "0/10 * * * * ?"));
            cronExpression: "0 0 12 ? * FRI"));//Every Friday
            builder.Services.AddHostedService<QuartzHostedService>();
        }
    }
}
