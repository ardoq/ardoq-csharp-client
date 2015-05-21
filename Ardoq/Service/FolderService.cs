using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;
using System.Linq;
using System;

namespace Ardoq.Service
{
	public class FolderService : ServiceBase, IFolderService
	{
		internal FolderService (IFolderService service, string org)
			: base (org)
		{
			Service = service;
		}

		private IFolderService Service { get; set; }

		public Task<List<Folder>> GetAllFolders(string org = null)
		{
            org = org ?? Org;
            return Service.GetAllFolders(org);
		}

		public async Task<Folder> GetFolderByName(string name, string org = null)
		{
            org = org ?? Org;
            var allModels = await Service.GetAllFolders(org);
			var result = allModels.Where (m => m.Name.ToLower () == name.ToLower ()).ToList ();
			if (result.Count () != 1)
				throw new InvalidOperationException ("No folder with that name exists!");

			return result.First ();
		}

		public Task<Folder> GetFolderById (string id, string org = null)
		{
            org = org ?? Org;
            return Service.GetFolderById(id, org);
		}

		public Task<Folder> CreateFolder (Folder folder, string org = null)
		{
            org = org ?? Org;
            return Service.CreateFolder(folder, org);
		}

		public Task<Folder> UpdateFolder (string id, Folder folder, string org = null)
		{
            org = org ?? Org;
            return Service.UpdateFolder(id, folder, org);
		}

		public Task DeleteFolder (string id, string org = null)
		{
            org = org ?? Org;
            return Service.DeleteFolder(id, org);
		}
	}
}