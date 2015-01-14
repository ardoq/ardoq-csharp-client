using System;
using Newtonsoft.Json;

namespace Ardoq.Models
{
	public class Reference : IModel, IEquatable<Reference>
	{
		#region Properties

		[JsonProperty (PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
		public String CreatedBy { get; set; }

		[JsonProperty (PropertyName = "rootWorkspace", NullValueHandling = NullValueHandling.Ignore)]
		public String RootWorkspace { get; set; }

		[JsonProperty (PropertyName = "targetWorkspace", NullValueHandling = NullValueHandling.Ignore)]
		public String TargetWorkspace { get; set; }

		[JsonProperty (PropertyName = "source", NullValueHandling = NullValueHandling.Ignore)]
		public String Source { get; set; }

		[JsonProperty (PropertyName = "target", NullValueHandling = NullValueHandling.Ignore)]
		public String Target { get; set; }

		[JsonProperty (PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
		public int Type { get; set; }

		[JsonProperty (PropertyName = "order", NullValueHandling = NullValueHandling.Ignore)]
		public int Order { get; set; }

		[JsonProperty (PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
		public String Description { get; set; }

		[JsonProperty (PropertyName = "returnValue", NullValueHandling = NullValueHandling.Ignore)]
		public String ReturnValue { get; set; }

		[JsonProperty (PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Created { get; set; }

		[JsonProperty (PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? LastUpdated { get; set; }

		[JsonProperty (PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty (PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
		public int? VersionCounter { get; set; }

		#endregion

		public Component cachedSource { get; set; }


		public Component cachedTarget { get; set; }

		#region ctor

		public Reference (String rootWorkspace, String description, String source, String target, int type)
		{
			RootWorkspace = rootWorkspace;
			Description = description;
			Source = source;
			Target = target;
			Type = type;
		}

		#endregion

		#region methods

		public bool Equals (Reference other)
		{
			if (ReferenceEquals (null, other))
				return false;
			if (ReferenceEquals (this, other))
				return true;
			return string.Equals (CreatedBy, other.CreatedBy) && Created.Equals (other.Created) &&
			LastUpdated.Equals (other.LastUpdated) && string.Equals (Id, other.Id) &&
			VersionCounter == other.VersionCounter && string.Equals (RootWorkspace, other.RootWorkspace) &&
			string.Equals (Source, other.Source) && string.Equals (Target, other.Target) && Type == other.Type && ReturnValue == other.ReturnValue &&
			Order == other.Order && string.Equals (Description, other.Description);
		}

		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj))
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != GetType ())
				return false;
			return Equals ((Reference)obj);
		}

		public override int GetHashCode ()
		{
			unchecked {
				int hashCode = (CreatedBy != null ? CreatedBy.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ Created.GetHashCode ();
				hashCode = (hashCode * 397) ^ LastUpdated.GetHashCode ();
				hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ VersionCounter.GetHashCode ();
				hashCode = (hashCode * 397) ^ (RootWorkspace != null ? RootWorkspace.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Source != null ? Source.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Target != null ? Target.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ Type;
				hashCode = (hashCode * 397) ^ Order;
				hashCode = (hashCode * 397) ^ (ReturnValue != null ? ReturnValue.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode () : 0);
				return hashCode;
			}
		}

		#endregion

		public override string ToString ()
		{
			return "Reference{" +
			"id='" + Id + '\'' +
			", created=" + Created +
			", createdBy='" + CreatedBy + '\'' +
			", lastUpdated=" + LastUpdated +
			", _version=" + VersionCounter +
			", rootWorkspace='" + RootWorkspace + '\'' +
			", type=" + Type +
			", source='" + Source + '\'' +
			", target='" + Target + '\'' +
			", description='" + Description + '\'' +
			'}';
		}
	}
}