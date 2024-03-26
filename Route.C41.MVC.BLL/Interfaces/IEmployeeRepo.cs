using Route.C41.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.MVC.BLL.IGeniricRepo
{
    public interface  IEmployeeRepo:IGeniricRepo<Employee>
    {
        IQueryable<Employee> GetByAddress(string Address);
    }
}
