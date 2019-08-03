using Newtonsoft.Json;

namespace Akinator.Api.Net.Model.External
{
    internal class Answer
    {
        [JsonProperty("answer")]
        public string Text { get; set; }
    }
}