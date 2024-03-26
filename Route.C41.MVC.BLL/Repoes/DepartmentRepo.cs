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
    public class DepartmentRepo : GeniricRepo<Department>,IDepartmentRepo
    {
        public DepartmentRepo(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
    }
}
