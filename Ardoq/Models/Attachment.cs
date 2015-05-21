using System;
using Newtonsoft.Json;

namespace Ardoq.Models
{
    public class Attachment : IModelBase, IEquatable<Attachment>
    {
        #region Properties

        [JsonProperty(PropertyName = "created-by", NullValueHandling = NullValueHandling.Ignore)]
        public String CreatedBy { get; set; }

        [JsonProperty(PropertyName = "content-type", NullValueHandling = NullValueHandling.Ignore)]
        public String ContentType { get; set; }

        [JsonProperty(PropertyName = "filename", NullValueHandling = NullValueHandling.Ignore)]
        public String Filename { get; set; }

        [JsonProperty(PropertyName = "uri", NullValueHandling = NullValueHandling.Ignore)]
        public String Url { get; set; }

        [JsonProperty(PropertyName = "size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size { get; set; }

        [JsonProperty(PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
        public int? VersionCounter { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdated { get; set; }

        #endregion

        #region methods

        public bool Equals(Attachment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && VersionCounter == other.VersionCounter &&
                   Created.Equals(other.Created) && LastUpdated.Equals(other.LastUpdated) &&
                   string.Equals(CreatedBy, other.CreatedBy) && string.Equals(ContentType, other.ContentType) &&
                   string.Equals(Filename, other.Filename) && string.Equals(Url, other.Url) && Size == other.Size;
        }

        public override string ToString()
        {
            return "Attachment{" +
                   "id='" + Id + '\'' +
                   ", created=" + Created +
                   ", createdBy='" + CreatedBy + '\'' +
                   ", lastUpdated=" + LastUpdated +
                   ", contentType='" + ContentType + '\'' +
                   ", filename='" + Filename + '\'' +
                   ", uri='" + Url + '\'' +
                   ", size=" + Size +
                   '}';
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Attachment) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ VersionCounter.GetHashCode();
                hashCode = (hashCode*397) ^ Created.GetHashCode();
                hashCode = (hashCode*397) ^ LastUpdated.GetHashCode();
                hashCode = (hashCode*397) ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ContentType != null ? ContentType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Filename != null ? Filename.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Size.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}