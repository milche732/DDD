using System;
using System.Linq;
using Xunit;
using ClearBank.DeveloperTest.Types;
using ClearBank.Domain.Types.PaymentRequests;
using ClearBank.DeveloperTest.Services;
using ClearBank.Infrastructure.Repository;
using Moq;

namespace ClaerBank.UnitTests
{
    public class PaymentServiceTests
    {
        [Fact]
        public void MakePayment_LegitRequest_Sucessfull()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);
            Moq.Mock<IAccountRepository> repo = new Moq.Mock<IAccountRepository>();
            repo.Setup(x => x.GetAccount("001")).Returns(acc);

            Moq.Mock<MakePaymentRequest> paymentRequest = new Moq.Mock<MakePaymentRequest>();
            
            paymentRequest.Setup(x => x.IsApplicableTo(acc)).Returns(true);
            paymentRequest.Setup(x => x.GetAmount()).Returns(10);
            paymentRequest.Setup(x => x.GetDebtorAccountNumber()).Returns("001");

            PaymentService paymentService = new PaymentService(repo.Object);
            var result = paymentService.MakePayment(paymentRequest.Object);

            Assert.True(result.Success);
            Assert.Equal(90m, acc.Balance);
            repo.Verify(v => v.UpdateAccount(acc));
        }

        [Fact]
        public void MakePayment_NotLegitRequest_Failed()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);
            Moq.Mock<IAccountRepository> repo = new Moq.Mock<IAccountRepository>();
            repo.Setup(x => x.GetAccount("001")).Returns(acc);

            Moq.Mock<MakePaymentRequest> paymentRequest = new Moq.Mock<MakePaymentRequest>();

            paymentRequest.Setup(x => x.IsApplicableTo(acc)).Returns(false);
            paymentRequest.Setup(x => x.GetAmount()).Returns(10);
            paymentRequest.Setup(x => x.GetDebtorAccountNumber()).Returns("001");

            PaymentService paymentService = new PaymentService(repo.Object);
            var result = paymentService.MakePayment(paymentRequest.Object);
            Assert.False(result.Success);
            Assert.Equal(100m, acc.Balance);
        }

        [Fact]
        public void MakePayment_AccountNotFoundRequest_Sucessfull()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);
            Moq.Mock<IAccountRepository> repo = new Moq.Mock<IAccountRepository>();
            repo.Setup(x => x.GetAccount("001")).Returns(acc);

            Moq.Mock<MakePaymentRequest> paymentRequest = new Moq.Mock<MakePaymentRequest>();

            paymentRequest.Setup(x => x.IsApplicableTo(acc)).Returns(false);
            paymentRequest.Setup(x => x.GetAmount()).Returns(10);
            paymentRequest.Setup(x => x.GetDebtorAccountNumber()).Returns("002");

            PaymentService paymentService = new PaymentService(repo.Object);
            var result = paymentService.MakePayment(paymentRequest.Object);
            Assert.False(result.Success);
            Assert.Equal(100m, acc.Balance);
        }

    }
}
