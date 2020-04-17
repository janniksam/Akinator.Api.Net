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
        /// Undoes the previously given answer
        /// </summary>
        /// <returns>The previous question</returns>
        Task<AkinatorQuestion> UndoAnswer();

        /// <summary>
        /// Undoes the previously given answer
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The previous question</returns>
        Task<AkinatorQuestion> UndoAnswer(CancellationToken cancellationToken);

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
        /// Searches Akinator for a specific character (person, object, animal, movie)
        /// </summary>
        /// <param name="search">Searchterm to be searched for</param>
        /// <returns>The list of characters, Akinator comes up with</returns>
        Task<AkinatorGuess[]> SearchCharacter(string search);

        /// <summary>
        /// Searches Akinator for a specific character (person, object, animal, movie)
        /// </summary>
        /// <param name="search">Searchterm to be searched for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The list of characters, Akinator comes up with</returns>
        Task<AkinatorGuess[]> SearchCharacter(string search, CancellationToken cancellationToken);

        /// <summary>
        /// Get the hall of fame
        /// </summary>
        Task<AkinatorHallOfFameEntries[]> GetHallOfFame();

        /// <summary>
        /// Get the hall of fame
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        Task<AkinatorHallOfFameEntries[]> GetHallOfFame(CancellationToken cancellationToken);

        /// <summary>
        /// Indicates, if Akinator is ready to guess based on a few criterias such as
        /// already answered questions or the probability given by Akinator
        /// </summary>
        /// <param name="platform">specify the logic varsion of a specific platform</param>
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