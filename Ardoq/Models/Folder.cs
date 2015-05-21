using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ardoq.Models
{
	public class Folder : IModelBase, IEquatable<Folder>
	{
		#region Properties

		[JsonProperty (PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty (PropertyName = "created-by", NullValueHandling = NullValueHandling.Ignore)]
		public String CreatedBy { get; set; }

		[JsonProperty (PropertyName = "workspaces", NullValueHandling = NullValueHandling.Ignore)]
		public List<String> Workspaces { get; set; }

		[JsonProperty (PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
		public String Description { get; set; }

		[JsonProperty (PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Created { get; set; }

		[JsonProperty (PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? LastUpdated { get; set; }

		[JsonProperty (PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty (PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
		public int? VersionCounter { get; set; }

		#endregion

		#region ctor

		public Folder ()
		{
		}

		public Folder (String name, String description)
		{
			Name = name;
			Description = description;
		}

		#endregion

		#region methods

		public bool Equals (Folder other)
		{
			if (ReferenceEquals (null, other))
				return false;
			if (ReferenceEquals (this, other))
				return true;
			return string.Equals (Name, other.Name) && string.Equals (CreatedBy, other.CreatedBy) &&
			Created.Equals (other.Created) && LastUpdated.Equals (other.LastUpdated) && string.Equals (Id, other.Id) &&
			VersionCounter == other.VersionCounter &&
			Equals (Workspaces, other.Workspaces) &&
			string.Equals (Description, other.Description);
		}

		public override string ToString ()
		{
			return "Tag{" +
			"id='" + Id + '\'' +
			", name='" + Name + '\'' +
			", created=" + Created +
			", createdBy='" + CreatedBy + '\'' +
			", lastUpdated=" + LastUpdated +
			", _version=" + VersionCounter +
			", workspaces='" + Workspaces + '\'' +
			", description='" + Description + '\'' +
			'}';
		}

		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj))
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != GetType ())
				return false;
			return Equals ((Tag)obj);
		}

		public override int GetHashCode ()
		{
			unchecked {
				int hashCode = (Name != null ? Name.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (CreatedBy != null ? CreatedBy.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ Created.GetHashCode ();
				hashCode = (hashCode * 397) ^ LastUpdated.GetHashCode ();
				hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ VersionCounter.GetHashCode ();
				hashCode = (hashCode * 397) ^ (Workspaces != null ? Workspaces.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode () : 0);
				return hashCode;
			}
		}

		#endregion
	}
}