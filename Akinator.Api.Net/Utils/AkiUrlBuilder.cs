using System;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net.Utils
{
    internal static class AkiUrlBuilder
    {
        public static string NewGame(ApiKey apiKey, Language language, ServerType serverType)
        {
            var server = ServerSelector.GetServerFor(language, serverType);
            if (string.IsNullOrEmpty(server))
            {
                throw new InvalidOperationException($"No server does match the language {language} and server type {serverType}.");
            }

            return
                $"https://{server}/new_session?partner=1&callback=jQuery331023608747682107778_{GetTime()}&player=website-desktop&uid_ext_session={apiKey.SessionUid}&frontaddr={apiKey.FrontAdress.UrlEncode()}&constraint=ETAT%3C%3E%27AV%27&&constraint=ETAT<>'AV'";
        }

        private static long GetTime()
        {
            long retval = 0;
            var st = new DateTime(1970, 1, 1);
            var t = (DateTime.Now.ToUniversalTime() - st);
            retval = (long)(t.TotalMilliseconds + 0.5);
            return retval;
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
    }
}