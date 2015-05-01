using System;
using Ardoq;
using Ardoq.Models;
using Ardoq.Service;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class ModelServiceTest
    {
        private IDeprecatedModelService service;
        private String modelId;
        private IArdoqClient client;

        [TestFixtureSetUp]
        public void Setup()
        {
            client = TestUtils.GetClient();
            service = client.ModelService;
            modelId = TestUtils.GetTestPropery("modelId");
        }

        [Test]
        public async void GetModelByNameTest()
        {
            Model modelById = await service.GetModelById(modelId);
            Model modelByName = await service.GetModelByName(modelById.Name);
            Assert.True(modelById.Id == modelByName.Id);
        }
    }
}