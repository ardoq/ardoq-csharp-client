using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;

namespace Ardoq.Service
{
	public class WorkspaceService : ServiceBase, IWorkspaceService
	{
		internal WorkspaceService (IWorkspaceService service, string org) : base (org)
		{
			Service = service;
		}

		private IWorkspaceService Service { get; set; }

		public Task<List<Workspace>> GetAllWorkspaces (string org = null)
		{
            org = org ?? Org;
            return Service.GetAllWorkspaces(org);
		}

		public Task<Workspace> GetWorkspaceById (string id, string org = null)
		{
            org = org ?? Org;
            return Service.GetWorkspaceById(id, org);
		}

		public Task<AggregatedWorkspace> GetAggregatedWorkspace (string id, string org = null)
		{
            org = org ?? Org;
            return Service.GetAggregatedWorkspace(id, org);
		}

		public Task<Workspace> CreateWorkspace (Workspace workspace, string org = null)
		{
            org = org ?? Org;
            return Service.CreateWorkspace(workspace, org);
		}

		public Task<Workspace> BranchWorkspace (string id, WorkspaceBranchRequest branch, string org = null)
		{
            org = org ?? Org;
            return Service.BranchWorkspace(id, branch, org);
		}

		public Task<Workspace> UpdateWorkspace (string id, Workspace workspace, string org = null)
		{
            org = org ?? Org;
            workspace.Created = null;
			workspace.LastUpdated = null;
			return Service.UpdateWorkspace (id, workspace, org);
		}

		public Task DeleteWorkspace (string id, string org = null)
		{
            org = org ?? Org;
            return Service.DeleteWorkspace(id, org);
		}

        public Task<List<Workspace>> SearchWorkspacesByName(string name, string org = null)
        {
            org = org ?? Org;
            return Service.SearchWorkspacesByName(name, org);
        }
    }
}