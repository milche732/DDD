using System;

namespace ClearBank.DeveloperTest.Types
{
    public abstract class MakePaymentRequest
    {
        public string CreditorAccountNumber { get; private set; }

        public string _debtorAccountNumber { get;}

        public decimal _amount;


        //that is needed to moq request, otherwise we need to implement IMakePaymentRequest
        public virtual decimal GetAmount()
        {
            return _amount;
        }
        public virtual string GetDebtorAccountNumber()
        {
            return _debtorAccountNumber;
        }

        public DateTime PaymentDate { get; private set; }

        public PaymentScheme PaymentScheme { get; private set; }

        public MakePaymentRequest() { }

        protected MakePaymentRequest(PaymentScheme paymentScheme, string creditAccountNumber, string debtAccountNumber, decimal amount, DateTime paymentDate)
        {
            CreditorAccountNumber = creditAccountNumber;
            _debtorAccountNumber = debtAccountNumber;
            _amount = amount;
            PaymentDate = paymentDate;
            PaymentScheme = paymentScheme;
        }

        public abstract bool IsApplicableTo(Account account);

        public override string ToString()
        {
            return $"<{PaymentScheme.Description}, {GetAmount()}, {CreditorAccountNumber}, {GetDebtorAccountNumber()}>";
        }
    }
}
