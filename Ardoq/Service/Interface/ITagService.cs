using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
    public interface ITagService
    {
        [Get("/api/tag")]
        Task<List<Tag>> GetAllTags([AliasAs("org")] string org);

        [Get("/api/tag/{id}")]
        Task<Tag> GetTagById([AliasAs("id")] String id, [AliasAs("org")] string org);

        [Post("/api/tag")]
        Task<Tag> CreateTag([Body] Tag tag, [AliasAs("org")] string org);

        [Put("/api/tag/{id}")]
        Task<Tag> UpdateTag([AliasAs("id")] String id, [Body] Tag tag, [AliasAs("org")] string org);

        [Delete("/api/tag/{id}")]
        Task DeleteTag([AliasAs("id")] String id, [AliasAs("org")] string org);
    }
}