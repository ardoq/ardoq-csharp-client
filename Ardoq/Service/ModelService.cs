using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;

namespace Ardoq.Service
{
    public class ModelService : ServiceBase, IDeprecatedModelService
    {
        internal ModelService(IDeprecatedModelService service, string org) : base(org)
        {
            Service = service;
        }

        private IDeprecatedModelService Service { get; set; }

        public Task<List<Model>> GetAllModels(string org)
        {
            return Service.GetAllModels(org);
        }

        public Task<Model> GetModelById(string id, string org)
        {
            return Service.GetModelById(id, org);
        }

        public Task<List<Model>> GetAllModels()
        {
            return GetAllModels(Org);
        }

        public Task<Model> GetModelById(String id)
        {
            return GetModelById(id, Org);
        }

        public async Task<Model> GetModelByName(String name)
        {
            List<Model> allModels = await Service.GetAllModels(Org);
			List<Model> result = allModels.Where(m => m.Name.ToLower() == name.ToLower()).ToList();
            if (result.Count() != 1)
                throw new InvalidOperationException("No model with that name exists!");

            return result.First();
        }
    }
}