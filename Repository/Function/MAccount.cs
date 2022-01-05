using Microsoft.EntityFrameworkCore;
using SmartBank.Models;
using SmartBank.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Repository.Function
{
    public class MAccount : IAccount
    {
        private readonly AuthenticationContext _db;

        public MAccount(AuthenticationContext db)
        {
            _db = db;

        }

        public async Task<Account> AccountByAccountNo(string accountNo)
        {
            try
            {
                var result = await _db.Accounts.Include(a => a.Profile).Where(a => a.AccountNo == accountNo).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception )
            {

                throw;
            }
        }

        public async Task<string> CreateAccount(Account account)
        {
            try
            {
                var checkAccount = _db.Accounts.Where(a => a.AccountNo == account.AccountNo).FirstOrDefault();
                    if(checkAccount != null)
                    {
                          return new Exception("Account number exist").ToString();
                    }


                await _db.AddAsync(account);
                await _db.SaveChangesAsync();
                return "success";
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<Account>> GetAccounts()
        {
            try
            {
                return await _db.Accounts.OrderByDescending(c => c.Created_at).Include(p=>p.Profile).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
