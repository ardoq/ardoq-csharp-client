using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Ardoq.Service
{
	public class ModelService : ServiceBase, IDeprecatedModelService
    {
		internal ModelService (IDeprecatedModelService service, HttpClient sharedHttpClient, string org) : base (org)
		{
			Service = service;
			HttpClient = sharedHttpClient;
		}

		private HttpClient HttpClient { get; set; }

		private IDeprecatedModelService Service { get; set; }

		public Task<List<Model>> GetAllModels (string org = null)
		{
            org = org ?? Org;
            return Service.GetAllModels(org);
		}

		public Task<Model> GetModelById (string id, string org = null)
		{
            org = org ?? Org;
            return Service.GetModelById(id, org);
		}

		public async Task<Model> GetTemplateByName(string name, string org = null)
		{
            org = org ?? Org;
            var allModels = await Service.GetAllModels(org);
			var result = allModels.Where (m => m.Name.ToLower () == name.ToLower ()).ToList ();
			if (!result.Any())
				throw new InvalidOperationException ("No template with "+name+" name exists!");

			return result.First();
		}

		public async Task<Model> UploadModel (String model, String org = null)
		{
            org = org ?? Org;
            const string urlTemplate = "api/model?org={0}";
			var url = HttpClient.BaseAddress + string.Format (urlTemplate, org);

			var modelContent = new StringContent (model, System.Text.Encoding.UTF8);
			modelContent.Headers.ContentType = MediaTypeHeaderValue.Parse ("application/json");
			var responseMessage = await HttpClient.PostAsync (url, modelContent);

			var attachmentJson = await responseMessage.Content.ReadAsStringAsync ();
			return JsonConvert.DeserializeObject<Model> (attachmentJson);
		}
	}
}