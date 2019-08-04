using System;
using System.Threading;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net
{
    public interface IAkinatorClient : IDisposable
    {
        /// <summary>
        /// Starts a new Akinator game
        /// </summary>
        /// <returns>The first question</returns>
        Task<AkinatorQuestion> StartNewGame();

        /// <summary>
        /// Starts a new Akinator game
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The first question</returns>
        Task<AkinatorQuestion> StartNewGame(CancellationToken cancellationToken);

        /// <summary>
        /// Answers the question given previously by Akinator
        /// </summary>
        /// <param name="answer">The answer you want to give</param>
        /// <returns>The next question</returns>
        Task<AkinatorQuestion> Answer(AnswerOptions answer);

        /// <summary>
        /// Answers the question given previously by Akinator
        /// </summary>
        /// <param name="answer">The answer you want to give</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The next question</returns>
        Task<AkinatorQuestion> Answer(AnswerOptions answer, CancellationToken cancellationToken);

        /// <summary>
        /// Prompts Akinator to take a guess
        /// </summary>
        /// <returns>The most probable guesses Akinator can come up with</returns>
        Task<AkinatorGuess[]> GetGuess();

        /// <summary>
        /// Prompts Akinator to take a guess
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The most probable guesses Akinator can come up with</returns>
        Task<AkinatorGuess[]> GetGuess(CancellationToken cancellationToken);

        /// <summary>
        /// Indicates, if Akinator is ready to guess based on a few criterias such as
        /// already answered questions or the probability given by Akinator
        /// </summary>
        /// <param name="question">The last question asked by Akinator</param>
        /// <returns>Indicates whether Akinator is ready to guess or not</returns>
        bool GuessIsDue(AkinatorQuestion question);

        /// <summary>
        /// Gives back all the necessary informations that are required to continue
        /// the session in future time.
        /// </summary>
        AkinatorUserSession GetUserSession();
    }
}