using EmployeeManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class SecondController:Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment hostingEnviroment;

        public SecondController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnviroment)
        {
            this.employeeRepository = employeeRepository;
            this.hostingEnviroment = hostingEnviroment;
        }


        public IActionResult Page(int id)
        {
            return View(id);
        }
    }
}
