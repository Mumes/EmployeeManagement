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
                new Employee{Id = 1,Name="Mary",Email = "mary@mail.ru",Department = Dept.Sales},
                new Employee{Id = 2,Name="John",Email = "jjn@mail.ru",Department = Dept.Main},
                new Employee{Id = 3,Name="Vicky",Email = "vik88@mail.ru",Department = Dept.Constructor},
                new Employee{Id = 4,Name="Richard",Email = "rich@mail.ru",Department = Dept.Innovation}

            };
        }

        public Employee Add(Employee emp)
        {
            emp.Id=employeeList.Max(e => e.Id)+1;
            employeeList.Add(emp);
            return emp;
        }

        public Employee Delete(int id)
        {
            Employee emp = employeeList.FirstOrDefault(e => e.Id == id);
            if(emp!=null) employeeList.Remove(emp);
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return employeeList.FirstOrDefault(c => c.Id == id);
        }

        public Employee Update(Employee changesEmployee)
        {
            Employee emp = employeeList.FirstOrDefault(e => e.Id == changesEmployee.Id);
            if (emp != null)
            {
                emp.Email = changesEmployee.Email;
                emp.Name = changesEmployee.Name;
                emp.Department = changesEmployee.Department;
            }
            return emp;
        }
    }
}
