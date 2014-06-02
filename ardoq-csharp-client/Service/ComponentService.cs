using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;

namespace Ardoq.Service
{
    public class ComponentService : ServiceBase, IComponentService
    {

        internal ComponentService(IComponentService service, string org) : base(org)
        {
            Service = service;
        }

        private IComponentService Service { get; set; }

        public Task<List<Component>> GetAllComponents(string org)
        {
            return Service.GetAllComponents(Org);
        }


        public Task DeleteComponent(string id, string org)
        {
            return Service.DeleteComponent(id, org);
        }

        public Task<Component> GetComponentById(string id, string org)
        {
            return Service.GetComponentById(id, org);
        }

        public Task<Component> CreateComponent(Component component, string org)
        {
            return Service.CreateComponent(component, org);
        }

        public Task<Component> UpdateComponent(string id, Component component, string org)
        {
            return Service.UpdateComponent(id, component, org);
        }

        public Task<List<Component>> GetAllComponents()
        {
            return GetAllComponents(Org);
        }

        public Task DeleteComponent(string id)
        {
            return DeleteComponent(id, Org);
        }

        public Task<Component> GetComponentById(string id)
        {
            return GetComponentById(id, Org);
        }

        public Task<Component> CreateComponent(Component component)
        {
            return CreateComponent(component, Org);
        }

        public Task<Component> UpdateComponent(string id, Component component)
        {
            return UpdateComponent(id, component, Org);
        }
    }
}