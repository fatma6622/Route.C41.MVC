using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.DAL.Models;
using System;

namespace Route.C41.MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo _departmentRepo;

        public IWebHostEnvironment Env { get; }

        public DepartmentController(IDepartmentRepo departmentRepo,IWebHostEnvironment env)
        {
            _departmentRepo=departmentRepo;
            Env = env;
        }
        public IActionResult Index()
        {
            var department=_departmentRepo.GetAll();
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                var count=_departmentRepo.Add(department);
                if(count>0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(department);
        }
        [HttpGet]
        public IActionResult details(int? id, string view = "details")
        {
            if (!id.HasValue)
                return BadRequest();
            var department=_departmentRepo.Get(id.Value);
            if(department==null)
                return NotFound();

            return View(view,department);
        }
        [HttpGet]
        public IActionResult edit(int? id)
        {

            return details(id, "edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult edit([FromRoute] int Id, Department department)
        {
            if(Id!=department.Id)
                return BadRequest();
            if(!ModelState.IsValid)
                return BadRequest();
            try
            {
                _departmentRepo.Update(department);
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
                return View(department);
            }
        }
        [HttpGet]
        public IActionResult delete(int? id)
        {

            return details(id, "delete");
        } 
        [HttpPost]
        public IActionResult delete(Department department)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                _departmentRepo.Delete(department);
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
                return View(department);
            }
        }
    }
}
