using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;

namespace Ardoq.Service
{
	public class ReferenceService : ServiceBase, IReferenceService
	{
		internal ReferenceService (IReferenceService service, string org) : base (org)
		{
			Service = service;
		}

		private IReferenceService Service { get; set; }

		public Task<List<Reference>> GetAllReferences (string org = null)
		{
            org = org ?? Org;
            return Service.GetAllReferences(org);
		}

        public Task<List<Reference>> GetReferencesByWorkspace(string workspaceId, string org = null)
        {
            org = org ?? Org;
            return Service.GetReferencesByWorkspace(workspaceId, org);
        }

        public Task<Reference> GetReferenceById (string id, string org = null)
		{
            org = org ?? Org;
            return Service.GetReferenceById(id, org);
		}

		public Task<Reference> CreateReference (Reference reference, string org = null)
		{
            org = org ?? Org;
            return Service.CreateReference(reference, org);
		}

		public Task<Reference> UpdateReference (string id, Reference reference, string org = null)
		{
            org = org ?? Org;
            reference.Created = null;
			reference.LastUpdated = null;
			return Service.UpdateReference (id, reference, org);
		}

		public Task DeleteReference (string id, string org = null)
		{
            org = org ?? Org;
            return Service.DeleteReference(id, org);
		}
	}
}