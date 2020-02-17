using ClearBank.DeveloperTest.Types;

namespace ClearBank.Infrastructure.Repository
{
    public class AccountDataStore: IAccountRepository
    {
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account("ACC_001", 10, AccountStatus.Live, AllowedPaymentSchemes.Bacs);
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
