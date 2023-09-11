using BankDomainLayer.CommonDomains;
using System.ComponentModel.DataAnnotations;


namespace BankDomainLayer.TransactionDomains
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionRequestBody transactionRequest { get; set; }
    }

    public class TransactionRequestBody
    {
        [Required(ErrorMessage ="Account Id is required for a Transaction")]
        public int TransactionAccountId { get; set; }
        [Required(ErrorMessage = "Account Type is required for Transaction")]
        public TransactionType transactionType { get; set; }
        [Required(ErrorMessage = "Amount is required for Transaction")]
        public decimal Amount { get; set; }
    }
}
