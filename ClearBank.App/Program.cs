using ClearBank.DeveloperTest.Services;
using ClearBank.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using ClearBank.Infrastructure.Repository.Backup;
using ClearBank.Domain.Types.PaymentRequests;

namespace ClearBank.App
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .Build();

            var serviceProvider = new ServiceCollection()
           .AddReposiory(config)
           .AddTransient<IPaymentService, PaymentService>()
           .AddLogging(opt =>
           {
               opt.AddConsole();
           })
            .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
           .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogDebug("Starting application");

            var paymentService = serviceProvider.GetService<IPaymentService>();
            paymentService.MakePayment(new MakeFasterPaymentRequest("1","1",100, DateTime.Now));
            /*
            IAccountRepository accountRepository = serviceProvider.GetService<IAccountRepository>();

            var fromAccount = accountRepository.GetAccountById(Guid.Parse("7511ca49-059e-4eae-9c8f-caf29bc61063"));
            var toAccount = accountRepository.GetAccountById(Guid.Parse("b3388bd0-de5d-40aa-8582-4571b4223d36"));
            Console.WriteLine("Send 10");
            Console.WriteLine("From: " + fromAccount.ToString());
            Console.WriteLine("To: " + toAccount.ToString());
            var transferMoney = serviceProvider.GetService<ITransferMoney>();

            transferMoney.Execute(fromAccount.Id, toAccount.Id, 10);

            Console.WriteLine("Hello World!");*/
        }

     
    }

    public static class ProgramExtension{
        public static IServiceCollection AddReposiory(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["AppSettings"];

            if (configuration.GetValue<string>("DataStoreType") == "Backup")
            {
                services.AddTransient<IAccountRepository, AccountDataStore>();
            }
            else
            {
                services.AddTransient<IAccountRepository, BackupAccountDataStore>();
            }


            return services;
        }
    }
}
