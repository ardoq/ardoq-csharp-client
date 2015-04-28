using System;
using System.Collections.Generic;
using Ardoq;
using Ardoq.Models;
using Ardoq.Service;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class TagServiceTest
    {
        private ITagService tagService;
        private IArdoqClient client;
        private Workspace workspaceTemplate;

        [TestFixtureSetUp]
        public void Setup()
        {
            client = TestUtils.GetClient();
            workspaceTemplate = new Workspace("Test Workspace", TestUtils.GetTestPropery("modelId"), "Hello world!");
            tagService = client.TagService;
        }

        [Test]
        public async void CreateTagTest()
        {
            Workspace workspace = await client.WorkspaceService.CreateWorkspace(workspaceTemplate, client.Org);
            var testTag = new Tag("myTag", workspace.Id, "Hello world!");
            Tag tag = await tagService.CreateTag(testTag, client.Org);
            Assert.NotNull(tag.Id);

            // roll back
            await client.WorkspaceService.DeleteWorkspace(workspace.Id, client.Org);
        }

        [Test]
        public async void DeleteTagTest()
        {
            Workspace workspace = await client.WorkspaceService.CreateWorkspace(workspaceTemplate, client.Org);
            var testTag = new Tag("myTag", workspace.Id, "Hello world!");
            Tag tag = await tagService.CreateTag(testTag, client.Org);
            try
            {
                await tagService.DeleteTag(tag.Id, client.Org);
                await tagService.GetTagById(tag.Id, client.Org);

                await client.WorkspaceService.DeleteWorkspace(workspace.Id, client.Org);
                Assert.Fail("Expected the tag to be deleted.");
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
            await client.WorkspaceService.DeleteWorkspace(workspace.Id, client.Org);
        }

        [Test]
        public async void GetTagTest()
        {
            Workspace workspace = await client.WorkspaceService.CreateWorkspace(workspaceTemplate, client.Org);

            var testTag = new Tag("myTag", workspace.Id, "Hello world!");
            await tagService.CreateTag(testTag, client.Org);
            List<Tag> allTags = await tagService.GetAllTags(client.Org);
            string id = allTags[0].Id;
            Tag result = await tagService.GetTagById(id, client.Org);
            Assert.True(id == result.Id);

            // roll back
            await client.WorkspaceService.DeleteWorkspace(workspace.Id, client.Org);
        }

        [Test]
        public async void UpdateTagTest()
        {
            Workspace workspace = await client.WorkspaceService.CreateWorkspace(workspaceTemplate, client.Org);
            var testTag = new Tag("myTag", workspace.Id, "Hello world!");
            Tag tag = await tagService.CreateTag(testTag, client.Org);
            tag.Name = "updatedName";
            Tag updatedTag = await tagService.UpdateTag(tag.Id, tag, client.Org);
            Assert.True("updatedName" == updatedTag.Name);

            // roll back
            await client.WorkspaceService.DeleteWorkspace(workspace.Id, client.Org);
        }
    }
}