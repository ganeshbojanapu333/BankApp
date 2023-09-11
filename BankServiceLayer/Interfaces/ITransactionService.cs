
using BankDomainLayer.TransactionDomains;

namespace BankServiceLayer.Interfaces
{
    public interface ITransactionService
    {
        public Task<bool> CreateTransaction(TransactionRequestBody transactionRequestBody);
    }
}
