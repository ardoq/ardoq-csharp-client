using System;
using Newtonsoft.Json;

namespace Ardoq.Models
{
    public class WorkspaceBranchRequest
    {
        public WorkspaceBranchRequest(String branchName)
        {
            BranchName = branchName;
        }

        [JsonProperty(PropertyName = "branch-name", NullValueHandling = NullValueHandling.Ignore)]
        public String BranchName { get; set; }

        public override String ToString()
        {
            return "WorkspaceBranchRequest{" +
                   "branchName='" + BranchName + '\'' +
                   '}';
        }
    }
}