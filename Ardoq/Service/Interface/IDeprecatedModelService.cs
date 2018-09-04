using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
    public interface IDeprecatedModelService
    {
        [Get("/api/model")]
        Task<List<Model>> GetAllModels(
            [AliasAs("org")] string org = null);

        [Get("/api/model?includeCommon=true")]
        Task<List<Model>> GetAllTemplates(
            [AliasAs("org")] string org = null);

        [Get("/api/model/{id}")]
        Task<Model> GetModelById(
            [AliasAs("id")] string id,
            [AliasAs("org")] string org = null);

        Task<List<Model>> GetModelsByName(string name, string org = null);

        Task<Model> GetTemplateByName(string name, string org = null);

        Task<List<Model>> GetTemplatesByName(string name, string org = null);

        Task<Model> UploadModel(string model, string org = null);
    }
}