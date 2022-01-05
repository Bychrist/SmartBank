using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SmartBank.Repository.Interface
{
    public interface ITransaction
    {
        public Task<string> MakeCreditTransaction(Models.Transaction transaction);
        public Task<string> MakeDebitTransaction(Models.Transaction transaction);
        public Task<decimal> GetAccountBalance(int AccountId);
        public Task<decimal> TotalBalance();
        public Task<List<Models.Transaction>> AllTransaction();
    }
}
