using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;
using System;
using System.Text;

namespace Ardoq.Service
{
	public class ComponentService : ServiceBase, IComponentService
	{

		internal ComponentService (IComponentService service, string org) : base (org)
		{
			Service = service;
		}

		private IComponentService Service { get; set; }

		public Task<List<Component>> GetAllComponents (string org = null)
		{
            org = org ?? Org;
            return Service.GetAllComponents(org);
		}


		public Task DeleteComponent (string id, string org = null)
		{
            org = org ?? Org;
            return Service.DeleteComponent(id, org);
		}

		public Task<Component> GetComponentById (string id, string org = null)
		{
            org = org ?? Org;
            return Service.GetComponentById(id, org);
		}

		public Task<Component> CreateComponent (Component component, string org = null)
		{
            org = org ?? Org;
            return Service.CreateComponent(component, org);
		}

		public Task<Component> UpdateComponent (string id, Component component, string org = null)
		{
            org = org ?? Org;
            component.LastUpdated = null;
			component.Created = null;
			return Service.UpdateComponent (id, component, org);
		}

        public Task<List<Component>> FieldSearch(string workspace, Dictionary<string, string> fieldQuery, string org = null)
		{
            org = org ?? Org;
			return Service.FieldSearch (workspace, fieldQuery, org);
		}
	}
}