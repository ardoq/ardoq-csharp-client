using System.Threading.Tasks;
using Ardoq;
using Ardoq.Models;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;
using Refit;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class ComponentServiceTests
    {
        private IComponentService _service;
        private IArdoqClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _client = TestUtils.GetClient();
            _service = _client.ComponentService;
        }

        private async Task<Workspace> CreateWorkspace()
        {
            var workspace =
                await
                    _client.WorkspaceService.CreateWorkspace(
                        new Workspace("My Component Test Workspace",
                        TestUtils.GetTestProperty("modelId"), "Hello world!"));
            return workspace;
        }

        private Component CreateComponentTemplate(Workspace workspace)
        {
            return new Component("My Component Test", workspace.Id, "myDescription");
        }

        private async Task DeleteWorkspace(Workspace workspace)
        {
            await _client.WorkspaceService.DeleteWorkspace(workspace.Id);
        }

        [Test]
        public async Task CreateComponentTest()
        {
            var workspace = await CreateWorkspace();
            var componentTemplate = CreateComponentTemplate(workspace);
            var component = await _service.CreateComponent(componentTemplate);
            Assert.NotNull(component.Id);
            await _service.DeleteComponent(component.Id);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async Task DeleteComponentTest()
        {
            var workspace = await CreateWorkspace();
            var componentTemplate = CreateComponentTemplate(workspace);
            var result = await _service.CreateComponent(componentTemplate);
            Assert.NotNull(result.Id);
            await _service.DeleteComponent(result.Id);
            try
            {
                await _service.GetComponentById(result.Id);
                await DeleteWorkspace(workspace);
                Assert.Fail("Expected the Component to be deleted.");
            }
            catch (ApiException e)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, e.StatusCode);
            }
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async Task GetComponentTest()
        {
            var workspace = await CreateWorkspace();
            var componentTemplate = CreateComponentTemplate(workspace);
            var component = await _service.CreateComponent(componentTemplate);
            var allComponents = await _service.GetAllComponents();
            var id = allComponents[0].Id;
            var expectedComponent = await _service.GetComponentById(id);
            Assert.True(id == expectedComponent.Id);
            await _service.DeleteComponent(component.Id);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async Task UpdateComponentTest()
        {
            var workspace = await CreateWorkspace();
            var componentTemplate = CreateComponentTemplate(workspace);
            var component = await _service.CreateComponent(componentTemplate);
            component.Description = "Updated description";
            var updatedComponent = await _service.UpdateComponent(component.Id, component);
            Assert.True("Updated description" == updatedComponent.Description);
            Assert.True(component.VersionCounter == updatedComponent.VersionCounter - 1);
            await _service.DeleteComponent(component.Id);
            await DeleteWorkspace(workspace);
        }
    }
}