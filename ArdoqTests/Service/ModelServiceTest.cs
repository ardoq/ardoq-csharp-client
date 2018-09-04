using System.Linq;
using Ardoq;
using Ardoq.Service.Interface;
using ArdoqTest.Helper;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ArdoqTest.Service
{
    [TestFixture]
    public class ModelServiceTest
    {
        private IDeprecatedModelService _service;
        private string _modelId;
        private IArdoqClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _client = TestUtils.GetClient();
            _service = _client.ModelService;
            _modelId = TestUtils.GetTestProperty("modelId");
        }

        [Test]
        public async Task GetTemplatesByNameTest()
        {
            var modelById = await _service.GetModelById(_modelId);
            var modelsByName = await _service.GetModelsByName(modelById.Name);
            Assert.Contains(modelById.Id, modelsByName.Select(model => model.Id).ToList());
        }

        [Test]
        public async Task GetTemplateByNameTest()
        {
            var modelByName = await _service.GetTemplateByName("Application Service");
            Assert.AreEqual(modelByName.Name, "Application Service");
        }

        [Test]
        public async Task GetAllTemplatesTest()
        {
            var allTemplates = await _service.GetAllTemplates();
            var allModels = await _service.GetAllModels();
            Assert.True(allTemplates.TrueForAll(m => m.UseAsTemplate == true));
            Assert.Greater(allModels.Count, allTemplates.Count);
        }
    }
}