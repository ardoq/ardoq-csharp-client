using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Ardoq;
using System.Reflection;

namespace ArdoqTest.Helper
{
    public static class TestUtils
    {
        private static Dictionary<string, string> _p;

        static TestUtils()
        {
            InitProperties();
        }

        public static IArdoqClient GetClient()
        {
            return new ArdoqClient(new HttpClient(new LoggingHandler(new HttpClientHandler())), "", "", "");
        }

        private static void InitProperties()
        {
            if (null != _p) return;
            _p = new Dictionary<string, string>
            {
                {"organization", "jmeter"},
                {"modelId", "555dc8b1e4b098e2e8379add"},
                {"filename", "ardoq_hero.png"}
            };
        }

        public static string GetTestProperty(string name)
        {
            return _p[name];
        }

        public static string LoadJsonFile(string name)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/TestData/json/" + name;
            return File.ReadAllText(path);
        }
    }
}