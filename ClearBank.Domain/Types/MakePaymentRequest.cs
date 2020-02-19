using System;

namespace ClearBank.Domain.Types
{
    public abstract class MakePaymentRequest
    {
        public DateTime PaymentDate { get; private set; }

        public PaymentScheme PaymentScheme { get; private set; }

        public string CreditorAccountNumber { get; private set; }

        private string _debtorAccountNumber { get;}

        private decimal _amount;


        public MakePaymentRequest() { }

        protected MakePaymentRequest(PaymentScheme paymentScheme, string creditAccountNumber, string debtAccountNumber, decimal amount, DateTime paymentDate)
        {
            CreditorAccountNumber = creditAccountNumber;
            _debtorAccountNumber = debtAccountNumber;
            _amount = amount;
            PaymentDate = paymentDate;
            PaymentScheme = paymentScheme;
        }

        //that is needed to moq Amount, otherwise we need to implement IMakePaymentRequest, but was mentioned to not change signature of IPaymentService 
        public virtual decimal GetAmount()
        {
            return _amount;
        }
        public virtual string GetDebtorAccountNumber()
        {
            return _debtorAccountNumber;
        }


        public abstract bool IsApplicableTo(Account account);

        public override string ToString()
        {
            return $"<{PaymentScheme.Description}, {GetAmount()}, {CreditorAccountNumber}, {GetDebtorAccountNumber()}>";
        }
    }
}
