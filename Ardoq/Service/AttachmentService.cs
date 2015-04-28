using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;
using Newtonsoft.Json;

namespace Ardoq.Service
{
    public class AttachmentService : ServiceBase, IAttachmentService
    {

        internal AttachmentService(IAttachmentService service, HttpClient sharedHttpClient, string org)
            : base(org)
        {
            Service = service;
            HttpClient = sharedHttpClient;
        }

        private IAttachmentService Service { get; set; }

        private HttpClient HttpClient { get; set; }

        public Task<List<Attachment>> GetAttachments(string resourceId, string resourceType, string org)
        {
            string checkedResourceType = CheckResourceType(resourceType);
#if DEBUG
            Debug.WriteLine("Calling the GetAttachments Service");
            Debug.WriteLine("Resource ID :" + resourceId);
            Debug.WriteLine("Org :" + org);
            Debug.WriteLine("ResourceType :" + checkedResourceType);
#endif
            return Service.GetAttachments(resourceId, org, checkedResourceType);
        }

        public async Task<Attachment> UploadAttachment(string resourceId, Stream attachment, string fileName,
            string resourceType, string org)
        {
            const string urlTemplate = "api/attachment/{0}/{1}/upload?org={2}";

            string url = HttpClient.BaseAddress +
                         string.Format(urlTemplate, CheckResourceType(resourceType), resourceId, org);

            using (var multiPartContent = new MultipartFormDataContent("----WebKitFormBoundaryhBvlNBmNqHPBuOm2"))
            {
                multiPartContent.Add(new StreamContent(attachment), "attachment", fileName);

                using (HttpResponseMessage message = await HttpClient.PostAsync(url, multiPartContent))
                {
                    string attachmentJson = await message.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Attachment>(attachmentJson);
                }
            }
        }

        public Task DeleteAttachment(string resourceId, string resourceType, string filename, string org)
        {
            return Service.DeleteAttachment(resourceId, resourceType, filename, org);
        }

        public async Task<Stream> DownloadAttachment(string resourceId, string resourceType, string filename, string org)
        {
            const string urlTemplate = "api/attachment/{0}/{1}/{2}?org={3}";

            string url = HttpClient.BaseAddress +
                         string.Format(urlTemplate, CheckResourceType(resourceType), resourceId, filename, org);

            HttpResponseMessage responseMessage =
                await HttpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            using (Stream httpStream = await responseMessage.Content.ReadAsStreamAsync())
            {
                var resultStream = new MemoryStream();
                await httpStream.CopyToAsync(resultStream);
                return resultStream;
            }
        }

        /// <summary>
        ///     temporarily used
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        private static string CheckResourceType(string resourceType)
        {
            string newResourceType = resourceType;
            if (newResourceType != null && newResourceType != "workspace")
                newResourceType = "workspace";
            return newResourceType;
        }
    }
}