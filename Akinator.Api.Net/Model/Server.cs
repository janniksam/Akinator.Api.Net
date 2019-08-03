namespace Akinator.Api.Net.Model
{
    public class Server
    {
        public Server(ServerType type, string url)
        {
            Type = type;
            Url = url;
        }

        public ServerType Type { get; }

        public string Url { get; }
    }

    public enum ServerType
    {
        Person = 0,
        Animal = 1,
        Object = 2
    }
}