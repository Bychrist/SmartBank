using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Models
{
    public class Dashboard
    {
        public int CustomerNumber { get; set; }
        public int CreditAccountNoToday { get; set; }
        public int DebitAccountNoToday { get; set; }
        public decimal AmountDebitedToday { get; set; }
        public decimal AmountCreditedToday { get; set; }
        public decimal AccountBalance { get; set; }
        public List<Transaction> TransactionsToday { get; set; }
    }
}
