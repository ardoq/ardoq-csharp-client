using System;

namespace Ardoq.Models
{
    public interface IModel
    {
        string Id { get; set; }

        int? VersionCounter { get; set; }

        DateTime? Created { get; set; }

        DateTime? LastUpdated { get; set; }
    }
}