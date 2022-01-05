using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Models
{
    public class ProfileModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Range(0, Int64.MaxValue, ErrorMessage = "Contact number should not contain characters")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "Contact number should have minimum 11 digits")]
        public string PhoneNumber { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;

        public List<Account> Accounts { get; set; }
    }
}
