using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Akinator.Api.Net.Model.External
{
    internal class Guess : IBaseParameters
    {
        [JsonProperty("elements")]
        public IReadOnlyList<GuessElement> Elements { get; set; }

        [JsonProperty("NbObjetsPertinents")]
        private int ObjectCount { get; set; }

        public IReadOnlyCollection<Character> Characters => Elements.Select(a => a.Character).ToList();
    }
}