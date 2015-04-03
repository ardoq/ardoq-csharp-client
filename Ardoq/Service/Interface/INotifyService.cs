using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
	public interface INotifyService
	{
		[Post ("/api/user/notify/email")]
		Task<Message> PostMessage ([Body] Message message);
	}
}