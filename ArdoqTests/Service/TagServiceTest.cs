using System;
using System.Collections.Generic;
using Ardoq;
using Ardoq.Models;
using Ardoq.Service;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class TagServiceTest
    {
        private ITagService tagService;
        private IArdoqClient client;
        private Workspace workspaceTemplate;

        [OneTimeSetUp]
        public void Setup()
        {
            client = TestUtils.GetClient();
            workspaceTemplate = new Workspace("Test Workspace", TestUtils.GetTestPropery("modelId"), "Hello world!");
            tagService = client.TagService;
        }

        [Test]
        public async Task CreateTagTest()
        {
            Workspace workspace = await client.WorkspaceService.CreateWorkspace(workspaceTemplate);
            var testTag = new Tag("myTag", workspace.Id, "Hello world!");
            Tag tag = await tagService.CreateTag(testTag);
            Assert.NotNull(tag.Id);

            // roll back
            await client.WorkspaceService.DeleteWorkspace(workspace.Id);
        }

        [Test]
        public async Task DeleteTagTest()
        {
            Workspace workspace = await client.WorkspaceService.CreateWorkspace(workspaceTemplate);
            var testTag = new Tag("myTag", workspace.Id, "Hello world!");
            Tag tag = await tagService.CreateTag(testTag);
            try
            {
                await tagService.DeleteTag(tag.Id);
                await tagService.GetTagById(tag.Id);

                await client.WorkspaceService.DeleteWorkspace(workspace.Id);
                Assert.Fail("Expected the tag to be deleted.");
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
            await client.WorkspaceService.DeleteWorkspace(workspace.Id);
        }

        [Test]
        public async Task GetTagTest()
        {
            Workspace workspace = await client.WorkspaceService.CreateWorkspace(workspaceTemplate);

            var testTag = new Tag("myTag", workspace.Id, "Hello world!");
            await tagService.CreateTag(testTag);
            List<Tag> allTags = await tagService.GetAllTags();
            string id = allTags[0].Id;
            Tag result = await tagService.GetTagById(id);
            Assert.True(id == result.Id);

            // roll back
            await client.WorkspaceService.DeleteWorkspace(workspace.Id);
        }

        [Test]
        public async Task UpdateTagTest()
        {
            Workspace workspace = await client.WorkspaceService.CreateWorkspace(workspaceTemplate);
            var testTag = new Tag("myTag", workspace.Id, "Hello world!");
            Tag tag = await tagService.CreateTag(testTag);
            tag.Name = "updatedName";
            Tag updatedTag = await tagService.UpdateTag(tag.Id, tag);
            Assert.True("updatedName" == updatedTag.Name);

            // roll back
            await client.WorkspaceService.DeleteWorkspace(workspace.Id);
        }
    }
}