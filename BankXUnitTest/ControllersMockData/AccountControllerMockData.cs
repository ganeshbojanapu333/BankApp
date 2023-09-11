using BankDomainLayer.AccountDomains;
using BankDomainLayer.CommonDomains;
using BankDomainLayer.CustomerDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankXUnitTest.ControllersMockData
{
    public class AccountControllerMockData
    {
        public static async Task<IEnumerable<Account>> GetAllAccountsMockData()
        {
            List < Account> testAccounts= new List<Account>();
                testAccounts = new List<Account>();
                Customer customer = new();
                customer.FirstName = "Ram";
                customer.LastName = "Raj";
                customer.Id = 1;
                testAccounts.Add(new Account()
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
                testAccounts.Add(new Account()
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
                testAccounts.Add(new Account()
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
                testAccounts.Add(new Account()
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
            var testAccountsResult= await Task.FromResult(testAccounts);
            return testAccountsResult;
        }
    }
}
