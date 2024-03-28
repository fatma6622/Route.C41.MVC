using Route.C41.MVC.DAL.Models;
using System.Collections.Generic;
using System;

namespace Route.C41.MVC.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
