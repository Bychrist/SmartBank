using Microsoft.EntityFrameworkCore;
using SmartBank.Models;
using SmartBank.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SmartBank.Repository.Function
{
    public class MTransaction : ITransaction
    {
        private readonly AuthenticationContext _db;

        public MTransaction(AuthenticationContext db)
        {
            _db = db;
        }

     
        public async Task<decimal> GetAccountBalance(int AccountId)
        {
            try
            {
                var getTrans = await _db.Transactions.Where(t => t.AccountId == AccountId).ToListAsync();
                var credit = getTrans.Sum(p => p.Credit);
                var debit = getTrans.Sum(p => p.Debit);
                return credit - debit;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<string> MakeCreditTransaction(Models.Transaction transaction)
        {
                try
                {
                        Models.Transaction tran = new Models.Transaction();
                        tran.AccountId = transaction.AccountId;
                        tran.Credit = transaction.Credit;
                        await  _db.AddAsync(tran);
                        await  _db.SaveChangesAsync();

                        return "success";

                }
                catch (Exception e)
                {

                    throw;
                }

        }

        public async Task<string> MakeDebitTransaction(Models.Transaction transaction)
        {
            try
            {

                Models.Transaction tran = new Models.Transaction();
                tran.AccountId = transaction.AccountId;
                tran.Debit = transaction.Debit;
                await _db.AddAsync(tran);
                await _db.SaveChangesAsync();
                return "success";

            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task<decimal> TotalBalance()
        {
            try
            {
                var allTrans = await _db.Transactions.ToListAsync();
                return allTrans.Sum(a => a.Credit) - allTrans.Sum(d => d.Debit);

            }
            catch (Exception )
            {

                throw;
            }
        }

        public async Task<List<Models.Transaction>> AllTransaction()
        {
            try
            {
                var transactions = await _db.Transactions.Include(a=>a.Account.Profile).ToListAsync();
                return transactions;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }





}
