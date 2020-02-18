using ClearBank.Domain.Services;
using ClearBank.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using ClearBank.Infrastructure.Repository.Backup;
using ClearBank.Domain.Types.PaymentRequests;
using System.IO;

namespace ClearBank.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

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
            var account = serviceProvider.GetService<IAccountRepository>().GetAccount("ACC_001");

            Console.WriteLine($"Account Before: {account}");

            var payment = new MakeFasterPaymentRequest("1", "ACC_001", 100, DateTime.Now);

            var result = paymentService.MakePayment(payment);

            Console.WriteLine($"Making Payment: {payment}");
            Console.WriteLine($"Payment: {result.Success}");
            Console.WriteLine($"Account After: {account}");

        }

     
    }

    public static class ProgramExtension{
        public static IServiceCollection AddReposiory(this IServiceCollection services, IConfiguration configuration)
        {
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
