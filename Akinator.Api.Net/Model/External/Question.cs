using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akinator.Api.Net.Model.External
{
    internal class Question : IBaseParameters
    {
        [JsonProperty("question")]
        public string Text { get; set; }

        [JsonProperty("answers")]
        private List<Answer> Answers { get; set; }

        [JsonProperty("step")]
        public int Step { get; set; }

        [JsonProperty("progression")]
        public double Progression { get; set; }

        [JsonProperty("questionid")]
        public int QuestionId { get; set; }

        [JsonProperty("Infogain")]
        public double Infogain { get; set; }

        public override string ToString() => Text;
    }
}