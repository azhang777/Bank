using AutoMapper;
using BankOfMikaila.Controllers;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Response;
using BankOfMikaila.Response.Format;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Controllers
{
    public class AccountControllerTest
    {
        private readonly AccountResponse _accountResponse;
        private readonly CustomerResponse _customerResponse;

        public AccountControllerTest()
        {
            _accountResponse = A.Fake<AccountResponse>();
            _customerResponse = A.Fake<CustomerResponse>();
        }

        [Test]
        public void AccountController_GetAllAccounts_ReturnOK()
        {
            var accounts = A.Fake<List<Account>>();
            var accountsConverted = A.Fake<IEnumerable<AccountDTO>>();
            
            //Arrange
            //creating a fake output that is produced from a mock of the accountResponse
            var response = new DataResponse
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Accounts retrieved",
                Data = accountsConverted
            };
            //CallTo is used for setting up behavior on fake objects, not directly for setting up data. It is used to specify what should happen when a mathod is called on a fake object.
            //In this case, we are setting up GetAllAccounts() method on the fake _accountResponse. Whenever GetAllAccounts of _accountResponse is called, it should return the faked data response.
            A.CallTo(() => _accountResponse.GetAllAccounts()).Returns(response); //this only allows me to pass in DataResponse bc it recognizes the return type of accountResponse.GetAllAccounts()
            var controller = new AccountController(_accountResponse, _customerResponse); //set up the controller with its mocked dependencies
            
            //Act
            var result = controller.GetAllAccounts();  //when this is called, the mocked _accountResponse is used, which returns the fake response.
            
            //Assert
            Assert.That(result.Value.Code, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(result.Value.Data, Is.SameAs(accountsConverted));
        }

    }
}
