using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingWebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {

        [StringLength(20)] //admin or customer - can we include enum?
        public string? Role { get; set; } //at present nullable

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(12)]
        public string AadharNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserId { get; set; }
      
        [StringLength(12)]
        public string AccountNumber { get; set; }

    }
}
