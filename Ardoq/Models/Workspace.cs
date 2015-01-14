using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ardoq.Models
{
	public class Workspace : IModel, IEquatable<Workspace>
	{
		public Workspace (String name, String componentModel, String description)
		{
			Name = name;
			ComponentModel = componentModel;
			Description = description;
		}

		#region Methods

		public bool Equals (Workspace other)
		{
			if (ReferenceEquals (null, other))
				return false;
			if (ReferenceEquals (this, other))
				return true;
			return string.Equals (Id, other.Id) && VersionCounter == other.VersionCounter &&
			Created.Equals (other.Created) && string.Equals (Name, other.Name) &&
			string.Equals (ComponentModel, other.ComponentModel) && LastUpdated.Equals (other.LastUpdated) &&
			string.Equals (CreatedBy, other.CreatedBy) && Equals (Components, other.Components) &&
			Equals (References, other.References) && Equals (Tags, other.Tags) && string.Equals (Type, other.Type) &&
			string.Equals (Description, other.Description);
		}

		public override bool Equals (Object obj)
		{
			if (ReferenceEquals (null, obj))
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != GetType ())
				return false;
			return Equals ((Workspace)obj);
		}

		public override string ToString ()
		{
			return "Workspace{" +
			"id='" + Id + '\'' +
			", name='" + Name + '\'' +
			", componentModel='" + ComponentModel + '\'' +
			", created=" + Created +
			", createdBy='" + CreatedBy + '\'' +
			", lastUpdated=" + LastUpdated +
			", _version=" + VersionCounter +
			", components=" + Components +
			", references=" + References +
			", tags=" + Tags +
			", type='" + Type + '\'' +
			", description='" + Description + '\'' +
			'}';
		}

		public override int GetHashCode ()
		{
			unchecked {
				int hashCode = (Id != null ? Id.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ VersionCounter.GetHashCode ();
				hashCode = (hashCode * 397) ^ Created.GetHashCode ();
				hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (ComponentModel != null ? ComponentModel.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ LastUpdated.GetHashCode ();
				hashCode = (hashCode * 397) ^ (CreatedBy != null ? CreatedBy.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Components != null ? Components.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (References != null ? References.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Tags != null ? Tags.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Views != null ? Views.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode () : 0);
				return hashCode;
			}
		}

		#endregion

		[JsonProperty (PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
		public String Name { get; set; }

		[JsonProperty (PropertyName = "componentModel", NullValueHandling = NullValueHandling.Ignore)]
		public String ComponentModel { get; set; }

		[JsonProperty (PropertyName = "created-by", NullValueHandling = NullValueHandling.Ignore)]
		public String CreatedBy { get; set; }

		[JsonProperty (PropertyName = "components", NullValueHandling = NullValueHandling.Ignore)]
		public List<String> Components { get; set; }

		[JsonProperty (PropertyName = "origin", NullValueHandling = NullValueHandling.Ignore)]
		public Origin Origin { get; set; }

		[JsonProperty (PropertyName = "references", NullValueHandling = NullValueHandling.Ignore)]
		public List<String> References { get; set; }

		[JsonProperty (PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
		public List<String> Tags { get; set; }

		[JsonProperty (PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
		public String Type { get; set; }

		[JsonProperty (PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
		public String Description { get; set; }

		[JsonProperty (PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty (PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
		public int? VersionCounter { get; set; }

		[JsonProperty (PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Created { get; set; }

		[JsonProperty (PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? LastUpdated { get; set; }

		[JsonProperty (PropertyName = "views", NullValueHandling = NullValueHandling.Ignore)]
		public List<String> Views { get; set; }
	}
}