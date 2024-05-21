using System.Threading.Tasks;

namespace Route.C41.MVC.PL.Services.EmailSettings
{
	public interface IEmailSender
	{
		Task SendAsync(string from,string recipients,string subject, string body);
	}
}
