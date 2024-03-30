using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.BLL.Interfaces;
using Route.C41.MVC.BLL.repoes;
using Route.C41.MVC.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.MVC.BLL.Repoes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepo employeeRepo{get; set;} = null;
        public IDepartmentRepo departmentRepo{get; set;} = null;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            employeeRepo =new EmployeeRepo(_dbContext);
            departmentRepo=new DepartmentRepo(_dbContext);
        }

        public int complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
