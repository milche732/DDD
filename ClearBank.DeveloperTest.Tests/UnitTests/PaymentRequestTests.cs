using System;
using System.Linq;
using Xunit;
using ClearBank.Domain.Types;
using ClearBank.Domain.Types.PaymentRequests;

namespace ClaerBank.UnitTests
{
    public class PaymentRequestTests
    {
        [Fact]
        public void IsApplicableTo_BacsSameSchema_Sucessfull()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.BacsSchema.Code);

            MakePaymentRequest mak = new MakeBacsPaymentRequest("001", "001", 10, DateTime.Now);
            Assert.True(mak.IsApplicableTo(acc));
        }

        [Fact]
        public void IsApplicableTo_BacsDifferentSchema_Failed()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.ChapsSchema.Code);

            MakePaymentRequest mak = new MakeBacsPaymentRequest("001", "001", 10, DateTime.Now);
            Assert.False(mak.IsApplicableTo(acc));
        }


        [Fact]
        public void IsApplicableTo_FasterSameSchemaBalanceMoreThanAmount_Sucessfull()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.FasterSchema.Code);

            MakePaymentRequest mak = new MakeFasterPaymentRequest("001", "001", 10, DateTime.Now);
            Assert.True(mak.IsApplicableTo(acc));
        }

        [Fact]
        public void IsApplicableTo_FasterSameSchemaBalanceLowerThanAmount_Failed()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.ChapsSchema.Code);

            MakeBacsPaymentRequest mak = new MakeBacsPaymentRequest("001", "001", 110m, DateTime.Now);
            Assert.False(mak.IsApplicableTo(acc));
        }

        [Fact]
        public void IsApplicableTo_ChapsSameSchemaBalanceMoreThanAmount_Sucessfull()
        {
            Account acc = new Account("001", 100m, AccountStatus.Live, PaymentScheme.ChapsSchema.Code);

            MakePaymentRequest mak = new MakeChapsPaymentRequest("001", "001", 10, DateTime.Now);
            Assert.True(mak.IsApplicableTo(acc));
        }

        [Fact]
        public void IsApplicableTo_ChapsNotLiveStatus_Failed()
        {
            Account acc = new Account("001", 100m, AccountStatus.InboundPaymentsOnly, PaymentScheme.ChapsSchema.Code);

            MakeBacsPaymentRequest mak = new MakeBacsPaymentRequest("001", "001", 110, DateTime.Now);
            Assert.False(mak.IsApplicableTo(acc));
        }

    }
}
