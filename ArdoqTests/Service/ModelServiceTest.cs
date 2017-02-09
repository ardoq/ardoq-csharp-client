using System;
using Ardoq;
using Ardoq.Models;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class ModelServiceTest
    {
        private IDeprecatedModelService service;
        private String modelId;
        private IArdoqClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            client = TestUtils.GetClient();
            service = client.ModelService;
            modelId = TestUtils.GetTestPropery("modelId");
        }

        [Test]
        public async Task GetModelByNameTest()
        {
            Model modelById = await service.GetModelById(modelId);
            Model modelByName = await service.GetTemplateByName(modelById.Name);
            Assert.True(modelById.Id == modelByName.Id);
        }

        [Test]
        public async Task GetTemplateByNameTest()
        {
            Model modelByName = await service.GetTemplateByName("Application Service");
            Assert.True(modelByName.Name == "Application Service");
        }

        [Test]
        public async Task GetAllTemplatesTest()
        {
            List<Model> allTemplates = await service.GetAllTemplates();
            List<Model> allModels = await service.GetAllModels();
            allTemplates.TrueForAll(m => m.UseAsTemplate == true);
            Assert.True(allModels.Count > allTemplates.Count);
        }
    }
}