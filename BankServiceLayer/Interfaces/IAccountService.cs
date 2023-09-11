using BankDomainLayer.AccountDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServiceLayer.Interfaces
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAccountsData();
        public Task<Account> CreateNewAccount(Account account);
        public Task<Account> DeleteAccount(int accountId);
    }
}
