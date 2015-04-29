using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
	public interface IDeprecatedModelService
	{
		[Get ("/api/model")]
		Task<List<Model>> GetAllModels(
            [AliasAs ("org")] string org = null);

		[Get ("/api/model/{id}")]
		Task<Model> GetModelById(
            [AliasAs ("id")] String id, 
            [AliasAs ("org")] string org = null);
	}
}