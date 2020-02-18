using System;

namespace ClearBank.DeveloperTest.Types
{
    public abstract class MakePaymentRequest
    {
        public string CreditorAccountNumber { get; private set; }

        public string DebtorAccountNumber { get; private set; }

        public decimal Amount { get; private set; }

        public DateTime PaymentDate { get; private set; }

        public PaymentScheme PaymentScheme { get; private set; }

        protected MakePaymentRequest(PaymentScheme paymentScheme, string creditAccountNumber, string debtAccountNumber, decimal amount, DateTime paymentDate)
        {
            CreditorAccountNumber = creditAccountNumber;
            DebtorAccountNumber = debtAccountNumber;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentScheme = paymentScheme;
        }

        public abstract bool IsApplicableTo(Account account); 
    }
}
