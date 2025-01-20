//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace BankingWebApplication.Models
//{
//    public class Account
//    {
//        [Key]
//        public int AccountId { get; set; }

//        [ForeignKey("ApplicationUser")]
//        public string Id { get; set; }

//        [ForeignKey("ApplicationUser")]
//        public string AccountNumber { get; set; }
//        public ApplicationUser ApplicationUser { get; set; } //navigation property

//        [StringLength(20)]
//        public string AccountType { get; set; }

//        [Column(TypeName = "decimal(15, 2)")]
//        public decimal Balance { get; set; } = 0.00M;
//        //M suffix is used to denote a decimal type, which is a high-precision floating-point number
//        //suitable for financial and monetary calculations.

//        [ForeignKey("Branch")]
//        public int BranchId { get; set; }
//        public Branch Branch { get; set; } //This defines a navigation property named Branch of type Branch
//                                           //in the Account class. It allows you to navigate from an Account to its related Branch.
//        public DateTime CreatedAt { get; set; } = DateTime.Now;
//    }
//}
