using Akinator.Api.Net.Enumerations;

namespace Akinator.Api.Net.Model
{
    public class AnswerRequest
    {
        public AnswerRequest(AnswerOptions choice, int step, string session, string signature)
        {
            Choice = choice;
            Step = step;
            Session = session;
            Signature = signature;
        }

        public AnswerOptions Choice { get; }
        
        public int Step { get; }

        public string Session { get; }
        
        public string Signature { get; }
    }
}