using ClearBank.Domain.Exceptions;
using System;

namespace ClearBank.DeveloperTest.Types
{
    public class Account
    {
        public string _accountNumber;
        public decimal _balance;
        public AccountStatus _status;
        public PaymentScheme _allowedPaymentSchemes;

        public string AccountNumber => _accountNumber;
        public decimal Balance => _balance;
        public AccountStatus Status => _status;

        public Account(string accountNumber, decimal balance, AccountStatus status, int allowedPaymentSchemes)
        {
            if (allowedPaymentSchemes <= 0)
            {
                throw new ArgumentException($"AllowedPaymentSchemes  should be greater than 0. Actual {allowedPaymentSchemes}.", nameof(accountNumber));
            }
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("AccountNumber cannot not be empty or null.", nameof(accountNumber));
            }

            _accountNumber = accountNumber;
            _balance = balance;
            _status = status;
            _allowedPaymentSchemes = PaymentScheme.Create(allowedPaymentSchemes, accountNumber + "_scheme");
        }
        public void ProcessPayment(MakePaymentRequest request)
        {
            if (!request.IsApplicableTo(this))
            {
                throw new InvalidPaymentOperationException("Cannot process payment.");
            }
            _balance -= request.GetAmount();
        }

        public bool CanProcessPayment(MakePaymentRequest request)
        {
            return request.IsApplicableTo(this);
        }

        public bool IsAllowedPaymentScheme(PaymentScheme paymentScheme)
        {
            return this._allowedPaymentSchemes.Has(paymentScheme);
        }

        public override string ToString()
        {
            return $"<{AccountNumber}, {Balance}, {Status}>";
        }
    }
}
