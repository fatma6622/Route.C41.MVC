using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Route.C41.MVC.PL.ViewModels.User
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "UserName Is required")]
		public string UserName { get; set; }
		[Required(ErrorMessage ="Email Is required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }
        [Required(ErrorMessage = "Password Is required")]
		[MinLength(5,ErrorMessage ="Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
		[Required(ErrorMessage = "Password Is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Comfirm Password")]
		public string ComfirmPassword { get; set; }
		public bool IsAgree { get; set; }
		[Required(ErrorMessage = "First Name Is required")]
		[Display(Name ="First Name")]
		public string FName { get; set; }
		[Required(ErrorMessage = "Last Name Is required")]
		[Display(Name = "Last Name")]
		public string LName { get; set; }
	}
}
