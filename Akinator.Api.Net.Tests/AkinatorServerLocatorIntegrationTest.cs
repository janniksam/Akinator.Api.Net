using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akinator.Api.Net.Tests
{
    [TestClass]

    public class AkinatorServerLocatorIntegrationTest
    {
        private static IAkinatorServerLocator m_serverLocator;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            m_serverLocator = new AkinatorServerLocator();
        }

        [TestMethod]
        public async Task SearchAsync_Returns_AtleastOneServerPerLanguage()
        {
            var allLanguages = Enum.GetValues(typeof(Language)).Cast<Language>();
            foreach (var language in allLanguages)
            {
                var foundServer = await m_serverLocator.SearchAsync(language, ServerType.Person);
                Assert.IsNotNull(foundServer, $"No server found for language {language}");
            }
        }

        [TestMethod]
        public async Task SearchAllAsync_ReturnsAtleastOneServerPerLanguage()
        {
            var allLanguages = Enum.GetValues(typeof(Language)).Cast<Language>();
            foreach (var language in allLanguages)
            {
                var foundServer = await m_serverLocator.SearchAllAsync(language);
                Assert.IsNotNull(foundServer);
            }
        }

        [TestMethod]
        public async Task SearchAllAsync_UsesCacheOnSecondCall()
        {
            m_serverLocator = new AkinatorServerLocator();
            const Language language = Language.English;

            var sw = new Stopwatch();
            sw.Start();
            var foundServer = (await m_serverLocator.SearchAllAsync(language)).ToArray();
            sw.Stop();
            Assert.IsTrue(sw.ElapsedMilliseconds > 20);

            sw.Restart();
            var foundServer2 = (await m_serverLocator.SearchAllAsync(language)).ToArray();
            sw.Stop();
            Assert.IsTrue(sw.ElapsedMilliseconds < 20);


            Assert.IsNotNull(foundServer);
            Assert.AreNotEqual(0, foundServer.Length);
            Assert.IsNotNull(foundServer2);
            Assert.AreNotEqual(0, foundServer2.Length);
            for (var i = 0; i < foundServer.Length; i++)
            {
                Assert.AreEqual(foundServer[i].ServerUrl, foundServer2[i].ServerUrl);
            }
        }
    }
}
