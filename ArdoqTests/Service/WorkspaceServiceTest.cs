using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WorkspaceServiceTest
    {
        private IWorkspaceService service;
        private IArdoqClient client;


        [OneTimeSetUp]
        public void Before()
        {
            client = TestUtils.GetClient();
            service = client.WorkspaceService;
        }

        private static Workspace CreateWorkspaceTemplate()
        {
            return new Workspace("Test Workspace", TestUtils.GetTestProperty("modelId"), "Hello world!");
        }

        [Test]
        public async Task BranchWorkspaceTest()
        {
            Workspace workspaceTemplate = CreateWorkspaceTemplate();
            Workspace result = await service.CreateWorkspace(workspaceTemplate);
            Workspace myBranch = await service.BranchWorkspace(result.Id, new WorkspaceBranchRequest("myBranch"));

            Assert.True(result.Id == myBranch.Origin.Id);
            Assert.True("myBranch" == myBranch.Name);
            Assert.True(result.VersionCounter == myBranch.Origin.VersionCounter);

            // rolling back
            await service.DeleteWorkspace(result.Id);
        }


        [Test]
        public async Task CreateWorkspaceTest()
        {
            Workspace workspaceTemplate = CreateWorkspaceTemplate();
            Workspace result = await service.CreateWorkspace(workspaceTemplate);
            Assert.NotNull(result.Id);
            // roll back
            await service.DeleteWorkspace(result.Id);
        }

        [Test]
        public async Task DeleteWorkspaceTest()
        {
            Workspace workspaceTemplate = CreateWorkspaceTemplate();
            Workspace result = await service.CreateWorkspace(workspaceTemplate);
            try
            {
                await service.DeleteWorkspace(result.Id);
            }
            catch (Exception)
            {
                Assert.Fail("Expected the workspace to be existing before deletion.");
            }

            try
            {
                await service.GetWorkspaceById(result.Id);
                Assert.Fail("Expected the workspace to be deleted.");
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Test]
        public async Task GetAggregatedWorkspaceTest()
        {
            var workspace = new Workspace("Test Workspace", TestUtils.GetTestProperty("modelId"), "Hello world!");

            Workspace aggregatedWorkspace = await service.CreateWorkspace(workspace);

            Component a = await client.ComponentService.CreateComponent(new Component("Test Component", aggregatedWorkspace.Id,
                "Test Component"));

            Component b = await client.ComponentService.CreateComponent(new Component("Test Component", aggregatedWorkspace.Id,
               "Test Component"));

            await client.ReferenceService.CreateReference(new Reference(aggregatedWorkspace.Id, "test reference",
                a.Id, b.Id, 0));

            await client.TagService.CreateTag(new Tag("Test Tag", aggregatedWorkspace.Id, "Test tag"));

            AggregatedWorkspace result = await service.GetAggregatedWorkspace(aggregatedWorkspace.Id);
            Assert.True(result.Components.Any());
            Assert.True(result.References.Any());
            Assert.True(result.Tags.Any());

            // rolling back
            await service.DeleteWorkspace(aggregatedWorkspace.Id);
        }

        [Test]
        public async Task GetWorkspaceTest()
        {
            Workspace workspaceTemplate = CreateWorkspaceTemplate();
            Workspace workspace = await service.CreateWorkspace(workspaceTemplate);
            Workspace result = await service.GetWorkspaceById(workspace.Id);
            Assert.True(workspace.Id == result.Id);

            // roll back
            await service.DeleteWorkspace(workspace.Id);
        }

        [Test]
        public async Task UpdateWorkspaceTest()
        {
            Workspace workspaceTemplate = CreateWorkspaceTemplate();
            Workspace result = await service.CreateWorkspace(workspaceTemplate);
            result.Name = "updatedName";
            Workspace updatedWorkspace = await service.UpdateWorkspace(result.Id, result);
            Assert.True("updatedName" == updatedWorkspace.Name);
            //roll back
            await service.DeleteWorkspace(result.Id);
        }

        [Test]
        public async Task SearchWorkspacesByNameTest()
        {
            Workspace result1 = null;
            Workspace result2 = null;
            Workspace result3 = null;

            try
            {
                Workspace workspaceTemplate1 = CreateWorkspaceTemplate();
                workspaceTemplate1.Name = "Test Workspace 1";
                result1 = await service.CreateWorkspace(workspaceTemplate1);

                Workspace workspaceTemplate2 = CreateWorkspaceTemplate();
                workspaceTemplate2.Name = "Test Workspace 2";
                result2 = await service.CreateWorkspace(workspaceTemplate2);

                Workspace workspaceTemplate3 = CreateWorkspaceTemplate();
                workspaceTemplate3.Name = "Workspace Test 3";
                result3 = await service.CreateWorkspace(workspaceTemplate3);

                var searchResult = await service.SearchWorkspacesByName("Test");

                Assert.AreEqual(2, searchResult.Count);
                Assert.That(
                    searchResult,
                    Has
                        .Exactly(1)
                        .Matches<Workspace>(workspace => workspace.Name == "Test Workspace 1"));
                Assert.That(
                    searchResult,
                    Has
                        .Exactly(1)
                        .Matches<Workspace>(workspace => workspace.Name == "Test Workspace 2"));
            }
            finally
            {
                //roll back

                if (result1 != null)
                    await service.DeleteWorkspace(result1.Id);

                if (result2 != null)
                    await service.DeleteWorkspace(result2.Id);

                if (result3 != null)
                    await service.DeleteWorkspace(result3.Id);
            }
        }
    }
}