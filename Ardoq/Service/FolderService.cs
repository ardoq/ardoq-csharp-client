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

		public Task<List<Folder>> GetAllFolders (string org)
		{
			return Service.GetAllFolders (org);
		}

		public async Task<Folder> GetFolderByName (string name)
		{
			List<Folder> allModels = await Service.GetAllFolders (Org);
			List<Folder> result = allModels.Where (m => m.Name.ToLower () == name.ToLower ()).ToList ();
			if (result.Count () != 1)
				throw new InvalidOperationException ("No folder with that name exists!");

			return result.First ();
		}

		public Task<Folder> GetFolderById (string id, string org)
		{
			return Service.GetFolderById (id, org);
		}

		public Task<Folder> CreateFolder (Folder folder, string org)
		{
			return Service.CreateFolder (folder, org);
		}

		public Task<Folder> CreateFolder (String folderName, string org)
		{
			var folder = new Folder (folderName, "");
			return Service.CreateFolder (folder, org);
		}

		public Task<Folder> UpdateFolder (string id, Folder folder, string org)
		{
			return Service.UpdateFolder (id, folder, org);
		}

		public Task DeleteFolder (string id, string org)
		{
			return Service.DeleteFolder (id, org);
		}

		public Task<List<Folder>> GetAllFolders ()
		{
			return GetAllFolders (Org);
		}

		public Task<Folder> GetFolderById (string id)
		{
			return GetFolderById (id, Org);
		}


		public Task<Folder> CreateFolder (Folder folder)
		{
			return CreateFolder (folder, Org);
		}

		public Task<Folder> CreateFolder (string folderName)
		{
			var folder = new Folder (folderName, "");
			return CreateFolder (folder, Org);
		}

		public Task<Folder> UpdateFolder (string id, Folder folder)
		{
			return UpdateFolder (id, folder, Org);
		}

		public Task DeleteFolder (string id)
		{
			return DeleteFolder (id, Org);
		}
	}
}