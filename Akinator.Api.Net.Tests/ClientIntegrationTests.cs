using System;
using System.Linq;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
using Akinator.Api.Net.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akinator.Api.Net.Tests
{
    [TestClass]
    public class ClientIntegrationTests
    {
        [TestMethod]
        public async Task SimpleWorkflowTest()
        {
            var client = new AkinatorClient(Language.English, ServerType.Person);
            var question = await client.StartNewGame();
            while (true)
            {
                var nextQuestion = await client.Answer(AnswerOptions.Yes);
                if (client.GuessIsDue(nextQuestion))
                {
                    var guess = await client.GetGuess();
                    if (!guess.Any())
                    {
                        Assert.Fail("No guess was found");
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public async Task ReuseSessionWorks()
        {
            AkinatorUserSession userSessionFromFirstClient;

            using (var client = new AkinatorClient(Language.English, ServerType.Person))
            {
                var question = await client.StartNewGame();
                Assert.AreEqual(0, question.Step);

                question = await client.Answer(AnswerOptions.Yes);
                Assert.AreEqual(1, question.Step);

                userSessionFromFirstClient = client.GetUserSession();
            }

            using (var newClient = new AkinatorClient(Language.English, ServerType.Person, userSessionFromFirstClient))
            {
                var question = await newClient.Answer(AnswerOptions.Yes);
                Assert.AreEqual(2, question.Step);
            }
        }
    }
}
