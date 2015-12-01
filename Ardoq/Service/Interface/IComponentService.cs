using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
	public interface IComponentService
	{
		[Get ("/api/component")]
		Task<List<Component>> GetAllComponents(
            [AliasAs ("org")] string org = null);

		[Get ("/api/component/{id}")]
		Task<Component> GetComponentById(
            [AliasAs ("id")] string id, 
            [AliasAs ("org")] string org = null);

		[Post ("/api/component")]
		Task<Component> CreateComponent(
            [Body] Component component, 
            [AliasAs ("org")] string org = null);

		[Put ("/api/component/{id}")]
		Task<Component> UpdateComponent(
            [AliasAs ("id")] string id, 
            [Body] Component component, 
            [AliasAs ("org")] string org = null);

		[Delete ("/api/component/{id}")]
		Task DeleteComponent(
            [AliasAs ("id")] string id, 
            [AliasAs ("org")] string org = null);

		[Get ("/api/component/fieldsearch?{fieldquery}")]
        Task<List<Component>> FieldSearch(
            [AliasAs("workspace")] string name,
            [AliasAs("fieldquery")] Dictionary<string, string> fieldquery, 
            [AliasAs("org")] string org = null);
	}
}