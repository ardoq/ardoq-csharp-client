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
        private IModelService service;
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
            Model modelById = await service.GetModelById(modelId, client.Org);
            Model modelByName = await service.GetModelByName(modelById.Name, client.Org);
            Assert.True(modelById.Id == modelByName.Id);
        }
    }
}