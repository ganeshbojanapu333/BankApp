using BankDomainLayer.TransactionDomains;
using BankServiceLayer.Interfaces;
using DataAccessLayer;

namespace BankServiceLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IDataAccess _dataAccess;

        public TransactionService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<bool> CreateTransaction(TransactionRequestBody transactionRequestBody)
        {
            try
            {
                var IsSuccess= _dataAccess.MakeAccountTransaction(transactionRequestBody);
                return Task.FromResult(IsSuccess);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
