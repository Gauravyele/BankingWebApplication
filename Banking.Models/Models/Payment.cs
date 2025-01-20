//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace BankingWebApplication.Models
//{
//    public class Payment
//    {
//        [Key]
//        public int PaymentId { get; set; }

//        [ForeignKey("Transaction")]
//        public int TransactionID { get; set; }
//        public Transaction Transaction { get; set; }

//        [ForeignKey("Loan")]
//        public int? LoanID { get; set; }
//        public LoanApplication Loan { get; set; }

//        [Column(TypeName = "decimal(15, 2)")]
//        public decimal Amount { get; set; }

//        [StringLength(20)]
//        public string PaymentType { get; set; }
//        public DateTime PaymentDate { get; set; } = DateTime.Now;

//        [StringLength(20)]
//        public string PaymentStatus { get; set; }

//    }
//}
