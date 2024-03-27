using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.MVC.DAL.Models
{
    public class Employee:ModelBase
    {
        public enum gender
        {
            [EnumMember(Value ="Male")]
            male=1,
            [EnumMember(Value = "Female")]
            female = 2
        }
        public enum empType
        {
            fullTime=1,
            partTime=2
        }
        [Required(ErrorMessage ="name is required")]
        [MaxLength(50,ErrorMessage ="max length of name is 50 char")]
        [MinLength(5,ErrorMessage ="min length of name is 5 char")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal salary { get; set; }
        [Display(Name = "is active")]
        public bool isActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string phone { get; set; }
        [Display(Name ="hiring date")]
        public DateTime hirringDate { get; set; }
        public gender gen { get; set; }
        public empType employeeType { get; set; }
        public DateTime creationDate { get; set; }= DateTime.Now;
        public bool isDeleted { get; set; } = false;
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

    }
}
