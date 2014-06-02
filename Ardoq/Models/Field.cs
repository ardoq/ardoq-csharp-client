using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ardoq.Models
{
    public class Field : IModel, IEquatable<Field>
    {
        #region Properties

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public String CreatedBy { get; set; }

        [JsonProperty(PropertyName = "model", NullValueHandling = NullValueHandling.Ignore)]
        public String Model { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof (StringEnumConverter))]
        public FieldType Type { get; set; }

        [JsonProperty(PropertyName = "label", NullValueHandling = NullValueHandling.Ignore)]
        public String Label { get; set; }

        [JsonProperty(PropertyName = "componentType", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> ComponentType { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public String Description { get; set; }

        [JsonProperty(PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
        public int? VersionCounter { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdated { get; set; }

        #endregion

        #region ctor

        public Field(String name, String label, String modelId, List<String> componentType, FieldType type)
        {
            Name = name;
            Label = label;
            Model = modelId;
            ComponentType = componentType;
            Type = type;
        }

        #endregion

        #region Methods

        public bool Equals(Field other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && VersionCounter == other.VersionCounter &&
                   Created.Equals(other.Created) && string.Equals(Name, other.Name) &&
                   string.Equals(CreatedBy, other.CreatedBy) && LastUpdated.Equals(other.LastUpdated) &&
                   string.Equals(Model, other.Model) && Type == other.Type && string.Equals(Label, other.Label) &&
                   Equals(ComponentType, other.ComponentType) && string.Equals(Description, other.Description);
        }

        public override string ToString()
        {
            return "Field{" +
                   "id='" + Id + '\'' +
                   ", name='" + Name + '\'' +
                   ", created=" + Created +
                   ", createdBy='" + CreatedBy + '\'' +
                   ", lastUpdated=" + LastUpdated +
                   ", _version=" + VersionCounter +
                   ", model='" + Model + '\'' +
                   ", type=" + Type +
                   ", label='" + Label + '\'' +
                   ", componentType='" + ComponentType + '\'' +
                   ", description='" + Description + '\'' +
                   '}';
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Field) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ VersionCounter.GetHashCode();
                hashCode = (hashCode*397) ^ Created.GetHashCode();
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ LastUpdated.GetHashCode();
                hashCode = (hashCode*397) ^ (Model != null ? Model.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) Type;
                hashCode = (hashCode*397) ^ (Label != null ? Label.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ComponentType != null ? ComponentType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}