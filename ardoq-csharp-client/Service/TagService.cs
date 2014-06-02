using System.Collections.Generic;
using System.Threading.Tasks;
using Ardoq.Models;
using Ardoq.Service.Interface;

namespace Ardoq.Service
{
    public class TagService : ServiceBase, ITagService
    {
        internal TagService(ITagService service, string org)
            : base(org)
        {
            Service = service;
        }

        private ITagService Service { get; set; }

        public Task<List<Tag>> GetAllTags(string org)
        {
            return Service.GetAllTags(org);
        }

        public Task<Tag> GetTagById(string id, string org)
        {
            return Service.GetTagById(id, org);
        }

        public Task<Tag> CreateTag(Tag tag, string org)
        {
            return Service.CreateTag(tag, org);
        }

        public Task<Tag> UpdateTag(string id, Tag tag, string org)
        {
            return Service.UpdateTag(id, tag, org);
        }

        public Task DeleteTag(string id, string org)
        {
            return Service.DeleteTag(id, org);
        }

        public Task<List<Tag>> GetAllTags()
        {
            return GetAllTags(Org);
        }

        public Task<Tag> GetTagById(string id)
        {
            return GetTagById(id, Org);
        }

        public Task<Tag> CreateTag(Tag tag)
        {
            return CreateTag(tag, Org);
        }

        public Task<Tag> UpdateTag(string id, Tag tag)
        {
            return UpdateTag(id, tag, Org);
        }

        public Task DeleteTag(string id)
        {
            return DeleteTag(id, Org);
        }
    }
}