using EmployeeManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class RegisterViewModel
    {
        
        [Required]
        [Display(Name = "Your Email")]
        [Remote(action:"IsEmailInUse",controller:"account")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        [VaildEmaildomain(allowedDomain:"gmail.com",ErrorMessage ="email must be from gmail domain")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password, ErrorMessage = "Email is not valid")]
        
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password, ErrorMessage = "Email is not valid")]
        [Compare("Password", ErrorMessage ="Incorrect password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
    }
}
