using Route.C41.MVC.BLL.IGeniricRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.MVC.BLL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public IEmployeeRepo employeeRepo { get; set; }
        public IDepartmentRepo departmentRepo { get; set; }
        int complete();
    }
}
