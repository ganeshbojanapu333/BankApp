using BankApp.Controllers;
using BankDomainLayer.AccountDomains;
using BankDomainLayer.CommonDomains;
using BankDomainLayer.CustomerDomains;
using BankServiceLayer.Interfaces;
using BankXUnitTest.ControllersMockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankXUnitTest.TestControllers
{
    public class AccountControllerTest
    {
        private readonly Mock<IAccountService> _mockAccountService = new();
        private readonly AccountController _accountcontroller;
        public AccountControllerTest()
        {
            _accountcontroller = new AccountController(_mockAccountService.Object);
        }

        [Fact]
        public async Task GetAccountsReturnsAccountsResponse200()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).Returns(AccountControllerMockData.GetAllAccountsMockData());
            //Act
            var result = await _accountcontroller.GetAccounts();
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAccountsZeroCountReturnsResponseNoContent204()
        {
            ///Arrange
            IEnumerable<Account> testAccounts = new List<Account>();
            var testAccountsResult = await Task.FromResult(testAccounts);
            _mockAccountService.Setup(x => x.GetAccountsData()).ReturnsAsync(testAccountsResult);
            //Act
            var result = await _accountcontroller.GetAccounts();
            var noContentResult = result as NoContentResult;
            //Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task GetAccountsHandleExceptionReturnsInternalServer500()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).ThrowsAsync(new Exception());
            //Act
            var result = await _accountcontroller.GetAccounts();
            var noContentResult = result as ObjectResult;
            //Assert
            Assert.Equal(500, noContentResult.StatusCode);
        }


        [Fact]
        [Route("GetAccount/{id:int}")]
        public async Task GetAccountDetailsbyAccountIdResponse200()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).Returns(AccountControllerMockData.GetAllAccountsMockData());
            //Act
            var result = await _accountcontroller.GetAccountDetailsbyAccountId(1);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        [Route("GetAccount/{id:int}")]
        public async Task GetAccountDetailsbyNegativetIdInvalidIdReturnsBadRequest400()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).Returns(AccountControllerMockData.GetAllAccountsMockData());
            //Act
            var result = await _accountcontroller.GetAccountDetailsbyAccountId(-100);
            var badResult = result as BadRequestObjectResult;
            //Assert
            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
        }

        [Fact]
        [Route("GetAccount/{id:int}")]
        public async Task GetAccountDetailsbyAccountIdInvalidIdReturnsNotFound404()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).Returns(AccountControllerMockData.GetAllAccountsMockData());
            //Act
            var result = await _accountcontroller.GetAccountDetailsbyAccountId(30);
            var notFoundResult = result as NotFoundObjectResult;
            //Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        [Route("CreateAccount")]
        public async Task CreateNewAccountReturnsItemCreated201()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).Returns(AccountControllerMockData.GetAllAccountsMockData());
            Customer customer = new();
            customer.FirstName = "testFirst";
            customer.LastName = "testLast";
            customer.Id = 1;
            Account newAccount = new Account()
            {
                Customer = customer,
                AccountId = 5,
                AccountName = "tEST Savings Account",
                AccountNumber = Guid.NewGuid().ToString(),
                AccountType = (AccountType)1,
                CurrentBalance = 10000,
                Email = "test123@Bank.com",
                PhoneNumber = "9877654123",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };
            AccountRequestBody accountRequestBody = new()
            {
                FirstName = "testFirst",
                LastName = "testLast",
                AccountName = "tEST Savings Account",
                AccountType = (AccountType)1,
                CurrentBalance = 1000,
                Email = "test123@Bank.com",
                PhoneNumber = "9877654123",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };

            _mockAccountService.Setup(x => x.CreateNewAccount(It.IsAny<Account>())).ReturnsAsync(newAccount);
            //Act
            var result = await _accountcontroller.CreateNewAccount(accountRequestBody);
            var objResult = result as ObjectResult;
            //Assert
            Assert.NotNull(objResult);
            Assert.Equal(201, objResult.StatusCode);
        }

        [Fact]
        [Route("CreateAccount")]
        public async Task CreateNewAccountPassNullRequestBodyReturnsBadRequest400()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).Returns(AccountControllerMockData.GetAllAccountsMockData());
            Customer customer = new();
            customer.FirstName = "testFirst";
            customer.LastName = "testLast";
            customer.Id = 1;
            Account newAccount = new Account()
            {
                Customer = customer,
                AccountId = 5,
                AccountName = "tEST Savings Account",
                AccountNumber = Guid.NewGuid().ToString(),
                AccountType = (AccountType)1,
                CurrentBalance = 10000,
                Email = "test123@Bank.com",
                PhoneNumber = "9877654123",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };
            AccountRequestBody accountRequestBody = null;

            _mockAccountService.Setup(x => x.CreateNewAccount(It.IsAny<Account>())).ReturnsAsync(newAccount);
            //Act
            var result = await _accountcontroller.CreateNewAccount(accountRequestBody);
            var badResult = result as BadRequestObjectResult;
            //Assert
            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
        }

        [Fact]
        [Route("CreateAccount")]
        public async Task CreateNewAccountWithBalanceBelow100ReturnsErrorMessage()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).Returns(AccountControllerMockData.GetAllAccountsMockData());
            Customer customer = new();
            customer.FirstName = "testFirst";
            customer.LastName = "testLast";
            customer.Id = 1;
            Account newAccount = new Account()
            {
                Customer = customer,
                AccountId = 5,
                AccountName = "tEST Savings Account",
                AccountNumber = Guid.NewGuid().ToString(),
                AccountType = (AccountType)1,
                CurrentBalance = 80,
                Email = "test123@Bank.com",
                PhoneNumber = "9877654123",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };
            AccountRequestBody accountRequestBody = new()
            {
                FirstName = "testFirst",
                LastName = "testLast",
                AccountName = "tEST Savings Account",
                AccountType = (AccountType)1,
                CurrentBalance = 80,
                Email = "test123@Bank.com",
                PhoneNumber = "9877654123",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };

            _mockAccountService.Setup(x => x.CreateNewAccount(It.IsAny<Account>())).ReturnsAsync(newAccount);
            //Act
            var result = await _accountcontroller.CreateNewAccount(accountRequestBody);
            //Assert
            Assert.Contains(ValidateModel(accountRequestBody), v => v.MemberNames.Contains("CurrentBalance") &&
              v.ErrorMessage.Contains("The field CurrentBalance must be between 100 and 10000."));
        }

        [Fact]
        [Route("CreateAccount")]
        public async Task CreateNewAccountWithDepositBalanceAbove10000ReturnsErrorMessage()
        {
            ///Arrange
            _mockAccountService.Setup(x => x.GetAccountsData()).Returns(AccountControllerMockData.GetAllAccountsMockData());
            Customer customer = new();
            customer.FirstName = "testFirst";
            customer.LastName = "testLast";
            customer.Id = 1;
            Account newAccount = new Account()
            {
                Customer = customer,
                AccountId = 5,
                AccountName = "tEST Savings Account",
                AccountNumber = Guid.NewGuid().ToString(),
                AccountType = (AccountType)1,
                CurrentBalance = 15000,
                Email = "test123@Bank.com",
                PhoneNumber = "9877654123",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };
            AccountRequestBody accountRequestBody = new()
            {
                FirstName = "testFirst",
                LastName = "testLast",
                AccountName = "tEST Savings Account",
                AccountType = (AccountType)1,
                CurrentBalance = 15000,
                Email = "test123@Bank.com",
                PhoneNumber = "9877654123",
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };

            _mockAccountService.Setup(x => x.CreateNewAccount(It.IsAny<Account>())).ReturnsAsync(newAccount);
            //Act
            var result = await _accountcontroller.CreateNewAccount(accountRequestBody);
            //Assert
            Assert.Contains(ValidateModel(accountRequestBody), v => v.MemberNames.Contains("CurrentBalance") &&
              v.ErrorMessage.Contains("The field CurrentBalance must be between 100 and 10000."));
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
