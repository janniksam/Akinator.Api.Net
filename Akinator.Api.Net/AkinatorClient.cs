using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
using Akinator.Api.Net.Model.External;
using Akinator.Api.Net.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Akinator.Api.Net
{
    public class AkinatorClient : IAkinatorClient
    {
        private readonly Regex m_regexSession = new Regex("var uid_ext_session = '(.*)'\\;\\n.*var frontaddr = '(.*)'\\;");
        private readonly Regex m_regexStartGameResult = new Regex(@"^jQuery3410014644797238627216_\d+\((.+)\)$");
        private readonly HttpClient m_webClient;
        private readonly Language m_usedLanguage;
        private readonly ServerType m_usedServerType;
        private readonly bool m_childMode;
        private string m_session;
        private string m_signature;
        private int m_step;
        private int m_lastGuessStep;

        public AkinatorClient(Language language, ServerType serverType, AkinatorUserSession existingSession = null, bool childMode = false)
        {
            m_webClient = new HttpClient(new HttpClientHandler { UseCookies = false });
            m_webClient.DefaultRequestHeaders.Add("Accept", "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01");
            m_webClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,ar;q=0.8");
            m_webClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            m_webClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            m_webClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            m_webClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            m_webClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            m_webClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.92 Safari/537.36");
            m_webClient.DefaultRequestHeaders.Add("Referer", "https://en.akinator.com/game");
            m_usedLanguage = language;
            m_usedServerType = serverType;
            m_childMode = childMode;
            Attach(existingSession);
        }

        public async Task<AkinatorQuestion> StartNewGame(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var apiKey = await GetSession().ConfigureAwait(false);
            var url = AkiUrlBuilder.NewGame(apiKey, m_usedLanguage, m_usedServerType, m_childMode);

            var response = await m_webClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();

            var match = m_regexStartGameResult.Match(content);
            if (!match.Success && match.Groups.Count != 2)
            {
                throw new InvalidCastException($"Invalid result received from Akinator. Result was {response}");
            }

            var result = JsonConvert.DeserializeObject<BaseResponse<NewGameParameters>>(match.Groups[1].Value,
                new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

            m_session = result.Parameters.Identification.Session;
            m_signature = result.Parameters.Identification.Signature;
            m_step = result.Parameters.StepInformation.Step;
            CurrentQuestion = ToAkinatorQuestion(result.Parameters.StepInformation);
            return ToAkinatorQuestion(result.Parameters.StepInformation);
        }

        public async Task<AkinatorQuestion> Answer(AnswerOptions answer, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var url = AkiUrlBuilder.Answer(BuildAnswerRequest(answer), m_usedLanguage, m_usedServerType);

            var response = await m_webClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<BaseResponse<Question>>(content,
                new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

            m_step = result.Parameters.Step;
            CurrentQuestion = ToAkinatorQuestion(result.Parameters);
            return ToAkinatorQuestion(result.Parameters);
        }

        public async Task<AkinatorQuestion> UndoAnswer(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (m_step == 0)
            {
                return null;
            }

            var url = AkiUrlBuilder.UndoAnswer(m_session, m_signature, m_step, m_usedLanguage, m_usedServerType);

            var response = await m_webClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<BaseResponse<Question>>(content,
                new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

            m_step = result.Parameters.Step;
            CurrentQuestion = ToAkinatorQuestion(result.Parameters);
            return ToAkinatorQuestion(result.Parameters);
        }

        public async Task<AkinatorGuess[]> SearchCharacter(string search, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var url = AkiUrlBuilder.SearchCharacter(search, m_session, m_signature, m_step, m_usedLanguage, m_usedServerType);

            var response = await m_webClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<BaseResponse<Characters>>(content,
                new JsonSerializerSettings()
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

            return result.Parameters.AllCharacters.Select(p =>
                new AkinatorGuess(p.Name, p.Description)
                {
                    ID = p.IdBase,
                    PhotoPath = p.PhotoPath,
                }).ToArray();
        }

        public AkinatorQuestion CurrentQuestion { get; private set; }

        public async Task<AkinatorHallOfFameEntries[]> GetHallOfFame(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var hallOfFameRequestUrl = AkiUrlBuilder.MapHallOfFame(m_usedLanguage);
            var response = await m_webClient.GetAsync(hallOfFameRequestUrl, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();
            var data = XmlConverter.ToClass<HallOfFame>(content);
            return ToHallOfFameEntry(data.Awards.Award);
        }

        public async Task<AkinatorGuess[]> GetGuess(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var url = AkiUrlBuilder.GetGuessUrl(BuildGuessRequest(), m_usedLanguage, m_usedServerType);
            var response = await m_webClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<BaseResponse<Guess>>(content,
                new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

            m_lastGuessStep = m_step;

            return result.Parameters.Characters.Select(p =>
                new AkinatorGuess(p.Name, p.Description)
                {
                    ID = p.Id,
                    PhotoPath = p.PhotoPath,
                    Probabilty = p.Probabilty
                }).ToArray();
        }

        public Task<AkinatorQuestion> StartNewGame() => StartNewGame(CancellationToken.None);

        public Task<AkinatorQuestion> Answer(AnswerOptions answer) => Answer(answer, CancellationToken.None);

        public Task<AkinatorQuestion> UndoAnswer() => UndoAnswer(CancellationToken.None);

        public Task<AkinatorGuess[]> SearchCharacter(string search) => SearchCharacter(search, CancellationToken.None);

        public Task<AkinatorGuess[]> GetGuess() => GetGuess(CancellationToken.None);

        public Task<AkinatorHallOfFameEntries[]> GetHallOfFame() => GetHallOfFame(CancellationToken.None);
        
        public bool GuessIsDue() =>
            GuessDueChecker.GuessIsDue(CurrentQuestion, m_lastGuessStep);

        private async Task<ApiKey> GetSession()
        {
            var response = await m_webClient.GetAsync("https://en.akinator.com/game").ConfigureAwait(false);
            if (response?.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Cannot connect to Akinator.com");
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var match = m_regexSession.Match(content);
            if (!match.Success)
            {
                throw new InvalidOperationException("Cannot retrieve the Api-Key from Akinator.com");
            }

            var apiKey = new ApiKey
            {
                SessionUid = match.Groups[1].Value,
                FrontAdress = match.Groups[2].Value
            };

            return apiKey;
        }

        public AkinatorUserSession GetUserSession() =>
            new AkinatorUserSession(m_session, m_signature, m_step, m_lastGuessStep);

        private static AkinatorQuestion ToAkinatorQuestion(Question question) =>
            new AkinatorQuestion(question.Text, question.Progression, question.Step);

        private AkinatorHallOfFameEntries[] ToHallOfFameEntry(List<Award> awardsAward) =>
            awardsAward
                .Select(p => new AkinatorHallOfFameEntries(
                    p.AwardId,
                    p.CharacterName,
                    p.Description,
                    p.Type,
                    p.WinnerName,
                    p.Delai,
                    p.Pos))
                .ToArray();

        private GuessRequest BuildGuessRequest() =>
            new GuessRequest(m_step, m_session, m_signature);

        private AnswerRequest BuildAnswerRequest(AnswerOptions choice) =>
            new AnswerRequest(choice, m_step, m_session, m_signature);

        private void Attach(AkinatorUserSession existingSession)
        {
            if (existingSession != null)
            {
                m_step = existingSession.Step;
                m_lastGuessStep = existingSession.LastGuessStep;
                m_session = existingSession.Session;
                m_signature = existingSession.Signature;
            }
        }

        public void Dispose()
        {
            m_webClient?.Dispose();
        }
    }
}
