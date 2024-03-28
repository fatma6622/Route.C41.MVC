using Microsoft.EntityFrameworkCore;
using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.DAL.Data;
using Route.C41.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.MVC.BLL.repoes
{
    public class EmployeeRepo : GeniricRepo<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
        public IQueryable<Employee> GetByAddress(string Address)
        {
            return dbContext.Employees.Where(e=>e.Address.ToLower()== Address.ToLower());
        }
            
        public IQueryable<Employee> GetByName(string searchInp)
        {
            return dbContext.Employees.Where(E => E.Name.ToLower().Contains(searchInp));
        }
    }
}
