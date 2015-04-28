using System;

namespace Ardoq.Models
{
    public interface IModelBase
    {
        string Id { get; set; }
        int? VersionCounter { get; set; }
        DateTime? Created { get; set; }
        DateTime? LastUpdated { get; set; }
    }

    public interface IModel : IModelBase
    {
        int GetReferenceTypeByName(string name);
        String GetComponentTypeByName(String name);
    }
}