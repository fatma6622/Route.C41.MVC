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
        private readonly IEmployeeRepo _EmployeeRepo;

        public IWebHostEnvironment Env { get; }

        public EmployeeController(IEmployeeRepo EmployeeRepo, IWebHostEnvironment env)
        {
            _EmployeeRepo = EmployeeRepo;
            Env = env;
        }
        public IActionResult Index()
        {
            var Employee = _EmployeeRepo.GetAll();
            return View(Employee);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                var count = _EmployeeRepo.Add(Employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(Employee);
        }
        [HttpGet]
        public IActionResult details(int? id, string view = "details")
        {
            if (!id.HasValue)
                return BadRequest();
            var Employee = _EmployeeRepo.Get(id.Value);
            if (Employee == null)
                return NotFound();

            return View(view, Employee);
        }
        [HttpGet]
        public IActionResult edit(int? id)
        {

            return details(id, "edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult edit([FromRoute] int Id, Employee Employee)
        {
            if (Id != Employee.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                _EmployeeRepo.Update(Employee);
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
                return View(Employee);
            }
        }
        [HttpGet]
        public IActionResult delete(int? id)
        {

            return details(id, "delete");
        }
        [HttpPost]
        public IActionResult delete(Employee Employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                _EmployeeRepo.Delete(Employee);
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
                    ModelState.AddModelError(string.Empty, "deleting error");
                }
                return View(Employee);
            }
        }
    }
}
