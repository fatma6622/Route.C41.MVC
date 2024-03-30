using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Route.C41.MVC.BLL.IGeniricRepo;
using Route.C41.MVC.BLL.Interfaces;
using Route.C41.MVC.BLL.repoes;
using Route.C41.MVC.DAL.Data;
using Route.C41.MVC.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.MVC.BLL.Repoes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        //private Dictionary<string, IGeniricRepo<ModelBase>> _Repos;
        private Hashtable _Repos;

        //public IEmployeeRepo employeeRepo{get; set;} = null;
        //public IDepartmentRepo departmentRepo{get; set;} = null;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            //employeeRepo =new EmployeeRepo(_dbContext);
            //departmentRepo=new DepartmentRepo(_dbContext);
            _Repos=new Hashtable();
        }

        public IGeniricRepo<T> Repo<T>() where T : ModelBase
        {
            var key=typeof(T).Name;
            if(!_Repos.ContainsKey(key))
            {
                if (key == nameof(Employee))
                {
                    var repos = new EmployeeRepo(_dbContext);
                    _Repos.Add(key, repos);
                }
                else
                {
                    var repos = new GeniricRepo<T>(_dbContext);
                    _Repos.Add(key, repos);
                }

            }

            return _Repos[key] as IGeniricRepo<T>;
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
