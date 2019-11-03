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
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
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
    }
}
