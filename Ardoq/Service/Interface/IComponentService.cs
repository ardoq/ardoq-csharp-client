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
		Task<List<Component>> GetAllComponents ([AliasAs ("org")] string org);

		[Get ("/api/component/{id}")]
		Task<Component> GetComponentById ([AliasAs ("id")] String id, [AliasAs ("org")] string org);

		[Post ("/api/component")]
		Task<Component> CreateComponent ([Body] Component component, [AliasAs ("org")] string org);

		[Put ("/api/component/{id}")]
		Task<Component> UpdateComponent ([AliasAs ("id")] String id, [Body] Component component,
		                                 [AliasAs ("org")] string org);

		[Delete ("/api/component/{id}")]
		Task DeleteComponent ([AliasAs ("id")] String id, [AliasAs ("org")] string org);



		[Get ("/api/component/fieldsearch?{fieldquery}")]
		Task<List<Component>> FieldSearch ([AliasAs ("workspace")] String name, string fieldquery, [AliasAs ("org")] string org);
	}
}