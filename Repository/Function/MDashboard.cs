using Microsoft.EntityFrameworkCore;
using SmartBank.Models;
using SmartBank.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Repository.Function
{
    public class MDashboard : IDashboard
    {
        private readonly AuthenticationContext _db;
        private readonly IProfile _profile;
        private readonly ITransaction _itransaction;

        public MDashboard(AuthenticationContext db,IProfile profile,ITransaction transaction)
        {
            _db = db;
            _profile = profile;
            _itransaction = transaction;
        }
        public async Task<Dashboard> GetToDashboard()
        {
            try
            {
                var transactions = await _db.Transactions.ToListAsync();
                var totalCredit = transactions.Sum(p => p.Credit);
                var totalDebit = transactions.Sum(p => p.Debit);
                var balance = totalCredit - totalDebit;
                var profiles =  _profile.GetProfiles().Result.Count();
                var tran = _itransaction.AllTransaction().Result.Where(t => t.Created_At >= DateTime.Today && t.Created_At < DateTime.Today.AddDays(1)).OrderByDescending(p=>p.Created_At).ToList();
                var creditedToday = tran.Sum(t => t.Credit);
                var debitedToday = tran.Sum(t => t.Debit);
                var noOfDebitedAccount = tran.Where(t => t.Debit != 0).ToList().Count();
                var noOfCreditedAccount = tran.Where(t => t.Credit != 0).ToList().Count();
                var dashboard = new Dashboard
                {
                    AccountBalance = balance,
                    CustomerNumber = profiles,
                    TransactionsToday = tran,
                    AmountCreditedToday = creditedToday,
                    AmountDebitedToday = debitedToday,
                    CreditAccountNoToday = noOfCreditedAccount,
                    DebitAccountNoToday = noOfDebitedAccount

                };


                return dashboard;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
