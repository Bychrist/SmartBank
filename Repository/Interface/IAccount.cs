using SmartBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Repository.Interface
{
    public interface IAccount
    {
        public Task<string> CreateAccount(Account account);
        public Task<Account> AccountByAccountNo(string accountNo);
        public Task<List<Account>> GetAccounts();
    }
}
