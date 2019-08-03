using Newtonsoft.Json;

namespace Akinator.Api.Net.Model.External
{
    internal class NewGameParameters : IBaseParameters
    {
        [JsonProperty("identification")]
        internal Identification Identification { get; set; }

        [JsonProperty("step_information")]
        public Question StepInformation { get; set; }
    }
}