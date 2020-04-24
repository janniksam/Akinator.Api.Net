using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
using Akinator.Api.Net.Model.External;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Akinator.Api.Net
{
    public interface IAkinatorClient : IDisposable
    {
        /// <summary>
        /// Starts a new Akinator game
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The first question</returns>
        Task<AkinatorQuestion> StartNewGame(CancellationToken cancellationToken = default);

        /// <summary>
        /// Undoes the previously given answer
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The previous question</returns>
        Task<AkinatorQuestion> UndoAnswer(CancellationToken cancellationToken = default);

        /// <summary>
        /// Answers the question given previously by Akinator
        /// </summary>
        /// <param name="answer">The answer you want to give</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The next question</returns>
        Task<AkinatorQuestion> Answer(AnswerOptions answer, CancellationToken cancellationToken = default);

        /// <summary>
        /// Prompts Akinator to take a guess
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The most probable guesses Akinator can come up with</returns>
        Task<AkinatorGuess[]> GetGuess(CancellationToken cancellationToken = default);

        /// <summary>
        /// Searches Akinator for a specific character (person, object, animal, movie)
        /// </summary>
        /// <param name="search">Searchterm to be searched for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of characters, Akinator comes up with</returns>
        Task<AkinatorGuess[]> SearchCharacter(string search, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the hall of fame
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        Task<AkinatorHallOfFameEntries[]> GetHallOfFame(CancellationToken cancellationToken = default);

        /// <summary>
        /// Indicates, if Akinator is ready to guess based on a few criterias such as
        /// already answered questions or the probability given by Akinator
        /// </summary>
        /// <param name="platform">Specify the logic to determine, if Akinator is ready to guess</param>
        /// <returns>Indicates whether Akinator is ready to guess or not</returns>
        bool GuessIsDue(Platform platform = Platform.Android);

        /// <summary>
        /// Gives back all the necessary informations that are required to continue
        /// the session in future time.
        /// </summary>
        AkinatorUserSession GetUserSession();

        /// <summary>
        /// Gets the current question of your session
        /// </summary>
        AkinatorQuestion CurrentQuestion { get; }
    }
}