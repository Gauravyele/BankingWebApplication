//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace BankingWebApplication.Models
//{
//    public class Transaction
//    {
//        [Key]
//        public int TransactionId { get; set; }

//        [ForeignKey("Account")]
//        public int AccountId { get; set; }
//        public Account Account { get; set; }

//        [Required]
//        [StringLength(50)]
//        public string TransactionType { get; set; }

//        //[Required]
//        [Column(TypeName = "decimal(15, 2)")]
//        public decimal TransactionAmount { get; set; }

//        [StringLength(20)]
//        public string TransactionStatus { get; set; }
//        public DateTime CreatedAt { get; set; } = DateTime.Now;
//    }
//}
