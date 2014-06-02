using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ardoq.Models
{
    public class AggregatedWorkspace : IModel, IEquatable<AggregatedWorkspace>
    {
        #region properties

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "componentModel", NullValueHandling = NullValueHandling.Ignore)]
        public String ComponentModel { get; set; }


        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public String CreatedBy { get; set; }

        [JsonProperty(PropertyName = "components", NullValueHandling = NullValueHandling.Ignore)]
        public List<Component> Components { get; set; }

        [JsonProperty(PropertyName = "references", NullValueHandling = NullValueHandling.Ignore)]
        public List<Reference> References { get; set; }

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<Tag> Tags { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public String Type { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public String Description { get; set; }

        [JsonProperty(PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
        public String Id { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdated { get; set; }

        [JsonProperty(PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
        public int? VersionCounter { get; set; }

        #endregion

        #region methods

        public bool Equals(AggregatedWorkspace other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && Created.Equals(other.Created) && string.Equals(Name, other.Name) &&
                   string.Equals(ComponentModel, other.ComponentModel) && string.Equals(CreatedBy, other.CreatedBy) &&
                   LastUpdated.Equals(other.LastUpdated) && VersionCounter == other.VersionCounter &&
                   Equals(Components, other.Components) && Equals(References, other.References) &&
                   Equals(Tags, other.Tags) && string.Equals(Type, other.Type) &&
                   string.Equals(Description, other.Description);
        }

        public override string ToString()
        {
            return "AggregatedWorkspace{" +
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AggregatedWorkspace) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Created.GetHashCode();
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ComponentModel != null ? ComponentModel.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ LastUpdated.GetHashCode();
                hashCode = (hashCode*397) ^ VersionCounter.GetHashCode();
                hashCode = (hashCode*397) ^ (Components != null ? Components.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (References != null ? References.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Tags != null ? Tags.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}