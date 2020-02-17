using ClearBank.DeveloperTest.Types;

namespace ClearBank.Infrastructure.Repository
{
    public interface IAccountRepository
    {
        Account GetAccount(string accountNumber);
        void UpdateAccount(Account account);
    }
}
