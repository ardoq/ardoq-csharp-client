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

		public Task<List<Reference>> GetAllReferences (string org)
		{
			return Service.GetAllReferences (org);
		}

		public Task<Reference> GetReferenceById (string id, string org)
		{
			return Service.GetReferenceById (id, org);
		}

		public Task<Reference> CreateReference (Reference reference, string org)
		{
			return Service.CreateReference (reference, org);
		}

		public Task<Reference> UpdateReference (string id, Reference reference, string org)
		{
			reference.Created = null;
			reference.LastUpdated = null;
			return Service.UpdateReference (id, reference, org);
		}

		public Task DeleteReference (string id, string org)
		{
			return Service.DeleteReference (id, org);
		}

		public Task<List<Reference>> GetAllReferences ()
		{
			return GetAllReferences (Org);
		}

		public Task<Reference> GetReferenceById (string id)
		{
			return GetReferenceById (id, Org);
		}

		public Task<Reference> CreateReference (Reference reference)
		{
			return Service.CreateReference (reference, Org);
		}

		public Task<Reference> UpdateReference (string id, Reference reference)
		{
			return UpdateReference (id, reference, Org);
		}

		public Task DeleteReference (string id)
		{
			return DeleteReference (id, Org);
		}
	}
}