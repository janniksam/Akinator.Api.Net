namespace Akinator.Api.Net.Model
{
    public class GuessRequest
    {
        public GuessRequest(int step, string session, string signature)
        {
            Step = step;
            Session = session;
            Signature = signature;
        }

        public int Step { get; }

        public string Session { get; }

        public string Signature { get; }
    }
}