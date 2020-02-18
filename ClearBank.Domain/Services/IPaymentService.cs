using ClearBank.Domain.Types;

namespace ClearBank.Domain.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
