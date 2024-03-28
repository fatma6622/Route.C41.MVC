using Microsoft.Extensions.DependencyInjection;
using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.BLL.repoes;

namespace Route.C41.MVC.PL.Extentions
{
    public static class ApplicationServiesExtentions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepo,DepartmentRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
        }
    }
}
