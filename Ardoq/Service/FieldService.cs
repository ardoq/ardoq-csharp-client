using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;

namespace Ardoq.Service
{
    public class FieldService : ServiceBase, IFieldService
    {
        internal FieldService(IFieldService service, string org) : base(org)
        {
            Service = service;
        }

        private IFieldService Service { get; set; }

        public Task<List<Field>> GetAllFields(string org = null)
        {
            org = org ?? Org;
            return Service.GetAllFields(org);
        }

        public Task<Field> GetFieldById(string id, string org = null)
        {
            org = org ?? Org;
            return Service.GetFieldById(id, org);
        }

        public Task<Field> CreateField(Field field, string org = null)
        {
            org = org ?? Org;
            return Service.CreateField(field, org);
        }

        public Task<Field> UpdateField(string id, Field field, string org = null)
        {
            org = org ?? Org;
            return Service.UpdateField(id, field, org);
        }

        public Task DeleteField(string id, string org = null)
        {
            org = org ?? Org;
            return Service.DeleteField(id, org);
        }
    }
}