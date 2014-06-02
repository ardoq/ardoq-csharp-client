using System;
using Newtonsoft.Json;

namespace Ardoq.Models
{
    public class WorkspaceBranch : IModel, IEquatable<WorkspaceBranch>
    {
        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public String CreatedBy { get; set; }

        [JsonProperty(PropertyName = "workspace-id", NullValueHandling = NullValueHandling.Ignore)]
        public String WorkspaceId { get; set; }

        [JsonProperty(PropertyName = "branch-name", NullValueHandling = NullValueHandling.Ignore)]
        public String BranchName { get; set; }

        #region Methods

        public bool Equals(WorkspaceBranch other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && VersionCounter == other.VersionCounter &&
                   Created.Equals(other.Created) && LastUpdated.Equals(other.LastUpdated) &&
                   string.Equals(CreatedBy, other.CreatedBy) && string.Equals(WorkspaceId, other.WorkspaceId) &&
                   string.Equals(BranchName, other.BranchName);
        }

        public override string ToString()
        {
            return "WorkspaceBranch{" +
                   "id='" + Id + '\'' +
                   ", created=" + Created +
                   ", createdBy='" + CreatedBy + '\'' +
                   ", workspaceId='" + WorkspaceId + '\'' +
                   ", branchName='" + BranchName + '\'' +
                   '}';
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WorkspaceBranch) obj);
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
                hashCode = (hashCode*397) ^ (WorkspaceId != null ? WorkspaceId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (BranchName != null ? BranchName.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

        [JsonProperty(PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
        public int? VersionCounter { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdated { get; set; }
    }
}