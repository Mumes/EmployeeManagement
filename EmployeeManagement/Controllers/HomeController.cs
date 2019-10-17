﻿using EmployeeManagement.Models;
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

        public string Index()
        {
            
            return employeeRepository.GetEmployee(1).Name;
        }
        public ViewResult Details()
        {
            Employee model = employeeRepository.GetEmployee(1);
            ViewData["Employee"] = model;
            ViewData["PageTitle"] = "Employee details";
            return View(model);

        }
    }
}
