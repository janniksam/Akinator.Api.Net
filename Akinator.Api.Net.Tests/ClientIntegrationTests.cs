using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Akinator.Api.Net.Tests
{
    [TestClass]
    public class ClientIntegrationTests
    {
        private static IAkinatorServerLocator m_serverLocator;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            m_serverLocator = new AkinatorServerLocator();
        }

        [TestMethod]
        public async Task SimpleWorkflowTest_AllLanguages()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var allLanguages = Enum.GetValues(typeof(Language)).Cast<Language>();
            foreach (var language in allLanguages)
            {
                var elapsedLanguage = stopwatch.ElapsedMilliseconds;
                var servers = await m_serverLocator.SearchAllAsync(language).ConfigureAwait(false);
                foreach (var server in servers)
                {
                    try
                    {
                        Console.WriteLine($"Testing - Language {language}, Type {server.ServerType} - STARTED");
                        using IAkinatorClient client = new AkinatorClient(server);
                        await client.StartNewGame();
                        while (true)
                        {
                            var _ = await client.Answer(AnswerOptions.Yes).ConfigureAwait(false);
                            if (!client.GuessIsDue())
                            {
                                continue;
                            }

                            var guess = await client.GetGuess().ConfigureAwait(false);
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
                    catch (Exception e)
                    {
                        Assert.Fail(e.ToString());
                    }
                    finally
                    {
                        Console.WriteLine($"Testing - Language {language}, Type {server.ServerType} - DONE");
                    }
                }

                elapsedLanguage = stopwatch.ElapsedMilliseconds - elapsedLanguage;
                Console.WriteLine($"Took: {elapsedLanguage}ms");
            }

            Console.WriteLine($"Test done. Took {stopwatch.Elapsed.TotalSeconds}s.");
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task StartNewGameThrowsExceptionOnCancelled()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            using (IAkinatorClient client = new AkinatorClient(server))
            {
                var src = new CancellationTokenSource();
                var cancellationToken = src.Token;
                src.Cancel();

                await client.StartNewGame(cancellationToken);
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task AnswerThrowsExceptionOnCancelled()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            using (IAkinatorClient client = new AkinatorClient(server))
            {
                var src = new CancellationTokenSource();
                var cancellationToken = src.Token;
                src.Cancel();

                await client.Answer(AnswerOptions.Yes, cancellationToken);
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task UndoAnswerThrowsExceptionOnCancelled()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            using (IAkinatorClient client = new AkinatorClient(server))
            {
                var src = new CancellationTokenSource();
                var cancellationToken = src.Token;
                src.Cancel();

                await client.UndoAnswer(cancellationToken);
            }

            Assert.Fail("No exception was thrown");
        }


        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task GetGuessThrowsExceptionOnCancelled()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            using (IAkinatorClient client = new AkinatorClient(server))
            {
                var src = new CancellationTokenSource();
                var cancellationToken = src.Token;
                src.Cancel();

                await client.GetGuess(cancellationToken);
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task GetHallOfFameThrowsExceptionOnCancelled()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            using (IAkinatorClient client = new AkinatorClient(server))
            {
                var src = new CancellationTokenSource();
                var cancellationToken = src.Token;
                src.Cancel();

                await client.GetHallOfFame(cancellationToken);
            }

            Assert.Fail("No exception was thrown");
        }


        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task SearchCharacterThrowsExceptionOnCancelled()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            using (IAkinatorClient client = new AkinatorClient(server))
            {
                var src = new CancellationTokenSource();
                var cancellationToken = src.Token;
                src.Cancel();

                await client.SearchCharacter("Brat Pitt", cancellationToken);
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        public async Task ReuseSessionWorks()
        {
            AkinatorUserSession userSessionFromFirstClient;

            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            using (IAkinatorClient client = new AkinatorClient(server))
            {
                var question = await client.StartNewGame();
                Assert.AreEqual(0, question.Step);

                question = await client.Answer(AnswerOptions.Yes);
                Assert.AreEqual(1, question.Step);

                userSessionFromFirstClient = client.GetUserSession();
            }

            using (IAkinatorClient newClient = new AkinatorClient(server, userSessionFromFirstClient))
            {
                var question = await newClient.Answer(AnswerOptions.Yes);
                Assert.AreEqual(2, question.Step);
            }
        }


        [TestMethod]
        public async Task UndoWorksAsExpected()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            
            using IAkinatorClient client = new AkinatorClient(server);
            
            var questionStart = await client.StartNewGame();
            Assert.AreEqual(0, questionStart.Step);

            var question1 = await client.Answer(AnswerOptions.Yes);
            Assert.AreEqual(1, question1.Step);

            var question2 = await client.Answer(AnswerOptions.Yes);
            Assert.AreEqual(2, question2.Step);

            var questionPrevious = await client.UndoAnswer();
            Assert.AreEqual(1, questionPrevious.Step);
            Assert.AreEqual(question1.Text, questionPrevious.Text);
        }
        
        [TestMethod]
        public async Task ChildModeDoesNotThrowAnyExceptions()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            
            using IAkinatorClient client = new AkinatorClient(server, childMode: true);
            var questionStart = await client.StartNewGame(); 
            var nextQuestion = await client.Answer(AnswerOptions.Yes);
            Assert.AreEqual(0, questionStart.Step);
            Assert.AreEqual(1, nextQuestion.Step);
        }

        [TestMethod]
        public async Task CurrentQuestionReturnsCurrentQuestion()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            
            using IAkinatorClient client = new AkinatorClient(server);
            
            var questionStart = await client.StartNewGame();
            Assert.AreEqual(questionStart.Text, client.CurrentQuestion.Text);

            questionStart = await client.Answer(AnswerOptions.Yes);
            Assert.AreEqual(questionStart.Text, client.CurrentQuestion.Text);

            questionStart = await client.Answer(AnswerOptions.Yes);
            Assert.AreEqual(questionStart.Text, client.CurrentQuestion.Text);

            questionStart = await client.UndoAnswer();
            Assert.AreEqual(questionStart.Text, client.CurrentQuestion.Text);
        }

        [TestMethod]
        public async Task SearchCharacter_ReturnsValidCharactersForASearchTerm()
        {
            var server = await m_serverLocator.SearchAsync(Language.English, ServerType.Person).ConfigureAwait(false);
            using IAkinatorClient client = new AkinatorClient(server);
            await client.StartNewGame();
            var chars = await client.SearchCharacter("Brat Pitt");
            Assert.IsTrue(chars.Any(p => p.Name.Contains("brat pitt", StringComparison.InvariantCultureIgnoreCase)));
        }

        [TestMethod]
        public async Task HallOfFameGivesValidResponseForEveryLanguage()
        {
            var allLanguages = Enum.GetValues(typeof(Language)).Cast<Language>();
            foreach (var language in allLanguages)
            {
                var server = await m_serverLocator.SearchAsync(language, ServerType.Person).ConfigureAwait(false);
                using IAkinatorClient client = new AkinatorClient(server);
                var result = await client.GetHallOfFame();
                Assert.IsNotNull(result);
                Assert.AreNotEqual(0, result.Length);
            }
        }
    }
}
