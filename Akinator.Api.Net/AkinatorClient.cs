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
        private readonly Regex m_regexStartGameResult = new Regex(@"^jQuery331023608747682107778_\d+\((.+)\)$");
        private readonly HttpClient m_webClient;
        private readonly Language m_usedLanguage;
        private readonly ServerType m_usedServerType;
        private string m_session;
        private string m_signature;
        private int m_step;
        private int m_lastGuessStep;
        private List<AkinatorGuess> m_excludedList;
        private AkinatorQuestion m_currentQuestion;

        public AkinatorClient(Language language, ServerType serverType, AkinatorUserSession existingSession = null)
        {
            m_webClient = new HttpClient();
            m_usedLanguage = language;
            m_usedServerType = serverType;
            Attach(existingSession);
        }

        public async Task<AkinatorQuestion> StartNewGame(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var apiKey = await GetSession().ConfigureAwait(false);
            var url = AkiUrlBuilder.NewGame(apiKey, m_usedLanguage, m_usedServerType);
            
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
            m_currentQuestion = ToAkinatorQuestion(result.Parameters.StepInformation);
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
            m_currentQuestion = ToAkinatorQuestion(result.Parameters);
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
            m_currentQuestion = ToAkinatorQuestion(result.Parameters);
            return ToAkinatorQuestion(result.Parameters);
        }
        
        public async Task<AkinatorGuess[]> SearchCharacter(string search, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var url = AkiUrlBuilder.SearchCharacter(search, m_session, m_signature, m_step, m_usedLanguage, m_usedServerType);

            var response = await m_webClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<BaseResponse<Characters>>(content,
                new JsonSerializerSettings()
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });
                
            return result.Parameters._Characters.Select(p =>
                new AkinatorGuess(p.Name, p.Description)
                {
                    PhotoPath = p.PhotoPath,
                    ID = p.IdBase
                }).ToArray();
        }
        
        public AkinatorQuestion GetCurrentQuestion()
        {
            return m_currentQuestion;
        }
        
        public void ExcludeGuess(AkinatorGuess guess)
        {
            m_excludedList.Add(guess);
        }
        
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

            var original = result.Parameters.Characters.Select(p =>
                new AkinatorGuess(p.Name, p.Description)
                {
                    PhotoPath = p.PhotoPath,
                    ID = p.Id,
                    Probabilty = p.Probabilty
                }).ToList();
            foreach(var item in m_excludedList)
            {
               original.Remove(item);
            }    
            var excluded = original.ToArray();
            return excluded;
        }

        public Task<AkinatorQuestion> StartNewGame() => StartNewGame(CancellationToken.None);

        public Task<AkinatorQuestion> Answer(AnswerOptions answer) => Answer(answer, CancellationToken.None);

        public Task<AkinatorQuestion> UndoAnswer() => UndoAnswer(CancellationToken.None);
        
        public Task<AkinatorGuess[]> SearchCharacter(string search) => SearchCharacter(search, CancellationToken.None);

        public Task<AkinatorGuess[]> GetGuess() => GetGuess(CancellationToken.None);

        public Task<AkinatorHallOfFameEntries[]> GetHallOfFame() => GetHallOfFame(CancellationToken.None);

        public bool GuessIsDue(AkinatorQuestion question)
        {
            if (question.Progression > 80 ||
                question.Step - m_lastGuessStep == 25)
            {
                return true;
            }

            return false;
        }

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
