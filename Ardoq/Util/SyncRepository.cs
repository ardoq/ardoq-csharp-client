using System;
using System.Collections.Generic;
using Ardoq.Models;
using Ardoq;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Ardoq.Util
{
	public class SyncRepository
	{
		Dictionary<String, Component> CompMap = new Dictionary<string, Component> ();
		Dictionary<String, Component> CompIDMap = new Dictionary<string, Component> ();
		Dictionary<String, Tag> TagMap = new Dictionary<string, Tag> ();
		Dictionary<String, Reference> RefMap = new Dictionary<string, Reference> ();
		Dictionary<String, Component> oldComponentMap = new Dictionary<string, Component> ();
		Dictionary<String, int> oldReferenceMap = new Dictionary<string, int> ();
		Dictionary<String, Tag> oldTagMap = new Dictionary<string, Tag> ();
		List<String> addedComps = new List<string> ();
		List<String> addedRefs = new List<string> ();
		List<String> addedTags = new List<string> ();

		int workspaceCheckSum;
		ArdoqClient client;
		Workspace workspace;

		int addedCompCount;
		int updatedCompCount;
		int deleteCompsCount;

		int addedRefCount;
		int updatedRefCount;
		int deleteRefsCount;
		int deleteTagsCount;

		int addedWorkspaceCount;
		int updatedWorkspaceCount;

		public SyncRepository (ArdoqClient client)
		{
			this.client = client;
		}

		public async Task<bool> CleanUpMissingComps ()
		{
			foreach (string fp in RefMap.Keys) {
				if (!addedRefs.Contains (fp)) {
					var id = RefMap [fp].Id;
					if (!String.IsNullOrEmpty (id)) {

						try {
							await client.ReferenceService.DeleteReference (id);
						} catch (System.Net.Http.HttpRequestException  http) {
							if (http.Message != "404 (Not Found)")
								Console.WriteLine ("Couldnt delete Ref " + id);
						} catch (Refit.ApiException err) {
							if (err.StatusCode != HttpStatusCode.NotFound)
								Console.WriteLine ("Couldnt delete Ref " + id);
						}

						deleteRefsCount++;
					}
				}
			}

			foreach (string fp in CompMap.Keys) {
				if (!addedComps.Contains (fp)) {
					var id = CompMap [fp].Id;
					if (!String.IsNullOrEmpty (id)) {
						
						try {
							await client.ComponentService.DeleteComponent (id);
						} catch (System.Net.Http.HttpRequestException  http) {
							if (http.Message != "404 (Not Found)")
								Console.WriteLine ("Couldnt delete comp " + id);
						} catch (Refit.ApiException err) {
							if (err.StatusCode != HttpStatusCode.NotFound)
								Console.WriteLine ("Couldnt delete comp " + id);
						}

						deleteCompsCount++;
					}
				}
			}
			return true;
		}

		public string GetReport ()
		{
			StringBuilder report = new StringBuilder ();
			report.Append ("Created ");
			report.Append (addedWorkspaceCount);
			report.AppendLine (" Workspaces");
			report.Append ("Updated ");
			report.Append (updatedWorkspaceCount);
			report.AppendLine (" Workspaces");

			report.Append ("Created ");
			report.Append (addedCompCount);
			report.AppendLine (" Components");

			report.Append ("Updated ");
			report.Append (updatedCompCount);
			report.AppendLine (" Components");

			report.Append ("Deleted ");
			report.Append (deleteCompsCount);
			report.AppendLine (" Components");

			report.Append ("Created ");
			report.Append (addedRefCount);
			report.AppendLine (" References");
			report.Append ("Updated ");
			report.Append (updatedRefCount);
			report.AppendLine (" References");

			report.Append ("Deleted ");
			report.Append (deleteRefsCount);
			report.AppendLine (" References");

			return report.ToString ();
		}

		public async Task<Workspace> PrefetchWorkspace (String name, String modelId)
		{
			workspace = await GetWorkspace (name);
			if (workspace == null) {
				workspace = await CreateWorkspace (name, modelId);
			} else {
				AggregatedWorkspace aws = await client.WorkspaceService.GetAggregatedWorkspace (workspace.Id);
				foreach (var comp in aws.Components) {
					CacheComp (comp);
				}
				foreach (var comp in CompIDMap.Values) {
					if (!String.IsNullOrEmpty (comp.Parent) && CompIDMap.ContainsKey (comp.Parent)) {
						comp.CachedParent = CompIDMap [comp.Parent];
					}
				}
				foreach (var reference in aws.References) {
					reference.cachedTarget = await GetComponentById (reference.Target);
					reference.cachedSource = await GetComponentById (reference.Source);
					var refName = reference.cachedSource.Fields ["_fullName"] + reference.Type.ToString () + reference.cachedTarget.Fields ["_fullName"];
					RefMap.Add (refName, reference);
					oldReferenceMap.Add (reference.Id, reference.GetHashCode ());
				}

				foreach (var tag in aws.Tags) {
					TagMap.Add (tag.Name, tag);
					//checksumMap.Add (tag.Id, tag.GetHashCode ());
				}
			}
			workspaceCheckSum = workspace.GetHashCode ();
			return workspace;

		}


		public async Task<Component> GetComponentById (string id)
		{
			if (CompIDMap.ContainsKey (id)) {
				return CompIDMap [id];
			}
			var c = await client.ComponentService.GetComponentById (id);
			CacheComp (c);
			return c;
		}

		void CacheComp (Component comp)
		{
			var fullName = comp.Fields ["_fullName"].ToString ();
			if (!CompMap.ContainsKey (fullName)) {
				CompMap.Add (fullName, comp);
				CompIDMap.Add (comp.Id, comp);
				oldComponentMap.Add (comp.Id, comp.Clone ());
			}	
		}

		public async Task<Workspace> GetOrCreateWorkspace (string name, string modelId)
		{
			var ws = await GetWorkspace (name);
			if (ws == null) {
				ws = await CreateWorkspace (name, modelId);
			}
			return ws;
		}

		public async Task<Workspace> CreateWorkspace (string name, string modelId)
		{
			var ws = await client.WorkspaceService.CreateWorkspace (new Workspace (name, modelId, ""));
			addedWorkspaceCount++;
			return ws;
		}

		public async Task<Workspace> GetWorkspace (String name)
		{
			List<Workspace> wsList = await client.WorkspaceService.GetAllWorkspaces ();
			List<Workspace> result = wsList.Where (w => w.Name.ToLower () == name.ToLower ()).ToList ();
			Workspace found = null;
			if (result.Count > 0) {
				found = result.First ();
			}
			return found;
		}

		public async Task<Component> GetComponentByPath (String workspaceName, String fullName)
		{
			var compList = await client.ComponentService.FieldSearch (workspaceName, "_fullName", fullName);
			if (compList.Count > 0) {
				var c = compList.First ();
				CacheComp (c);

				return c;
			}
			return null;
		}

		public async Task<Component> AddComp (String fullName, Component comp, Component parent = null)
		{

			if (!addedComps.Contains (fullName)) {
				addedComps.Add (fullName);
				Console.WriteLine ("Adding comp: " + comp.Name + ": " + fullName + " - ws: " + comp.RootWorkspace);
			}
			if (!CompMap.ContainsKey (fullName)) {
				var found = false;
				if (comp.RootWorkspace != workspace.Id) {
					try {
						var c = await GetComponentByPath (comp.RootWorkspace, fullName);
						if (c != null) {
							found = true;
						}
					} catch (System.Net.Http.HttpRequestException hre) {
						Console.WriteLine ("Could not find component by path: " + fullName);
					}
				}
				if (!found) {

					CompMap.Add (fullName, comp);
					comp.Fields = comp.Fields ?? new Dictionary<string, object> ();
					comp.Fields.Add ("_fullName", fullName);		
				}
			}
			comp.CachedParent = parent;
			return CompMap [fullName];
		}

		public Component GetComp (string fullName)
		{
			return CompMap.ContainsKey (fullName) ? CompMap [fullName] : null;
		}

		public async Task<bool> Save ()
		{
			if (workspace.GetHashCode () != workspaceCheckSum) {
				Console.WriteLine ("Updating workspace");
				workspace = await client.WorkspaceService.UpdateWorkspace (workspace.Id, workspace);
				updatedWorkspaceCount++;
			}
			var rootComps = CompMap.Values.Where (c => c.CachedParent == null).ToList ();
			foreach (var c in rootComps) {

				Console.WriteLine ("Saving root comp: " + c.Name + " : " + c.Fields ["_fullName"]);
				if (c.Id != null) {
					if (ComponentHasChanged (c)) {
						updatedCompCount++;
						Console.WriteLine ("Updating.");
						CompMap [c.Fields ["_fullName"].ToString ()] = await client.ComponentService.UpdateComponent (c.Id, c);
					}
				} else {
					addedCompCount++;
					CompMap [c.Fields ["_fullName"].ToString ()] = await client.ComponentService.CreateComponent (c);
				}
				await SaveChildren (c, CompMap [c.Fields ["_fullName"].ToString ()]);

			}

			await SaveReferences ();

			return true;
		}

		bool ComponentHasChanged (Component c)
		{
			if (oldComponentMap.ContainsKey (c.Id)) {
				var old = oldComponentMap [c.Id];
				if (!old.EqualsIgnoreChildren (c)) {
					Console.WriteLine ("OLD: " + old);
					Console.WriteLine ("New:" + c);
					return true;
				}
			} 
			return false;
		}

		async Task<bool> SaveChildren (Component oldParent, Component parent)
		{
			var compList = CompMap.Values.Where (c => c.CachedParent == oldParent).ToList ();
			if (compList != null && compList.Count > 0) {
				Console.WriteLine ("Saving children for: " + parent.Fields ["_fullName"]);
				foreach (var c in compList) {
					Component savedComp = null;
					c.Parent = parent.Id;
					Console.WriteLine ("Saving: " + c.Name + " - " + c.Fields ["_fullName"] + " : parent: " + parent.Fields ["_fullName"]);
					if (c.Id != null) {
						if (ComponentHasChanged (c)) {
							Console.WriteLine ("Updating component");
							updatedCompCount++;
							savedComp = await client.ComponentService.UpdateComponent (c.Id, c);
						}
					} else {
						addedCompCount++;
						savedComp = await client.ComponentService.CreateComponent (c);
					}
					if (savedComp != null) {
						CompMap [c.Fields ["_fullName"].ToString ()] = savedComp;
						savedComp.CachedParent = parent;
					}
					c.CachedParent = parent;

					await SaveChildren (c, CompMap [c.Fields ["_fullName"].ToString ()]);
				}
			}
			return true;
		}

		async Task<bool> SaveReferences ()
		{
			var keys = new List<String> (RefMap.Keys);
			foreach (var k in keys) {

				var reference = RefMap [k];
				if (reference.Source == null && reference.cachedSource != null) {
					var c = CompMap [reference.cachedSource.Fields ["_fullName"].ToString ()];
					reference.Source = c.Id;
					reference.RootWorkspace = c.RootWorkspace;
				}
				if (reference.Target == null && reference.cachedTarget != null) {
					var c = CompMap [reference.cachedTarget.Fields ["_fullName"].ToString ()];
					reference.TargetWorkspace = c.RootWorkspace;
					reference.Target = c.Id;
				}

				if (reference.Id != null) {
					if (!oldReferenceMap.ContainsKey (reference.Id) || oldReferenceMap [reference.Id] != reference.GetHashCode ()) {
						Console.WriteLine ("Updating reference ");
						updatedRefCount++;
						RefMap [k] = await client.ReferenceService.UpdateReference (reference.Id, reference);
					}
				} else {
					Console.WriteLine ("Saving reference");
					addedRefCount++;
					RefMap [k] = await client.ReferenceService.CreateReference (reference);
				}

			}
			return true;
		}

		public Reference AddReference (Component source, Component target, String description, int type)
		{
			var refNames = source.Fields ["_fullName"] + type.ToString () + target.Fields ["_fullName"];
			if (!addedRefs.Contains (refNames)) {
				addedRefs.Add (refNames);
			}
			if (!RefMap.ContainsKey (refNames)) {
				var reference = new Reference (source.RootWorkspace, description, source.Id, target.Id, type);
				reference.cachedSource = source;
				reference.cachedTarget = target;
				RefMap [refNames] = reference;
			}
			if (RefMap [refNames].Description != description) {
				RefMap [refNames].Description = description;
			}
			return RefMap [refNames];
		}
	}
}

