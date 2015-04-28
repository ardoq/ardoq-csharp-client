using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Ardoq;
using Ardoq.Models;
using Ardoq.Service;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class ComponentServiceTests
    {
        private IComponentService service;
        private IArdoqClient client;

        [TestFixtureSetUp]
        public void Setup()
        {
            client = TestUtils.GetClient();
            service = client.ComponentService;
        }

        private async Task<Workspace> CreateWorkspace()
        {
            Workspace workspace =
                await
                    client.WorkspaceService.CreateWorkspace(
                        new Workspace("My Component Test Workspace",
                        TestUtils.GetTestPropery("modelId"), "Hello world!"), client.Org);
            return workspace;
        }

        private Component CreateComponentTemplate(Workspace workspace)
        {
            return new Component("My Component Test", workspace.Id, "myDescription");
        }

        private async Task DeleteWorkspace(Workspace workspace)
        {
            await client.WorkspaceService.DeleteWorkspace(workspace.Id, client.Org);
        }

        [Test]
        public async void CreateComponentTest()
        {
            Workspace workspace = await CreateWorkspace();
            Component componentTemplate = CreateComponentTemplate(workspace);
            Component component = await service.CreateComponent(componentTemplate, client.Org);
            Assert.NotNull(component.Id);
            await service.DeleteComponent(component.Id, client.Org);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async void DeleteComponentTest()
        {
            Workspace workspace = await CreateWorkspace();
            Component componentTemplate = CreateComponentTemplate(workspace);
            Component result = await service.CreateComponent(componentTemplate, client.Org);
            await service.DeleteComponent(result.Id, client.Org);
            try
            {
                await service.GetComponentById(result.Id, client.Org);
                await DeleteWorkspace(workspace);
                Assert.Fail("Expected the Component to be deleted.");
            }
            catch (HttpRequestException)
            {
            }
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async void GetComponentTest()
        {
            Workspace workspace = await CreateWorkspace();
            Component componentTemplate = CreateComponentTemplate(workspace);
            Component component = await service.CreateComponent(componentTemplate, client.Org);
            List<Component> allComponents = await service.GetAllComponents(client.Org);
            string id = allComponents[0].Id;
            Component expectedComponent = await service.GetComponentById(id, client.Org);
            Assert.True(id == expectedComponent.Id);
            await service.DeleteComponent(component.Id, client.Org);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async void UpdateComponentTest()
        {
            Workspace workspace = await CreateWorkspace();
            Component componentTemplate = CreateComponentTemplate(workspace);
            Component component = await service.CreateComponent(componentTemplate, client.Org);
            component.Description = "Updated description";
            Component updatedComponent = await service.UpdateComponent(component.Id, component, client.Org);
            Assert.True("Updated description" == updatedComponent.Description);
            Assert.True(component.VersionCounter == updatedComponent.VersionCounter - 1);
            await service.DeleteComponent(component.Id, client.Org);
            await DeleteWorkspace(workspace);
        }
    }
}