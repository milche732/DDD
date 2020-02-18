using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.Domain.Types.PaymentRequests
{
    public class MakeBacsPaymentRequest: MakePaymentRequest
    {
        public MakeBacsPaymentRequest(string creditAccountNumber, string debtAccountNumber, decimal amount, DateTime paymentDate) : base(PaymentScheme.BacsSchema, creditAccountNumber, debtAccountNumber, amount, paymentDate)
        {

        }

        public override bool IsApplicableTo(Account account)
        {
           return  account.IsAllowedPaymentScheme(PaymentScheme);
        }
    }
}
