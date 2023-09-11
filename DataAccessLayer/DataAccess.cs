using BankDomainLayer.AccountDomains;
using BankDomainLayer.CommonDomains;
using BankDomainLayer.CustomerDomains;
using BankDomainLayer.TransactionDomains;

namespace DataAccessLayer
{
    public class DataAccess : IDataAccess
    {
        private IList<Account> accounts;
        public DataAccess()
        {
            //accounts = new List<Account>();
        }

        public Account CreateAccount(Account account)
        {
            try
            {
                var existedCustomer = accounts.SingleOrDefault(x => x.Customer.FirstName == account.Customer.FirstName && x.Customer.LastName == account.Customer.LastName);
                if (existedCustomer == null)
                {
                    int newCustomerId = accounts.Max(x => x.Customer.Id) + 1;
                    account.Customer.Id = newCustomerId;
                }
                else
                {
                    account.Customer.Id = existedCustomer.Customer.Id;
                }
                int newAccountId = accounts.Max(x => x.AccountId) + 1;
                string newAccountNumber = Guid.NewGuid().ToString();
                account.AccountNumber = newAccountNumber;
                account.AccountId = newAccountId;
                accounts.Add(account);
                return account;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Account DeleteAccount(int accountId)
        {
            try
            {
                Account account = accounts.SingleOrDefault(x => x.AccountId == accountId);
                accounts.Remove(account);
                return account;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<Account> GetAccounts()
        {
            try
            {
                if (accounts == null)
                {
                    accounts = new List<Account>();
                    Customer customer = new();
                    customer.FirstName = "Ram";
                    customer.LastName = "Raj";
                    customer.Id = 1;
                    accounts.Add(new Account()
                    {
                        Customer = customer,
                        AccountId = 1,
                        AccountName = "Ram Raj Savings Account",
                        AccountNumber = Guid.NewGuid().ToString(),
                        AccountType = (AccountType)1,
                        CurrentBalance = 10000,
                        Email = "ramrajsaho123@Bank.com",
                        PhoneNumber = "9877654356",
                        Created = DateTime.Now,
                        LastUpdated = DateTime.Now,
                    });
                    accounts.Add(new Account()
                    {
                        Customer = customer,
                        AccountId = 2,
                        AccountName = "Ram Raj Current Account",
                        AccountNumber = Guid.NewGuid().ToString(),
                        AccountType = (AccountType)2,
                        CurrentBalance = 20000,
                        Email = "ramrajsaho123@Bank.com",
                        PhoneNumber = "9877656940",
                        Created = DateTime.Now,
                        LastUpdated = DateTime.Now,
                    });
                    customer = new();
                    customer.FirstName = "Suresh";
                    customer.LastName = "Boj";
                    customer.Id = 2;
                    accounts.Add(new Account()
                    {
                        Customer = customer,
                        AccountId = 3,
                        AccountName = "Suresh Savings Account",
                        AccountNumber = Guid.NewGuid().ToString(),
                        AccountType = (AccountType)1,
                        CurrentBalance = 5000,
                        Email = "suresh123@Bank.com",
                        PhoneNumber = "9848033884",
                        Created = DateTime.Now,
                        LastUpdated = DateTime.Now,
                    });
                    accounts.Add(new Account()
                    {
                        Customer = customer,
                        AccountId = 4,
                        AccountName = "Suresh Current Account",
                        AccountNumber = Guid.NewGuid().ToString(),
                        AccountType = (AccountType)2,
                        CurrentBalance = 3000,
                        Email = "suresh123@Bank.com",
                        PhoneNumber = "9848033884",
                        Created = DateTime.Now,
                        LastUpdated = DateTime.Now,
                    });
                }
                return accounts;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool MakeAccountTransaction(TransactionRequestBody transactionRequestBody)
        {
            try
            {
                Account account = accounts.SingleOrDefault(x => x.AccountId == transactionRequestBody.TransactionAccountId);
                int accountIndex = accounts.IndexOf(account);
                if (transactionRequestBody.transactionType == (TransactionType)1)
                    account.CurrentBalance += transactionRequestBody.Amount;
                else
                    account.CurrentBalance -= transactionRequestBody.Amount;
                accounts.RemoveAt(accountIndex);
                accounts.Insert(accountIndex, account);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
