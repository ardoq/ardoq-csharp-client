using System;
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
    public class FieldServiceTests
    {
        private IFieldService service;
        private IArdoqClient client;
        private String modelId;

        [TestFixtureSetUp]
        public void Setup()
        {
            client = TestUtils.GetClient();
            modelId = TestUtils.GetTestPropery("modelId");
            service = client.FieldService;
        }

        private async Task<Workspace> CreateWorkspace()
        {
            return
                await
                    client.WorkspaceService.CreateWorkspace(
                        new Workspace("Field Test Workspace", modelId, "Hello world!"), client.Org);
        }

        private async Task<Component> CreateComponent(Workspace workspace)
        {
            return
                await client.ComponentService.CreateComponent(
                    new Component("Field Test Component", workspace.Id, ""), client.Org);
        }

        private Field CreateFieldTemplate(Component component)
        {
            var componentTypes = new List<String> {component.Type};
            return new Field("maintainer", "maintainer", modelId, componentTypes, FieldType.Email);
        }

        private async Task DeleteWorkspace(Workspace workspace)
        {
            await client.WorkspaceService.DeleteWorkspace(workspace.Id, client.Org);
        }

        [Test]
        public async void CreateFieldTest()
        {
            Workspace workspace = await CreateWorkspace();
            Component component = await CreateComponent(workspace);
            Field fieldTemplate = CreateFieldTemplate(component);
            Field result = await service.CreateField(fieldTemplate, client.Org);
            Assert.NotNull(result.Id);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async void DeleteFieldTest()
        {
            Workspace workspace = await CreateWorkspace();
            Component component = await CreateComponent(workspace);
            Field fieldTemplate = CreateFieldTemplate(component);

            Field result = await service.CreateField(fieldTemplate, client.Org);
            await service.DeleteField(result.Id, client.Org);
            try
            {
                await service.GetFieldById(result.Id, client.Org);
                await DeleteWorkspace(workspace);
                Assert.Fail("Expected the Field to be deleted.");
            }
            catch (HttpRequestException)
            {
            }
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async void GetFieldTest()
        {
            Workspace workspace = await CreateWorkspace();
            Component component = await CreateComponent(workspace);
            Field fieldTemplate = CreateFieldTemplate(component);

            Field result = await service.CreateField(fieldTemplate, client.Org);
            Field field = await service.GetFieldById(result.Id, client.Org);
            Assert.True(result.Id == field.Id);
            await DeleteWorkspace(workspace);
        }

        [Test]
        public async void UpdateFieldTest()
        {
            Workspace workspace = await CreateWorkspace();
            Component component = await CreateComponent(workspace);
            Field fieldTemplate = CreateFieldTemplate(component);

            Field result = await service.CreateField(fieldTemplate, client.Org);
            result.Name = "updatedName";
            Field updatedField = await service.UpdateField(result.Id, result, client.Org);
            Assert.True("updatedName" == updatedField.Name);
            await DeleteWorkspace(workspace);
        }
    }
}