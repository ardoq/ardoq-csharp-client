using System;
using System.Threading.Tasks;
using Ardoq.Models;

namespace Ardoq.Service.Interface
{
    public interface IModelService
    {
        Task<Model> GetModelByName(String name, string org);
        Task<Model> UploadModel(String model, String org);
    }
}
