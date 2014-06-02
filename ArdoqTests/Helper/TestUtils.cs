using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Ardoq;

namespace ArdoqTest.Helper
{
    public class TestUtils
    {
        private static Dictionary<string, string> p;

        static TestUtils()
        {
            InitProperties();
        }

        public static ArdoqClient GetClient
        {
            get
            {
                return new ArdoqClient(new HttpClient(new LoggingHandler(new HttpClientHandler())),
                    "http://10.20.32.188:3000", "3e1a3581ff7b43d685a0042d34411272");
            }
        }

        private static void InitProperties()
        {
            if (null != p) return;
            p = new Dictionary<string, string>
            {
                {"organization", "ardoq"},
                {"modelId", "53887e79e4b07e9046b2514e"},
                {"filename", "ardoq_hero.png"}
            };
        }

        public static String GetTestPropery(String name)
        {
            return p[name];
        }

        public static string LoadJsonFile(string name)
        {
            string path = Directory.GetCurrentDirectory() + @"\TestData\json\" + name;
            return File.ReadAllText(path);
        }
    }
}