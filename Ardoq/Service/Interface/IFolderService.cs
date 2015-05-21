using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
	public interface IFolderService
	{
		[Get ("/api/workspacefolder")]
		Task<List<Folder>> GetAllFolders(
            [AliasAs ("org")] string org = null);

		[Get ("/api/workspacefolder/{id}")]
		Task<Folder> GetFolderById(
            [AliasAs ("id")] String id, 
            [AliasAs ("org")] string org = null);

		[Post ("/api/workspacefolder")]
		Task<Folder> CreateFolder(
            [Body] Folder tag, 
            [AliasAs ("org")] string org = null);

		[Put ("/api/workspacefolder/{id}")]
		Task<Folder> UpdateFolder(
            [AliasAs ("id")] String id, 
            [Body] Folder tag, 
            [AliasAs ("org")] string org = null);

		[Delete ("/api/workspacefolder/{id}")]
		Task DeleteFolder(
            [AliasAs ("id")] String id, 
            [AliasAs ("org")] string org = null);

	    Task<Folder> GetFolderByName(
            string name, 
            string org = null);
	}
}