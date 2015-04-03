using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;
using System.Net.Http;

namespace Ardoq.Service
{
	public class NotifyService : ServiceBase, INotifyService
	{
		internal NotifyService (INotifyService service, HttpClient sharedHttpClient)
		{
			Service = service;
			HttpClient = sharedHttpClient;
		}

		private HttpClient HttpClient { get; set; }

		private INotifyService Service { get; set; }

		public Task<Message> PostMessage (Message message)
		{
			return Service.PostMessage (message);
		}
			
	}
}