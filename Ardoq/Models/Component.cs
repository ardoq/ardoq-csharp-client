using System;
using System.Collections.Generic;
using System.Reflection;
using Ardoq.Models.Converters;
using Ardoq.Service;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;

namespace Ardoq.Models
{
	/// <summary>
	///     Represents an Ardoq component or also called a page. Use the ComponentService to update.
	///     <remarks>
	///         Every updates or modifications done via <see cref="ComponentService" /> results in a new Immutable instance.
	///     </remarks>
	/// </summary>
	[JsonConverter (typeof(ComponentConverter)),Serializable]
	public class Component : IModel, IEquatable<Component>
	{
		#region Properties

		[JsonProperty (PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
		public String Name { get; set; }

		[JsonProperty (PropertyName = "model", NullValueHandling = NullValueHandling.Ignore)]
		public String Model { get; set; }

		[JsonProperty (PropertyName = "state", NullValueHandling = NullValueHandling.Ignore)]
		public String State { get; set; }


		[JsonProperty (PropertyName = "created-by", NullValueHandling = NullValueHandling.Ignore)]
		public String CreatedBy { get; set; }

		[JsonProperty (PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
		public string Version { get; set; }


		[JsonProperty (PropertyName = "rootWorkspace", NullValueHandling = NullValueHandling.Ignore)]
		public String RootWorkspace { get; set; }

		[JsonProperty (PropertyName = "children", NullValueHandling = NullValueHandling.Ignore)]
		public List<String> Children { get; set; }

		[JsonProperty (PropertyName = "parent", NullValueHandling = NullValueHandling.Ignore)]
		public String Parent { get; set; }

		[JsonProperty (PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
		public String Type { get; set; }


		[JsonProperty (PropertyName = "typeId", NullValueHandling = NullValueHandling.Ignore)]
		public String TypeId { get; set; }


		[JsonProperty (PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
		public String Description { get; set; }

		//[JsonConverter(typeof(ComponentMissingFieldsConverter))]
		[JsonIgnore]
		public Dictionary<string, object> Fields { get; set; }

		[JsonProperty (PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Created { get; set; }

		[JsonProperty (PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? LastUpdated { get; set; }

		[JsonProperty (PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty (PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
		public int? VersionCounter { get; set; }

		#endregion

		[JsonIgnore]
		public Component CachedParent { get; set; }

		#region ctor

		public Component ()
		{
			Fields = new Dictionary<string, object> ();
		}

		public Component (String name, String rootWorkspace, String description)
		{
			Name = name;
			RootWorkspace = rootWorkspace;
			Description = description;
			Parent = null;
		}

		public Component (String name, String rootWorkspace, String description, String typeId)
		{
			Name = name;
			RootWorkspace = rootWorkspace;
			Description = description;
			Parent = null;
			TypeId = typeId;
		}


		public Component (String name, String rootWorkspace, String description, String typeId, String parent)
			: this (name, rootWorkspace, description)
		{
			Parent = parent;
			TypeId = typeId;
		}

		#endregion

		#region methods

		public bool Equals (Component other)
		{
			if (ReferenceEquals (null, other))
				return false;
			if (ReferenceEquals (this, other))
				return true;
			return string.Equals (Name, other.Name) && string.Equals (Model, other.Model) &&
			string.Equals (State, other.State) && string.Equals (CreatedBy, other.CreatedBy) &&
			Created.Equals (other.Created) && LastUpdated.Equals (other.LastUpdated) && string.Equals (Id, other.Id) &&
			VersionCounter == other.VersionCounter && string.Equals (Version, other.Version) &&
			string.Equals (RootWorkspace, other.RootWorkspace) && Equals (Children, other.Children) &&
			string.Equals (Parent, other.Parent) && string.Equals (Type, other.Type) &&
			string.Equals (TypeId, other.TypeId) && string.Equals (Description, other.Description);
		}


		public bool EqualsIgnoreChildren (Component other)
		{
			if (ReferenceEquals (null, other))
				return false;
			if (ReferenceEquals (this, other))
				return true;
			return string.Equals (Name, other.Name) && string.Equals (Model, other.Model) &&
			string.Equals (State, other.State) && string.Equals (CreatedBy, other.CreatedBy) &&
			Created.Equals (other.Created) && LastUpdated.Equals (other.LastUpdated) && string.Equals (Id, other.Id) &&
			VersionCounter == other.VersionCounter && string.Equals (Version, other.Version) &&
			string.Equals (RootWorkspace, other.RootWorkspace) &&
			string.Equals (Parent, other.Parent) && string.Equals (Type, other.Type) &&
			string.Equals (TypeId, other.TypeId) && string.Equals (Description, other.Description) && FieldEquals (Fields, other.Fields);
		}



		bool FieldEquals (Dictionary<string, Object> fields, Dictionary<string, Object> other)
		{
			if (fields.Count != other.Count) {
				return false;
			}

			foreach (var f in fields.Keys) {
				if (!other.ContainsKey (f)) {
					return false;
				} else {
					var a = fields [f];
					a = (a is long || a is short) ? Convert.ToInt32 (a) : a;
					var b = other [f];
					if (!a.Equals (b)) {
						return false;
					}
				}
			}

			foreach (var d in other.Keys) {
				if (!fields.ContainsKey (d)) {
					return false;
				} else if (fields [d] != other [d]) {
					var a = fields [d];
					a = (a is long || a is short) ? Convert.ToInt32 (a) : a;
					var b = other [d];
					if (!a.Equals (b)) {
						return false;
					}
				}
			}

			return true;
		}

		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj))
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != GetType ())
				return false;
			return Equals ((Component)obj);
		}

		public override int GetHashCode ()
		{
			unchecked {
				int hashCode = (Name != null ? Name.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Model != null ? Model.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (CreatedBy != null ? CreatedBy.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ Created.GetHashCode ();
				hashCode = (hashCode * 397) ^ LastUpdated.GetHashCode ();
				hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ VersionCounter.GetHashCode ();
				hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (RootWorkspace != null ? RootWorkspace.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Children != null ? Children.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Parent != null ? Parent.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (TypeId != null ? TypeId.GetHashCode () : 0);
				hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode () : 0);
				return hashCode;
			}
		}

		public Component Clone ()
		{
			MemoryStream stream = new MemoryStream ();
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (stream, this);
			stream.Position = 0;
			return (Component)formatter.Deserialize (stream);
		}

		public override string ToString ()
		{
			return "Component{" +
			"id='" + Id + '\'' +
			", name='" + Name + '\'' +
			", model='" + Model + '\'' +
			", state='" + State + '\'' +
			", created=" + Created +
			", createdBy='" + CreatedBy + '\'' +
			", lastUpdated=" + LastUpdated +
			", version='" + Version + '\'' +
			", _version=" + VersionCounter +
			", rootWorkspace='" + RootWorkspace + '\'' +
			", children=" + Children +
			", parent='" + Parent + '\'' +
			", type='" + Type + '\'' +
			", typeId='" + TypeId + '\'' +
			", description='" + Description + '\'' +
			'}';
		}

		#endregion
	}
}