﻿using BankingWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Banking.Models.Models.ViewModels
{
    public class RegisterViewModel
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

        [Required]
        [StringLength(100, ErrorMessage =" The {0} must be atleast {2} characters long.",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<SelectListItem>? RoleList { get; set; }
        [Display(Name ="Role")]
        public string RoleSelected { get; set; }

        public static explicit operator RegisterViewModel(ApplicationUser v)
        {
            throw new NotImplementedException();
        }
    }
}
