using ClearBank.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;
namespace ClearBank.Infrastructure.Repository
{
    public class AccountDataStore: IAccountRepository
    {
        private static List<Account> list = new List<Account>
        {
            new Account("ACC_001", 500m, AccountStatus.Live, 1)
        };
        public Account GetAccount(string accountNumber)
        {
            return list.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
