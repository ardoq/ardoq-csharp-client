using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Refit;

namespace Ardoq.Service.Interface
{
    public interface IFieldService
    {
        [Get("/api/field")]
        Task<List<Field>> GetAllFields(
            [AliasAs("org")] string org = null);

        [Get("/api/field/{id}")]
        Task<Field> GetFieldById(
            [AliasAs("id")] String id, 
            [AliasAs("org")] string org = null);

        [Post("/api/field")]
        Task<Field> CreateField(
            [Body] Field field, 
            [AliasAs("org")] string org = null);

        [Put("/api/field/{id}")]
        Task<Field> UpdateField(
            [AliasAs("id")] String id, 
            [Body] Field field, 
            [AliasAs("org")] string org = null);

        [Delete("/api/field/{id}")]
        Task DeleteField(
            [AliasAs("id")] String id, 
            [AliasAs("org")] string org = null);
    }
}