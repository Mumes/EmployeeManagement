using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController:Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository  = employeeRepository;
        }
        public ViewResult Index()
        {
            var model=employeeRepository.GetAllEmployees();
            return View(model);
        }
        public ViewResult Details(int id=1)
        {
            HomeDetailsViewModel hdv = new HomeDetailsViewModel()
            {
                PageTitle = "Employee details",
                Emp = employeeRepository.GetEmployee(id)
            };            
            return View(hdv);
        }
    }
}
