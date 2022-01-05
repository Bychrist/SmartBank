using SmartBank.Utiltiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Credit { get; set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Debit { get; set; }

        public long TransactionRef { get; set; } = DataModifier.GenerateRandomNumber();
        public DateTime Created_At { get; set; } = DateTime.Now;
        public Account Account { get; set; }


    }
}

