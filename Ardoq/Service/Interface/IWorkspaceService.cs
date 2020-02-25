using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
    public interface IWorkspaceService
    {
        [Get("/api/workspace")]
        Task<List<Workspace>> GetAllWorkspaces(
            [AliasAs("org")] string org = null);

        [Get("/api/workspace/{id}")]
        Task<Workspace> GetWorkspaceById(
            [AliasAs("id")] string id,
            [AliasAs("org")] string org = null);

        [Get("/api/workspace/{id}/aggregated")]
        Task<AggregatedWorkspace> GetAggregatedWorkspace(
            [AliasAs("id")] string id,
            [AliasAs("org")] string org = null);

        [Post("/api/workspace")]
        Task<Workspace> CreateWorkspace(
            [Body] Workspace workspace,
            [AliasAs("org")] string org = null);

        [Post("/api/workspace/{id}/branch/create")]
        Task<Workspace> BranchWorkspace(
            [AliasAs("id")] string id,
            [Body] WorkspaceBranchRequest branch,
            [AliasAs("org")] string org = null);

        [Put("/api/workspace/{id}")]
        Task<Workspace> UpdateWorkspace(
            [AliasAs("id")] string id,
            [Body] Workspace workspace,
            [AliasAs("org")] string org = null);

        [Delete("/api/workspace/{id}")]
        Task DeleteWorkspace(
            [AliasAs("id")] string id,
            [AliasAs("org")] string org = null);

        [Get("/api/workspace/search")]
        Task<List<Workspace>> SearchWorkspacesByName(
            [AliasAs("name")] string name,
            [AliasAs("org")] string org = null);
    }
}