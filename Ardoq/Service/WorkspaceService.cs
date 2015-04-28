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

		public Task<List<Workspace>> GetAllWorkspaces (string org)
		{
			return Service.GetAllWorkspaces (org);
		}

		public Task<Workspace> GetWorkspaceById (string id, string org)
		{
			return Service.GetWorkspaceById (id, org);
		}

		public Task<List<WorkspaceBranch>> GetBranches (string id, string org)
		{
			return Service.GetBranches (id, org);
		}

		public Task<AggregatedWorkspace> GetAggregatedWorkspace (string id, string org)
		{
			return Service.GetAggregatedWorkspace (id, org);
		}

		public Task<Workspace> CreateWorkspace (Workspace workspace, string org)
		{
			return Service.CreateWorkspace (workspace, org);
		}

		public Task<Workspace> BranchWorkspace (string id, WorkspaceBranchRequest branch, string org)
		{
			return Service.BranchWorkspace (id, branch, org);
		}

		public Task<Workspace> UpdateWorkspace (string id, Workspace workspace, string org)
		{
			workspace.Created = null;
			workspace.LastUpdated = null;
			return Service.UpdateWorkspace (id, workspace, org);
		}

		public Task DeleteWorkspace (string id, string org)
		{
			return Service.DeleteWorkspace (id, org);
		}
	}
}