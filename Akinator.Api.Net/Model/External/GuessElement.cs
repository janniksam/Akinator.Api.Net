using Newtonsoft.Json;

namespace Akinator.Api.Net.Model.External
{
    internal class GuessElement
    {
        [JsonProperty("element")]
        public Character Character { get; set; }
    }
}