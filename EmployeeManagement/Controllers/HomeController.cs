using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        private readonly IWebHostEnvironment hostingEnviroment;
        private readonly ILogger<HomeController> logger;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnviroment, ILogger<HomeController> logger)
        {
           
            this.employeeRepository = employeeRepository;
            this.hostingEnviroment = hostingEnviroment;
            this.logger = logger;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = employeeRepository.GetAllEmployees();
            return View(model);
        }
        [AllowAnonymous]
        public ViewResult Details(int id = 1)
        {
            var emp = employeeRepository.GetEmployee(id);
            if (emp ==null)
            {
                Response.StatusCode = 404;
               // return View("EmployeeNotFound",id);
            }
            HomeDetailsViewModel hdv = new HomeDetailsViewModel()
            {
                PageTitle = "Employee details",
                Emp = employeeRepository.GetEmployee(id)
            };
            return View(hdv);
        }
        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqFileName = ProccessFileName(model);
                var emp = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,                  
                    PhotoPath = uniqFileName
                };
                employeeRepository.Add(emp);
                return RedirectToAction("details",new { id = emp.Id });
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ViewResult Edit(int id)
        {
            var emp = employeeRepository.GetEmployee(id);

            var empViewModel = new EmployeeEditViewModel
            {
                Id = emp.Id,
                Name = emp.Name,
                PhotoPath = emp.PhotoPath,
                Email = emp.Email,
                Department = emp.Department,               
                
            };
            return View(empViewModel);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var emp = employeeRepository.GetEmployee(model.Id);
                emp.Name = model.Name;
                emp.Email = model.Email;
                emp.Department = model.Department;
                emp.PhotoPath = model.PhotoPath;

                if (model.Photo != null)
                {
                    if(model.PhotoPath!=null)
                    {
                        var filePath=Path.Combine(hostingEnviroment.WebRootPath,
                            "images", model.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    emp.PhotoPath = ProccessFileName(model);
                }
                employeeRepository.Update(emp);
                return RedirectToAction("details", new { id = emp.Id });
            }
            return View();
        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            var emp = employeeRepository.GetEmployee(id);
            employeeRepository.Delete(id);               
            if (emp.PhotoPath != null)
            {
                var filePath = Path.Combine(hostingEnviroment.WebRootPath,
                    "images", emp.PhotoPath);
                System.IO.File.Delete(filePath);
            }
            return RedirectToAction("index");           
        }
        private  string ProccessFileName(EmployeeCreateViewModel model)
        {
            string uniqFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                uniqFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqFileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                { 
                    model.Photo.CopyTo(fs);
                }                  
            }
            return uniqFileName;
        }
    }
}
