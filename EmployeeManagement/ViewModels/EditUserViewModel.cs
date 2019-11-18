using EmployeeManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }

        [Display(Name = "User name")]      
        public string UserName { get; set; }
        [Required]
        [Display(Name = "User email")]       
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        [VaildEmaildomain(allowedDomain: "gmail.com", ErrorMessage = "email must be from gmail domain")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}
