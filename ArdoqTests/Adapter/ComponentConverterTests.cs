using System;
using System.Collections.Generic;
using Ardoq.Models;
using ArdoqTest.Helper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ArdoqTest.Adapter
{
    [TestFixture]
    public class ComponentConverterTests
    {
        [Test]
        public void DynamicFieldsDeSerializationTest()
        {
            var component = JsonConvert.DeserializeObject<Component>(TestUtils.LoadJsonFile("component.json"));

            foreach (string s in new[] {"endpoint", "maintainer", "environment", "wsdl"})
            {
                Assert.True(component.Fields.ContainsKey(s));
            }
            Assert.True(component.Fields["maintainer"].ToString() == "erik@ardoq.com");
        }

        [Test]
        public void DynamicFieldsSerializationTest()
        {
            var component = new Component("name", "rootWorkspace", "description", "typeId")
            {
                Fields = new Dictionary<String, Object>
                {
                    {"maintainer", "erik@ardoq.com"},
                    {"endpoint", "http://www.vg.no"}
                },
                Children = new List<string> {"test", "test 2", "test 3"}
            };


            string s = JsonConvert.SerializeObject(component);
            var result = JsonConvert.DeserializeObject<Component>(s);
            Assert.True(result.Fields["maintainer"].ToString() == "erik@ardoq.com");
            Assert.True(result.Fields["endpoint"].ToString() == "http://www.vg.no");
            Assert.True(result.Children.Count == 3);
        }
    }
}