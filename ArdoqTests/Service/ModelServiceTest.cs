using System;
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
    }
}