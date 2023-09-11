using BankDomainLayer.AccountDomains;
using BankServiceLayer.Interfaces;
using DataAccessLayer;

namespace BankServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        private IDataAccess _dataAccess;

        public AccountService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Account> CreateNewAccount(Account account)
        {
            try
            {
                var newAccount=_dataAccess.CreateAccount(account);
                return Task.FromResult(newAccount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Account> DeleteAccount(int accountId)
        {
            try
            {
                var deletedAccount = _dataAccess.DeleteAccount(accountId);
                return Task.FromResult(deletedAccount);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<IEnumerable<Account>> GetAccountsData()
        {
            try
            {
               var result=await Task.FromResult(_dataAccess.GetAccounts());
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
