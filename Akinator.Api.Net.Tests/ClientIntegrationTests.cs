using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Akinator.Api.Net.Tests
{
    [TestClass]
    public class ClientIntegrationTests
    {
        [TestMethod]
        public async Task SimpleWorkflowTest_Arabic_Person()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.Arabic, ServerType.Person))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_English_Animal()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.English, ServerType.Animal))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_English_Object()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.English, ServerType.Animal))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_English_Person()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.English, ServerType.Person))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_German_Person()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.German, ServerType.Person))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_Italian_Animal()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.Italian, ServerType.Animal))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_Italian_Person()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.Italian, ServerType.Person))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_Russian_Person()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.Russian, ServerType.Person))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_Spanish_Animal()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.Spanish, ServerType.Animal))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_Spanish_Person()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.Spanish, ServerType.Person))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task StartNewGameThrowsExceptionOnCancelled()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.English, ServerType.Person))
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
            using (IAkinatorClient client = new AkinatorClient(Language.English, ServerType.Person))
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
        public async Task GetGuessThrowsExceptionOnCancelled()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.English, ServerType.Person))
            {
                var src = new CancellationTokenSource();
                var cancellationToken = src.Token;
                src.Cancel();

                await client.GetGuess(cancellationToken);
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        public async Task ReuseSessionWorks()
        {
            AkinatorUserSession userSessionFromFirstClient;

            using (IAkinatorClient client = new AkinatorClient(Language.English, ServerType.Person))
            {
                var question = await client.StartNewGame();
                Assert.AreEqual(0, question.Step);

                question = await client.Answer(AnswerOptions.Yes);
                Assert.AreEqual(1, question.Step);

                userSessionFromFirstClient = client.GetUserSession();
            }

            using (IAkinatorClient newClient = new AkinatorClient(Language.English, ServerType.Person, userSessionFromFirstClient))
            {
                var question = await newClient.Answer(AnswerOptions.Yes);
                Assert.AreEqual(2, question.Step);
            }
        }
      
      [TestMethod]
        public async Task SimpleWorkflowTest_France_Animal()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.France, ServerType.Animal))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_France_Object()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.France, ServerType.Animal))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
        public async Task SimpleWorkflowTest_France_Person()
        {
            using (IAkinatorClient client = new AkinatorClient(Language.France, ServerType.Person))
            {
                var question = await client.StartNewGame();
                while (true)
                {
                    var nextQuestion = await client.Answer(AnswerOptions.Yes);
                    if (!client.GuessIsDue(nextQuestion))
                    {
                        continue;
                    }

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
    }
}
