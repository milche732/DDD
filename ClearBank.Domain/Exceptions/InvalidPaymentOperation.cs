using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.Domain.Exceptions
{
    public class InvalidPaymentOperationException: InvalidOperationException
    {
        public InvalidPaymentOperationException(string message) : base(message)
        {

        }
    }
}
