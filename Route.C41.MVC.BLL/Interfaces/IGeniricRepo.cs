using Route.C41.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.MVC.BLL.IGeniricRepo
{
    public interface IGeniricRepo<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int Id);
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);

    }
}
