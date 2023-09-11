using BankDomainLayer.CommonDomains;
using BankDomainLayer.TransactionDomains;
using BankServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public TransactionController(IAccountService accountService,ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }
        [HttpPut]
        [Route("MakeTransaction")]
        public async Task<IActionResult> MakeTransaction([FromBody] TransactionRequestBody transactionRequestBody)
        {
            try
            {
                if (transactionRequestBody == null)
                {
                    return BadRequest("Plz Provide valid Account details");
                }
                else
                {
                    var accounts = await _accountService.GetAccountsData();
                    if (accounts != null)
                    {
                        var transactionAccount = accounts.SingleOrDefault(x => x.AccountId == transactionRequestBody.TransactionAccountId);
                        if (transactionAccount == null)
                        {
                            return NotFound("There is no Account with Account Id" + transactionRequestBody.TransactionAccountId);
                        }
                        else 
                        {
                            if (transactionRequestBody.transactionType == TransactionType.withdrawl)
                            {
                                var currentBalance = transactionAccount.CurrentBalance;
                                if (currentBalance < transactionRequestBody.Amount)
                                {
                                    return BadRequest("Cannot withdraw amount as your Current balnce" + currentBalance.ToString() + " is lower than requested withdrawl amount " + transactionRequestBody.Amount.ToString());
                                }
                                decimal minimumBalance = 100;
                                if (currentBalance - transactionRequestBody.Amount <= minimumBalance)
                                {
                                    return BadRequest("Cannot withdraw amount as you are reaching below minimum balance limit of: $" + minimumBalance.ToString());
                                }
                                decimal max90LimitWithdrawlAmount = (decimal)(0.9 * (double)currentBalance);
                                if (transactionRequestBody.Amount > max90LimitWithdrawlAmount)
                                {
                                    return BadRequest("Cannot withdraw more than 90% amount i.e.  " + max90LimitWithdrawlAmount.ToString() + " of your current Balance in a single transaction.");
                                }
                            }
                            else if (transactionRequestBody.transactionType == TransactionType.deposit) 
                            {
                                if (transactionRequestBody.Amount > 10000) 
                                {
                                    return BadRequest("Cannot deposit more than $10000 in a single transaction");
                                }
                            }
                            else 
                            {
                                return BadRequest("Invalid Transaction Type. Transaction type value should be either deposit as 1 or withdrawl as 2.");
                            }
                        }
                        var isTransactionSucess=await _transactionService.CreateTransaction(transactionRequestBody);
                        if (isTransactionSucess)
                        {
                            return Ok("Transaction is successful and updated your Current Balance.");
                        }
                        else 
                        {
                            return StatusCode(500, "There is an error while making Transaction request.");
                        }
                    }
                    else 
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "There is an error while making Transaction request.");
            }

        }
    }
}
