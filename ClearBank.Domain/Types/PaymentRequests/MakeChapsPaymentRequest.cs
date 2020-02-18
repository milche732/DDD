using ClearBank.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.Domain.Types.PaymentRequests
{
    public class MakeChapsPaymentRequest: MakePaymentRequest
    {
        public MakeChapsPaymentRequest(string creditAccountNumber, string debtAccountNumber, decimal amount, DateTime paymentDate) : base(PaymentScheme.ChapsSchema, creditAccountNumber, debtAccountNumber, amount, paymentDate)
        {

        }

        public override bool IsApplicableTo(Account account)
        {
           return account.IsAllowedPaymentScheme(PaymentScheme) && account.Status == AccountStatus.Live;
        }
    }
}
