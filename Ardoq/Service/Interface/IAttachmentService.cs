using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
    public interface IAttachmentService
    {
        //[Post("/api/attachment/{type}/{id}/upload")]
        Task<Attachment> UploadAttachment([AliasAs("id")] string resourceId, Stream attachment, 
            [AliasAs("filename")] string filename,
            [AliasAs("type")] string resourceType, [AliasAs("org")] string org);

        [Delete("/api/attachment/{type}/{id}/{filename}")]
        Task DeleteAttachment([AliasAs("id")] string resourceId, [AliasAs("filename")] string filename,
            [AliasAs("type")] string resourceType, [AliasAs("org")] string org);

        //[Get("/api/attachment/{type}/{id}/{filename}")]
        Task<Stream> DownloadAttachment([AliasAs("id")] string resourceId, [AliasAs("filename")] string filename,
            [AliasAs("type")] string resourceType, [AliasAs("org")] string org);

        [Get("/api/attachment/{type}/{id}")]
        Task<List<Attachment>> GetAttachments([AliasAs("id")] string resourceId,
            [AliasAs("type")] string resourceType, [AliasAs("org")] string org);
    }
}