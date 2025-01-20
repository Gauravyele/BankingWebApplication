using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Models.Models.ViewModels
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "User Name is required.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(10, ErrorMessage = "Phone Number cannot be longer than 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Aadhar Number")]
        [StringLength(12, ErrorMessage = "Aadhar Number cannot be longer than 12 digits.")]
        public string AadharNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
