using System;
using System.Linq;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
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
        public async Task SearchManyAsync_ReturnsAtleastOneServerPerLanguage()
        {
            var allLanguages = Enum.GetValues(typeof(Language)).Cast<Language>();
            foreach (var language in allLanguages)
            {
                var foundServer = await m_serverLocator.SearchAllAsync(language);
                Assert.IsNotNull(foundServer);
            }
        }
    }
}
