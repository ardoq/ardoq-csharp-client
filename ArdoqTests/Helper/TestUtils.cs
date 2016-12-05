using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Ardoq;
using System.Reflection;

namespace ArdoqTest.Helper
{
    public class TestUtils
    {
        private static Dictionary<string, string> p;

        static TestUtils()
        {
            InitProperties();
        }

        public static IArdoqClient GetClient()
        {
            return new ArdoqClient(new HttpClient(new LoggingHandler(new HttpClientHandler())),
                "https://test.ardoq.com", "", "jmeter");
        }

        private static void InitProperties()
        {
            if (null != p) return;
            p = new Dictionary<string, string>
            {
                {"organization", "jmeter"},
                {"modelId", "555dc8b1e4b098e2e8379add"},
                {"filename", "ardoq_hero.png"}
            };
        }

        public static String GetTestPropery(String name)
        {
            return p[name];
        }

        public static string LoadJsonFile(string name)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\TestData\json\" + name;
            return File.ReadAllText(path);
        }
    }
}