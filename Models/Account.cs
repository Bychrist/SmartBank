using SmartBank.Utiltiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Models
{
    public class Account
    {
      
        public int Id { get; set; }

        [ForeignKey("Profile")]
        public int ProfileId { get; set; }

        public string AccountNo { get; set; } = DateTime.Today.Ticks.ToString().Substring(0, 7) + DataModifier.GenerateRandom();

        public string AccountType = "Customer";

        public DateTime Created_at { get; set; } = DateTime.Now;

        public Profile Profile { get; set; }

    }


}

