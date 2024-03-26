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
        IEnumerable<T> GetAll();
        T Get(int Id);
        int Update(T entity);
        int Delete(T entity);
        int Add(T entity);

    }
}
