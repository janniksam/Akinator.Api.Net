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
        [TestMethod]
        public async Task SearchAsync_Returns_AtleastOneServerPerLanguage()
        {
            IAkinatorServerLocator akinatorServerLocator = new AkinatorServerLocator();

            var allLanguages = Enum.GetValues(typeof(Language)).Cast<Language>();
            foreach (var language in allLanguages)
            {
                var foundServer = await akinatorServerLocator.SearchAsync(language, ServerType.Person);
                Assert.IsNotNull(foundServer);
            }
        }

        [TestMethod]
        public async Task SearchManyAsync_ReturnsAtleastOneServerPerLanguage()
        {
            IAkinatorServerLocator akinatorServerLocator = new AkinatorServerLocator();

            var allLanguages = Enum.GetValues(typeof(Language)).Cast<Language>();
            foreach (var language in allLanguages)
            {
                var foundServer = await akinatorServerLocator.SearchAllAsync(language);
                Assert.IsNotNull(foundServer);
            }
        }
    }
}
