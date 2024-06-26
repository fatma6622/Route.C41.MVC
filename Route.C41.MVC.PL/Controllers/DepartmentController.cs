﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.BLL.Interfaces;
using Route.C41.MVC.DAL.Models;
using Route.C41.MVC.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route.C41.MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepo _departmentRepo;

        public IWebHostEnvironment Env { get; }

        public DepartmentController(IMapper mapper, 
            IUnitOfWork unitOfWork
            ,IWebHostEnvironment env)
        {
            this.mapper = mapper;
            _unitOfWork = unitOfWork;
            //_departmentRepo =departmentRepo;
            Env = env;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var departments = await _unitOfWork.Repo<Department>().GetAllAsync();
            var mappedDepts = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid)
            {
                var mappedDept = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repo<Department>().Add(mappedDept);
                var count = await _unitOfWork.complete();
                if (count > 0)
                {
                    TempData["massage"] = "created successfully";
                }
                else
                {
                    TempData["massage"] = "not created";
                }
                return RedirectToAction(nameof(IndexAsync));

            }
            return View(departmentVM);
        }
        [HttpGet]
        public async Task<IActionResult> detailsAsync(int? id, string view = "details")
        {
            if (!id.HasValue)
                return BadRequest();
            var department= await _unitOfWork.Repo<Department>().GetAsync(id.Value);
            var mappedDept = mapper.Map<Department, DepartmentViewModel>(department);

            if (department==null)
                return NotFound();

            return View(view, mappedDept);
        }
        [HttpGet]
        public async Task<IActionResult> editAsync(int? id)
        {

            return await detailsAsync(id, "edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editAsync([FromRoute] int Id, DepartmentViewModel departmentVM)
        {
            if(Id!= departmentVM.Id)
                return BadRequest();
            if(!ModelState.IsValid)
                return BadRequest();
            try
            {
                var mappedDept = mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repo<Department>().Update(mappedDept);
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
                return View(departmentVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> deleteAsync(int? id)
        {

            return await detailsAsync(id, "delete");
        } 
        [HttpPost]
        public async Task<IActionResult> deleteAsync(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var mappedDept = mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repo<Department>().Delete(mappedDept);
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
                    ModelState.AddModelError(string.Empty, "deleting error");
                }
                return View(departmentVM);
            }
        }
    }
}
