using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        

        public Employee Add(Employee emp)
        {
            context.Add(emp);
            context.SaveChanges();
            return emp;
        }

        public Employee Delete(int id)
        {
           var emp= context.Employees.Find(id);
            if (emp != null)
                context.Employees.Remove(emp);
            context.SaveChanges();

            return emp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            var emp = context.Employees.Find(id);
            return emp;
        }

        public Employee Update(Employee changesEmployee)
        {
            var emp = context.Attach(changesEmployee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changesEmployee;
        }
    }
}
