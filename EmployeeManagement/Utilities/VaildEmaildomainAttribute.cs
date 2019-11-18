using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Utilities
{
    public class VaildEmaildomainAttribute:ValidationAttribute
    {
        private readonly string allowedDomain;

        public override bool IsValid(object value)
        {
          string[] strings =  value.ToString().Split('@');
          return  (strings[1].ToUpper().Trim() == allowedDomain.ToUpper());
           
        }
        public VaildEmaildomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
    }
}
