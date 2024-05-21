using System.ComponentModel.DataAnnotations;

namespace Route.C41.MVC.PL.ViewModels.Account
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password Is required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Password Is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "Comfirm Password don't match")]
		public string ComfirmNewPassword { get; set; }
	}
}
