

using BankDomainLayer.CommonDomains;
using BankDomainLayer.CustomerDomains;
using System.ComponentModel.DataAnnotations;

namespace BankDomainLayer.AccountDomains
{
    public class AccountRequestBody
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Account Name is required")]
        public string? AccountName { get; set; }
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Current Balance is required and amount should be minimum 100$ and Maximum 10,000$")]
        [Range(100,10000)]
        public decimal CurrentBalance { get; set; }
        [Required(ErrorMessage = "Account Type is required")]
        public AccountType AccountType { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
