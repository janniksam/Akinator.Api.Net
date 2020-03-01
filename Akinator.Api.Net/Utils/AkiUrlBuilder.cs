using System;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net.Utils
{
    internal static class AkiUrlBuilder
    {
        public static string NewGame(ApiKey apiKey, Language language, ServerType serverType, bool childMode)
        {
            var child_switch = String.Empty;
            var question_filter = String.Empty;

            var server = ServerSelector.GetServerFor(language, serverType);
            if (string.IsNullOrEmpty(server))
            {
                throw new InvalidOperationException($"No server does match the language {language} and server type {serverType}.");
            }

            if (childMode)
            {
                child_switch = "true";
                question_filter = "cat%3D1";
            }

            return
                $"https://{server}/new_session?partner=1&callback=jQuery331023608747682107778_{GetTime()}&player=website-desktop&uid_ext_session={apiKey.SessionUid}&frontaddr={apiKey.FrontAdress.UrlEncode()}&childMod={child_switch}&constraint=ETAT%3C%3E%27AV%27&&constraint=ETAT<>'AV'&question_filter={question_filter}";
        }

        public static string MapHallOfFame(Language usedLanguage)
        {
            switch (usedLanguage)
            {
                case Language.Arabic:
                {
                    return "http://classement.akinator.com:18666//get_hall_of_fame.php?basel_id=12";
                }
                case Language.English:
                {
                    return "http://classement.akinator.com:18666//get_hall_of_fame.php?basel_id=25";
                }
                case Language.German:
                {
                    return "http://classement.akinator.com:18666//get_hall_of_fame.php?basel_id=5";
                }
                case Language.French:
                {
                    return "http://classement.akinator.com:18666//get_hall_of_fame.php?basel_id=1";
                }
                default:
                {
                    throw new NotSupportedException($"The language {usedLanguage} is currently not supporting the hall of fame.");
                }
            }
        }

        public static string Answer(
            AnswerRequest request,
            Language language, 
            ServerType serverType)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var server = ServerSelector.GetServerFor(language, serverType);
            if (string.IsNullOrEmpty(server))
            {
                throw new InvalidOperationException($"No server does match the language {language} and server type {serverType}.");
            }

            var url = $"https://{server}/answer?session={request.Session}&signature={request.Signature}&step={request.Step}&answer={(int)request.Choice}";
            return url;
        }

        public static string UndoAnswer(
            string session,
            string signature,
            int step,
            Language language,
            ServerType serverType)
        {
            var server = ServerSelector.GetServerFor(language, serverType);
            if (string.IsNullOrEmpty(server))
            {
                throw new InvalidOperationException($"No server does match the language {language} and server type {serverType}.");
            }

            var url = $"https://{server}/cancel_answer?session={session}&signature={signature}&step={step}&answer=-1";
            return url;
        }
        
        public static string SearchCharacter(
            string search,
            string Session,
            string signature,
            int step,
            Language language,
            ServerType serverType)
        {
            
            var str = search.UrlEncode();

            var server = ServerSelector.GetServerFor(language, serverType);
            if (string.IsNullOrEmpty(server))
            {
                throw new InvalidOperationException($"No server does match the language {language} and server type {serverType}.");
            }

            var url = $"https://{server}/soundlike_search?session={Session}&signature={signature}&step={step}&name={str}";
            return url;
        }

        public static string GetGuessUrl(
            GuessRequest request,
            Language language, 
            ServerType serverType)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var server = ServerSelector.GetServerFor(language, serverType);
            if (string.IsNullOrEmpty(server))
            {
                throw new InvalidOperationException($"No server does match the language {language} and server type {serverType}.");
            }

            var url = $"https://{server}/list?session={request.Session}&signature={request.Signature}&step={request.Step}";
            return url;
        }

        private static long GetTime()
        {
            var st = new DateTime(1970, 1, 1);
            var t = (DateTime.Now.ToUniversalTime() - st);
            var retval = (long)(t.TotalMilliseconds + 0.5);
            return retval;
        }
    }
}
