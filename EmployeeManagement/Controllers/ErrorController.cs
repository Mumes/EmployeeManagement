using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                switch (statusCode)
                {
                    case 404:
                        ViewBag.ErrorMessage = "Cannot find resorce";
                        break;
                    default:
                        ViewBag.ErrorMessage = "Unexpected error";
                        break;
                }
            }
           
            return View("NotFound");
        }

        [Route("/Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exeptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExeptionPath = exeptionDetails.Path;
            ViewBag.ExeptionMessage = exeptionDetails.Error.Message;
            ViewBag.ExeptionTrace = exeptionDetails.Error.StackTrace;
            return View("Error");
        }

    }
}
