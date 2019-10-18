using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employeeList;
        public MockEmployeeRepository()
        {
            employeeList = new List<Employee>
            {
                new Employee{Id = 1,Name="Mary",Email = "mary@mail.ru",Department = "Sales"},
                new Employee{Id = 2,Name="John",Email = "jjn@mail.ru",Department = "Main"},
                new Employee{Id = 3,Name="Vicky",Email = "vik88@mail.ru",Department = "Constructor"},
                new Employee{Id = 4,Name="Richard",Email = "rich@mail.ru",Department = "Innovation"}

            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return employeeList.FirstOrDefault(c => c.Id == id);
        }
    }
}
