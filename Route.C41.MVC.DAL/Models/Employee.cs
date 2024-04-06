using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.MVC.DAL.Models
{
    public enum gender
    {
        [EnumMember(Value = "Male")]
        male = 1,
        [EnumMember(Value = "Female")]
        female = 2
    }
    public enum empType
    {
        fullTime = 1,
        partTime = 2
    }
    public class Employee:ModelBase
    {
        public string ImageName {  get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public decimal salary { get; set; }
        public bool isActive { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public DateTime hirringDate { get; set; }
        public gender gen { get; set; }
        public empType employeeType { get; set; }
        public DateTime creationDate { get; set; }= DateTime.Now;
        public bool isDeleted { get; set; } = false;
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

    }
}
