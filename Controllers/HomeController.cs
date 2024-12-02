using hw7_1.Models;
using hw7_1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hw7_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService employeeService;
        public HomeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View(employeeService.Employees);
        }
        [HttpPost]
        public IActionResult Index(IEnumerable<Employee> employees)
        {
            employeeService.Employees = employees.ToList();
            return RedirectToAction(nameof(Index));
        }


    }
}
