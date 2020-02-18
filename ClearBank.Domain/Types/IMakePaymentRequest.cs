using ClearBank.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.Domain.Types
{
    interface IMakePaymentRequest
    {
        string CreditorAccountNumber { get;  }

        string DebtorAccountNumber { get; }

        decimal Amount { get; }

        DateTime PaymentDate { get; }

        PaymentScheme PaymentScheme { get; }

    }
}
