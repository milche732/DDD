using System;

namespace ClearBank.DeveloperTest.Types
{
    public class Account
    {
        public string _accountNumber;
        public decimal _balance;
        public AccountStatus _status;
        public AllowedPaymentSchemes _allowedPaymentSchemes;

        public string AccountNumber => _accountNumber;
        public decimal Balance => _balance;
        public AccountStatus Status => _status;
        public AllowedPaymentSchemes AllowedPaymentSchemes => _allowedPaymentSchemes;

        public Account(string accountNumber, decimal balance, AccountStatus status, AllowedPaymentSchemes allowedPaymentSchemes)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("AccountNumber cannot not be empty or null.", nameof(accountNumber));
            }

            _accountNumber = accountNumber;
            _balance = balance;
            _status = status;
            _allowedPaymentSchemes = allowedPaymentSchemes;
        }
        public void ProcessPayment(MakePaymentRequest request)
        {
            if (!CanProcessPayment(request))
            {
                throw new InvalidOperationException("cannot process payment");
            }
            _balance -= request.Amount;
        }

        public bool CanProcessPayment(MakePaymentRequest request)
        {
            bool result = false;
            switch (request.PaymentScheme)
            {
                case PaymentScheme.Bacs:
                    result = AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
                    break;

                case PaymentScheme.FasterPayments:
                    result = AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && this.Balance >= request.Amount;
                    break;

                case PaymentScheme.Chaps:
                    result = AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && Status == AccountStatus.Live;
                    break;
            }

            return result;
        }
    }
}
