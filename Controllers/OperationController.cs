using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBank.Models;
using SmartBank.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperationController : ControllerBase
    {
        private readonly IProfile _prof;
        private readonly ITransaction _transaction;
        private readonly IAccount _account;
        private readonly IDashboard _dash;

        public OperationController(IProfile profile,ITransaction transaction, IAccount account,IDashboard dash)
        {
            _prof = profile;
            _transaction = transaction;
            _account = account;
            _dash = dash;
        }


        [HttpPost]
        [Route("/create/profile")]
        public async Task<IActionResult> CreateProfile(ProfileModel profile)
        {
            try
            {
                var message = string.Empty;

                if(ModelState.IsValid)
                {
                     message = await _prof.CreateProfile(profile);
                }

                if(message == "success")
                {
                    return Ok(new { success = "Success: Profile was created" });
                }

                return BadRequest(new { failure = "Failure:Something has gone wrong" });

            }
            catch (Exception e)
            {

               return BadRequest(e.Message.ToString());
            }
        }


        [HttpPost]
        [Route("/create/account")]
        public async Task<IActionResult> CreateAccount(Account account)
        {
            try
            {
                var message = string.Empty;

                if (ModelState.IsValid)
                {
                    message = await _account.CreateAccount(account);
                }

                if (message == "success")
                {
                    return Ok(new { success = "Success: Account was created" });
                }

                return BadRequest(new { failure = "Failure:Something has gone wrong" });

            }
            catch (Exception e)
            {

                return BadRequest( new { failure = e.Message.ToString() });
            }
        }




        [HttpGet]
        [Route("/profile/{Id}")]
        public async Task<IActionResult> GetProfile(int Id)
        {
            try
            {
                var profileDb = await _prof.GetProfile(Id);
                return Ok(profileDb);
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, data = new object() });
            }

        }


        [HttpPost]
        [Route("/update/profile")]
        public async Task<IActionResult> UpdateProfile(Profile profile)
        {
            try
            {
                var result = await _prof.UpdateProfile(profile);
              
                return Ok(profile);
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, data = new object() });
            }
             
        }




        [HttpGet]
        [Route("/all/profiles")]
        public async Task<IActionResult> AllProfile()
        {
            try
            {
                var result = await _prof.GetProfiles();
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, data = new object() });
            }

        }



        [HttpPost]
        [Route("/credit/transaction")]
        public async Task<IActionResult> MakeCredit(Transaction transaction)
        {
            try
            {
                var result = await _transaction.MakeCreditTransaction(transaction);
                return Ok(new { Success = "Account was successfully credited" });
            }
            catch (Exception e)
            {

                return BadRequest( e.Message);
            }

        }


        [HttpPost]
        [Route("/debit/transaction")]
        public async Task<IActionResult> MakeDebit(Transaction transaction)
        {
            try
            {
                var result = await _transaction.MakeDebitTransaction(transaction);
                return Ok(new { Success = "Account was successfully debited" });
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }




        [HttpGet]
        [Route("/account/balance/{accountId}")]
        public async Task<IActionResult> AccountBalance(int accountId)
        {
            try
            {
                var result = await _transaction.GetAccountBalance(accountId);
                 string valueform =   String.Format("{0:n}", result);
                return Ok(new { message = "success", data = valueform });
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, data = new object() });
            }

        }


        [HttpGet]
        [Route("/account/{accountNo}")]
        public async Task<IActionResult> AccountDetail(string accountNo)
        {
            try
            {
                var result = await _account.AccountByAccountNo(accountNo);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, data = new object() });
            }

        }



        [HttpGet]
        [Route("/total/balance")]
        public async Task<IActionResult> TotalBalance()
        {
            try
            {
                var result = await  _transaction.TotalBalance();
                return Ok(new { message = "success", data = result });
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, data = new object() });
            }

        }





        [HttpGet]
        [Route("/all/transactions")]
        public async Task<IActionResult> AllTransactions()
        {
            try
            {
                var result = await _transaction.AllTransaction();
                return Ok(new { message = "success", data = result });
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, data = new object() });
            }

        }


        [HttpGet]
        [Route("/dashboard")]
        public async Task<IActionResult>Dashboard()
        {
            try
            {
                var result = await _dash.GetToDashboard();
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, dashboard = new object() });
            }
        }




        [HttpGet]
        [Route("/accounts")]
        public async Task<IActionResult> GetAccount()
        {
            try
            {
                var accounts = await _account.GetAccounts();
                return Ok(accounts);
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
