using System.Linq;
using Ardoq.Models;
using ArdoqTest.Helper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ArdoqTest.Adapter
{
    [TestFixture]
    public class ModelConverterTests
    {
        [Test]
        public void DeserializeTest()
        {
            var jsonObject = TestUtils.LoadJsonFile("model.json");
            var result = JsonConvert.DeserializeObject<Model>(jsonObject);
            Assert.True(result.ReferenceTypes.Count == 5);
            Assert.True(2 == result.GetReferenceTypeByName("Implicit"));
            Assert.True(result.ComponentTypes.Any());
        }
    }
}