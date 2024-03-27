using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.DAL.Models;
using System;

namespace Route.C41.MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;

        public IWebHostEnvironment Env { get; }

        public EmployeeController(IEmployeeRepo employeeRepo, IWebHostEnvironment env)
        {
            _employeeRepo = employeeRepo;
            Env = env;
        }
        public IActionResult Index()
        {
            //Binding
            ViewData["massage"] = "hello viewData";
            ViewBag.massage = "hello viewBag";
            var department = _employeeRepo.GetAll();
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                {
                    TempData["massage"] = "created successfully";
                }
                else
                {
                    TempData["massage"] = "not created";
                }
                return RedirectToAction(nameof(Index));


            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult details(int? id, string view = "details")
        {
            if (!id.HasValue)
                return BadRequest();
            var employee = _employeeRepo.Get(id.Value);
            if (employee == null)
                return NotFound();

            return View(view, employee);
        }
        [HttpGet]
        public IActionResult edit(int? id)
        {

            return details(id, "edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult edit([FromRoute] int Id, Employee employee)
        {
            if (Id != employee.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                _employeeRepo.Update(employee);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                if (Env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "updating error");
                }
                return View(employee);
            }
        }
        [HttpGet]
        public IActionResult delete(int? id)
        {

            return details(id, "delete");
        }
        [HttpPost]
        public IActionResult delete(Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                _employeeRepo.Delete(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "deleting error");
                }
                return View(employee);
            }
        }
    }
}
