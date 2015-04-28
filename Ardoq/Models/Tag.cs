using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ardoq.Models
{
    public class Tag : IModelBase, IEquatable<Tag>
    {
        #region Properties

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public String CreatedBy { get; set; }

        [JsonProperty(PropertyName = "rootWorkspace", NullValueHandling = NullValueHandling.Ignore)]
        public String RootWorkspace { get; set; }

        [JsonProperty(PropertyName = "components", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> Components { get; set; }

        [JsonProperty(PropertyName = "references", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> References { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public String Description { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdated { get; set; }

        [JsonProperty(PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
        public int? VersionCounter { get; set; }

        #endregion

        #region ctor

        public Tag()
        {
        }

        public Tag(String name, String rootWorkspace, String description)
        {
            Name = name;
            RootWorkspace = rootWorkspace;
            Description = description;
        }

        public Tag(String name, String rootWorkspace, String description, List<string> components,
            List<string> references)
            : this(name, rootWorkspace, description)
        {
            Components = components;
            References = references;
        }

        #endregion

        #region methods

        public bool Equals(Tag other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(CreatedBy, other.CreatedBy) &&
                   Created.Equals(other.Created) && LastUpdated.Equals(other.LastUpdated) && string.Equals(Id, other.Id) &&
                   VersionCounter == other.VersionCounter && string.Equals(RootWorkspace, other.RootWorkspace) &&
                   Equals(Components, other.Components) && Equals(References, other.References) &&
                   string.Equals(Description, other.Description);
        }

        public void AddReference(String refId)
        {
            if (null == References)
            {
                References = new List<String>();
            }
            if (!References.Contains(refId))
            {
                References.Add(refId);
            }
        }

        public void AddComponent(String compId)
        {
            if (null == Components)
            {
                Components = new List<String>();
            }
            if (!Components.Contains(compId))
            {
                Components.Add(compId);
            }
        }

        public override string ToString()
        {
            return "Tag{" +
                   "id='" + Id + '\'' +
                   ", name='" + Name + '\'' +
                   ", created=" + Created +
                   ", createdBy='" + CreatedBy + '\'' +
                   ", lastUpdated=" + LastUpdated +
                   ", _version=" + VersionCounter +
                   ", rootWorkspace='" + RootWorkspace + '\'' +
                   ", components=" + Components +
                   ", references=" + References +
                   ", description='" + Description + '\'' +
                   '}';
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Tag) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Created.GetHashCode();
                hashCode = (hashCode*397) ^ LastUpdated.GetHashCode();
                hashCode = (hashCode*397) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ VersionCounter.GetHashCode();
                hashCode = (hashCode*397) ^ (RootWorkspace != null ? RootWorkspace.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Components != null ? Components.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (References != null ? References.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}