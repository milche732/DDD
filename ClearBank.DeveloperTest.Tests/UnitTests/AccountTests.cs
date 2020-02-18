using System;
using System.Linq;
using Xunit;
using ClearBank.Domain.Types;
using ClearBank.Domain.Types.PaymentRequests;
using ClearBank.Domain.Exceptions;
using Moq;

namespace ClaerBank.UnitTests
{
    public class AccountTests
    {
        [Fact]
        public void IsAllowedPaymentScheme_Single_Sucessfull()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);

            Assert.True(acc.IsAllowedPaymentScheme(PaymentScheme.FasterSchema));
            
        }

        [Fact]
        public void IsAllowedPaymentScheme_Single_Failed()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);

            Assert.False(acc.IsAllowedPaymentScheme(PaymentScheme.ChapsSchema));
        }

        [Fact]
        public void IsAllowedPaymentScheme_Mutiple_Sucessfull()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code | PaymentScheme.BacsSchema.Code);

            Assert.True(acc.IsAllowedPaymentScheme(PaymentScheme.FasterSchema));
        }

        [Fact]
        public void IsAllowedPaymentScheme_Mutiple_Failed()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code | PaymentScheme.BacsSchema.Code);

            Assert.False(acc.IsAllowedPaymentScheme(PaymentScheme.ChapsSchema));

        }

        [Fact]
        public void ProcessPayment_WithBalance_Sucessfull()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.BacsSchema.Code);

            acc.ProcessPayment(new MakeBacsPaymentRequest("001", "002", 10m, DateTime.Now));

            Assert.Equal(90, acc.Balance);
        }

        [Fact]
        public void ProcessPayment_NotApplicablePaymentRequest_ThrowException()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);
            Moq.Mock<MakePaymentRequest> paymentRequest = new Moq.Mock<MakePaymentRequest>();

            paymentRequest.Setup(x => x.IsApplicableTo(It.IsAny<Account>())).Returns(false);

            Assert.Throws<InvalidPaymentOperationException>(() => acc.ProcessPayment(paymentRequest.Object));
        }
    }
}
