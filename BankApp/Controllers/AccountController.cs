using BankDomainLayer.AccountDomains;
using BankDomainLayer.CustomerDomains;
using BankServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("GetAllAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
               var accounts=await _accountService.GetAccountsData();
                if (accounts == null || accounts.Count()==0)
                {
                    return NoContent();
                }
                return Ok(accounts);
            }
            catch (Exception)
            {

                return StatusCode(500, "There is an error while getting Accounts");
            }
        }

        [HttpGet]
        [Route("GetCustomerAccounts/{id:int}")]
        public async Task<IActionResult> GetUserAccountsByCustomerId([FromRoute] int id)
        {
            try
            {
                //var customerId = Request.RouteValues["id"];
                if (id < 1) 
                {
                    return BadRequest();
                }
                var accounts = await _accountService.GetAccountsData();
                if (accounts == null)
                {
                    return NoContent();
                }
                var userSpecificAccounts = accounts.Where(x => x.Customer.Id== Convert.ToInt32(id));
                if (userSpecificAccounts == null || userSpecificAccounts.Count()==0) 
                {
                    return NotFound("There are no Accounts for the user Id: "+id.ToString());
                }
                return Ok(userSpecificAccounts);
            }
            catch (Exception)
            {

                return StatusCode(500,"There is an error while getting Accounts");
            }
        }
        [HttpGet]
        [Route("GetAccount/{id:int}")]
        public async Task<IActionResult> GetAccountDetailsbyAccountId([FromRoute]int id)
        {
            try
            {
                //var accountId = Request.RouteValues["id"];
                if ( id < 1)
                {
                    return BadRequest("Invalid Account Id.");
                }
                var accounts = await _accountService.GetAccountsData();
                if (accounts == null)
                {
                    return NoContent();
                }
                var specificAccount = accounts.FirstOrDefault(x => x.AccountId == Convert.ToInt32(id));
                if (specificAccount == null )
                {
                    return NotFound("There is no Account with AccountId: " + id.ToString()+". Please provide valid Account Id");
                }
                return Ok(specificAccount);
            }
            catch (Exception)
            {

                return StatusCode(500, "There is an error while getting Account details");
            }
           
        }

        [HttpPost]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateNewAccount([FromBody]AccountRequestBody accountRequestBody)
        {
            try
            {
                if (accountRequestBody == null)
                {
                    return BadRequest("Plz Provide valid Account details");
                }
                else 
                {
                    var accounts = await _accountService.GetAccountsData();
                    if (accounts != null)
                    {
                        var sameNameAccount=accounts.SingleOrDefault(x=>x.AccountName==accountRequestBody.AccountName);
                        if (sameNameAccount != null) 
                        {
                            return BadRequest("There is an Account with the same as " + accountRequestBody.AccountName + ". Plz try another Account Name");
                        }
                    }
                    var newAccountData = new Account() 
                    { 
                        AccountName = accountRequestBody.AccountName,
                        PhoneNumber = accountRequestBody.PhoneNumber,
                        AccountType = accountRequestBody.AccountType,
                        Created = accountRequestBody.Created,
                        LastUpdated=accountRequestBody.LastUpdated,
                        CurrentBalance=accountRequestBody.CurrentBalance,
                        Email= accountRequestBody.Email,
                    };            
                    var newCustomer = new Customer() { FirstName = accountRequestBody.FirstName, LastName = accountRequestBody.LastName };
                    newAccountData.Customer = newCustomer;
                    var createdAccount=await _accountService.CreateNewAccount(newAccountData);
                    string uri = $"http://localhost:5026/api/Account/GetAccount/{createdAccount.AccountId}";
                    return Created(uri, createdAccount);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpDelete]
        [Route("DeleteAccount/{id:int}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            try
            {
                //var accountId = Request.RouteValues["id"];
                if (id < 1)
                {
                    return BadRequest("Invalid Account Id.");
                }
                var accounts = await _accountService.GetAccountsData();
                if (accounts == null)
                {
                    return NoContent();
                }
                var specificAccount = accounts.SingleOrDefault(x => x.AccountId == Convert.ToInt32(id));
                if (specificAccount == null)
                {
                    return NotFound("There is no Account with AccountId: " + id.ToString() + ". Please provide valid Account Id");
                }
                else 
                {
                    return Ok(await _accountService.DeleteAccount(specificAccount.AccountId));
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
