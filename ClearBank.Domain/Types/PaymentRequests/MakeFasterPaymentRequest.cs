using ClearBank.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.Domain.Types.PaymentRequests
{
    public class MakeFasterPaymentRequest: MakePaymentRequest
    {
        public MakeFasterPaymentRequest(string creditAccountNumber, string debtAccountNumber, decimal amount, DateTime paymentDate) : 
            base(PaymentScheme.FasterSchema, creditAccountNumber, debtAccountNumber, amount, paymentDate)
        {

        }

        public override bool IsApplicableTo(Account account)
        {
           return  account.IsAllowedPaymentScheme(this.PaymentScheme) &&  account.Balance >= GetAmount();
        }
    }
}
