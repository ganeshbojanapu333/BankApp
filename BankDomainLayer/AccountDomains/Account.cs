using BankDomainLayer.CommonDomains;
using BankDomainLayer.CustomerDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDomainLayer.AccountDomains
{
    public class Account
    {
        public int AccountId { get; set; }
        public Customer? Customer { get; set; }
        [Required(ErrorMessage = "Account Name is required")]
        public string? AccountName { get; set; }
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        [Required(ErrorMessage = "Account Type is required")]
        public AccountType AccountType { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
