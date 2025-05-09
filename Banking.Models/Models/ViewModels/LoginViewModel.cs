﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Models.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        //[EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
     

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
