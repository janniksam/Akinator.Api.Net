using System;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net.Utils
{
    internal static class AkiUrlBuilder
    {
        public static string NewGame(ApiKey apiKey, IAkinatorServer server, bool childMode)
        {
            var childSwitch = string.Empty;
            var questionFilter = string.Empty;
            if (childMode)
            {
                childSwitch = "true";
                questionFilter = "cat%3D1";
            }
            
            return
                $"https://en.akinator.com/new_session?callback=jQuery3410014644797238627216_{GetTime()}&urlApiWs={Uri.EscapeDataString(server.ServerUrl)}&player=website-desktop&&partner=1&uid_ext_session={apiKey.SessionUid}&frontaddr={apiKey.FrontAdress.UrlEncode()}&childMod={childSwitch}&constraint={Uri.EscapeDataString("ETAT<>'AV'")}&soft_constraint=&question_filter={questionFilter}&_={GetTime()}";
        }

        public static string MapHallOfFame(IAkinatorServer server)
        {
            return $"http://classement.akinator.com:18666//get_hall_of_fame.php?basel_id={server.BaseId}";
        }

        public static string Answer(
            AnswerRequest request,
            IAkinatorServer server)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var url = $"{server.ServerUrl}/answer?session={request.Session}&signature={request.Signature}&step={request.Step}&answer={(int)request.Choice}";
            return url;
        }

        public static string UndoAnswer(
            string session,
            string signature,
            int step,
            IAkinatorServer server)
        {
            var url = $"{server.ServerUrl}/cancel_answer?session={session}&signature={signature}&step={step}&answer=-1";
            return url;
        }
        
        public static string SearchCharacter(
            string search,
            string session,
            string signature,
            int step,
            IAkinatorServer server)
        {
            var str = search.UrlEncode();
            var url = $"{server.ServerUrl}/soundlike_search?session={session}&signature={signature}&step={step}&name={str}";
            return url;
        }

        public static string GetGuessUrl(
            GuessRequest request,
            IAkinatorServer server)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var url = $"{server.ServerUrl}/list?session={request.Session}&signature={request.Signature}&step={request.Step}";
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
