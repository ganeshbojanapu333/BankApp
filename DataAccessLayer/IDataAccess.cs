using BankDomainLayer.AccountDomains;
using BankDomainLayer.TransactionDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataAccess
    {
        public IEnumerable<Account> GetAccounts();
        public Account DeleteAccount(int AccountId);
        public Account CreateAccount(Account account);
        public bool MakeAccountTransaction(TransactionRequestBody transactionRequestBody);
    }
}
