using ClearBank.Domain.Types;
using ClearBank.Infrastructure.Repository;
using System.Configuration;

namespace ClearBank.Domain.Services
{

    public class PaymentService : IPaymentService
    {
        private readonly IAccountRepository accountRepository;

        public PaymentService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = this.accountRepository.GetAccount(request.GetDebtorAccountNumber());

            var result = new MakePaymentResult();

            if (account != null && account.CanProcessPayment(request))
            {
                account.ProcessPayment(request);
                accountRepository.UpdateAccount(account);
                result.Success = true;
            }

            return result;
        }
    }
}
