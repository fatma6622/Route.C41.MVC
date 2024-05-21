using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.BLL.Interfaces;
using Route.C41.MVC.DAL.Models;
using Route.C41.MVC.PL.Helpers;
using Route.C41.MVC.PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.MVC.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepo _employeeRepo;

        public IWebHostEnvironment Env { get; }

        public EmployeeController(IMapper mapper,/*IEmployeeRepo employeeRepo*/
            IUnitOfWork unitOfWork
            , IWebHostEnvironment env)
        {
            this.mapper = mapper;
            _unitOfWork = unitOfWork;
            //_employeeRepo = employeeRepo;
            Env = env;
        }
        public async Task<IActionResult> Index(string searchInp)
        {
            var employees=Enumerable.Empty<Employee>();
            var employeeRepo = _unitOfWork.Repo<Employee>() as IEmployeeRepo;
            if (string.IsNullOrEmpty(searchInp))
            {
                //Binding
                //ViewData["massage"] = "hello viewData";
                //ViewBag.massage = "hello viewBag";
                employees = await employeeRepo.GetAllAsync();
            }
            else
            {
                employees= employeeRepo.GetByName(searchInp.ToLower());
            }
            var mappedEmps = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmps);

        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName= await DocumentSettings.UploadFileAsync(employeeVM.Image, "Images");

                //manual mapping
                //var mappedEmp = new Employee()
                //{
                //    Name = employeeVM.Name,
                //    Age = employeeVM.Age,
                //    Address = employeeVM.Address,
                //    salary = employeeVM.salary,
                //    Email = employeeVM.Email,
                //    phone = employeeVM.phone,
                //    isActive = employeeVM.isActive,
                //    hirringDate = employeeVM.hirringDate
                //};
                //Employee mappedEmp =(Employee) employeeVM;

                var mappedEmp = mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                //mappedEmp.ImageName = fileName;

                 _unitOfWork.Repo<Employee>().Add(mappedEmp);

                var count = await _unitOfWork.complete();
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
            return View(employeeVM);
        }
        [HttpGet]
        public async Task<IActionResult> detailsAsync(int? id, string view = "details")
        {
            if (!id.HasValue)
                return BadRequest();
            var employee = await _unitOfWork.Repo<Employee>().GetAsync(id.Value);
            var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);
            if (employee == null)
                return NotFound();
            if(view.Equals("delete",StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = employee.ImageName;
            return View(view, mappedEmp);
        }
        [HttpGet]
        public async Task<IActionResult> editAsync(int? id)
        {

            return await detailsAsync(id, "edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editAsync([FromRoute] int Id, EmployeeViewModel employeeVM)
        {
            if (Id != employeeVM .Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.Repo<Employee>().Update(mappedEmp);
                await _unitOfWork.complete();
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
                return View(employeeVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> deleteAsync(int? id)
        {

            return await detailsAsync(id, "delete");
        }
        [HttpPost]
        public async Task<IActionResult> deleteAsync(EmployeeViewModel employeeVM)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            try
            {
                employeeVM.ImageName = TempData["ImageName"] as string;
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.Repo<Employee>().Delete(mappedEmp);
                var count= await _unitOfWork.complete();
                if (count > 0)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                    return RedirectToAction("Index");
                }
                return View(employeeVM);
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
                return View(employeeVM);
            }
        }
    }
}
