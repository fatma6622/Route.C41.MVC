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
    public class GeniricRepo<T>:IGeniricRepo<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext dbContext;

        public GeniricRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }

        public T Get(int Id)
        {
            return dbContext.Find<T>(Id); ;
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) dbContext.Employees.Include(e=>e.Department).ToList();
            }
            else
            {
                return dbContext.Set<T>().AsNoTracking().ToList();
            }
        }

        public int Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return dbContext.SaveChanges();
        }
    }
}
