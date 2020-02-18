using ClearBank.DeveloperTest.Types;
using ClearBank.Infrastructure.Repository;

namespace ClearBank.Infrastructure.Repository.Backup
{
    public class BackupAccountDataStore: IAccountRepository
    {
        public Account GetAccount(string accountNumber)
        {
            // Access backup data base to retrieve account, code removed for brevity 
            return new Account("BACC_001", 10, AccountStatus.Live, 1 );

        }

        public void UpdateAccount(Account account)
        {
            // Update account in backup database, code removed for brevity
        }
    }
}
