using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtention
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Department = Dept.Constructor,
                    Name = "Mary",
                    Email = "test@mail.ru"
                },
                new Employee
                {
                    Id = 2,
                    Department = Dept.Innovation,
                    Name = "John",
                    Email = "jjn@mail.ru"
                }
              );
        }
    }
}
