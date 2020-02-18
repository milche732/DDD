using System;
using System.Linq;
using Xunit;
using ClearBank.DeveloperTest.Types;
using ClearBank.Domain.Types.PaymentRequests;
using ClearBank.DeveloperTest.Services;
using ClearBank.Infrastructure.Repository;
using Moq;

namespace ClaerBank.IntegrationTest
{
    public class PaymentServiceTests
    {
        [Fact]
        public void MakePayment_LegitRequest_Sucessfull()
        {
            Account acc = new Account("001", 1001m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);
            Moq.Mock<IAccountRepository> repo = new Moq.Mock<IAccountRepository>();
            repo.Setup(x => x.GetAccount("001")).Returns(acc);

            MakeFasterPaymentRequest makeFasterPaymentRequest = new MakeFasterPaymentRequest("002", "001", 1000m, DateTime.Now);
            
            PaymentService paymentService = new PaymentService(repo.Object);
            var result = paymentService.MakePayment(makeFasterPaymentRequest);

            Assert.True(result.Success);
            Assert.Equal(1m, acc.Balance);
        }

        [Fact]
        public void MakePayment_NotLegitRequest_Sucessfull()
        {
            Account acc = new Account("001", 1001m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);
            Moq.Mock<IAccountRepository> repo = new Moq.Mock<IAccountRepository>();
            repo.Setup(x => x.GetAccount("001")).Returns(acc);

            MakeFasterPaymentRequest makeFasterPaymentRequest = new MakeFasterPaymentRequest("001", "002", 1002m, DateTime.Now);

            PaymentService paymentService = new PaymentService(repo.Object);
            var result = paymentService.MakePayment(makeFasterPaymentRequest);

            Assert.False(result.Success);
            Assert.Equal(1001m, acc.Balance);
        }

    }
}
