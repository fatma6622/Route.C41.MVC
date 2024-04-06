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

        public void Add(T entity)
        {
            dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public async Task<T> GetAsync(int Id)
        {
            return await dbContext.FindAsync<T>(Id); ;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await dbContext.Employees.Include(e=>e.Department).ToListAsync();
            }
            else
            {
                return await dbContext.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }
    }
}
