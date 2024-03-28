using AutoMapper;
using Route.C41.MVC.DAL.Models;
using Route.C41.MVC.PL.ViewModels;

namespace Route.C41.MVC.PL.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
