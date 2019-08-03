using System;
using Newtonsoft.Json;

namespace Akinator.Api.Net.Model.External
{
    internal class Character
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id_base")]
        private ulong IdBase { get; set; }

        [JsonProperty("proba")]
        public float Probabilty { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ranking")]
        public int Ranking { get; set; }

        [JsonProperty("absolute_picture_path")]
        public Uri PhotoPath { get; set; }
    }
}