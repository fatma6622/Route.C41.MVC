﻿using Microsoft.Extensions.DependencyInjection;
using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.BLL.Interfaces;
using Route.C41.MVC.BLL.repoes;
using Route.C41.MVC.BLL.Repoes;

namespace Route.C41.MVC.PL.Extentions
{
    public static class ApplicationServiesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IDepartmentRepo,DepartmentRepo>();
            //services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            return services;
        }
    }
}
