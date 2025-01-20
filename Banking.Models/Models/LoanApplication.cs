//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace BankingWebApplication.Models
//{
//    public class LoanApplication
//    {
//        [Key]
//        public int LoanId { get; set; }
//        [ForeignKey("User")]
//        public int UserID { get; set; }
//        public User User { get; set; }

//        [StringLength(50)]
//        public string LoanType { get; set; }

//        [Column(TypeName = "decimal(15, 2)")]
//        public decimal LoanAmount { get; set; }

//        [Column(TypeName = "decimal(5, 2)")]
//        public decimal InterestRate { get; set; }

//        public int DurationMonths { get; set; }


//        [StringLength(20)]
//        public string LoanStatus { get; set; }

//        public DateTime AppliedAt { get; set; }=DateTime.Now;
//        public DateTime? ProcessedAt { get; set; }
//        public string? Remarks { get; set; }
//    }
//}
