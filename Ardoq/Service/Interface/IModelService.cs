using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;

namespace Ardoq.Service.Interface
{
    public interface IModelService
    {
        Task<List<Model>> GetAllModels(string org = null);
        Task<Model> GetModelByName(String name, string org = null);
        Task<Model> GetModelById(string id, string org = null);
        Task<Model> UploadModel(String model, String org = null);
    }
}
