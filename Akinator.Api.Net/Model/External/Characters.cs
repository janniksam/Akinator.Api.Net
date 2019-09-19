using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Akinator.Api.Net.Model.External
{
    internal class Characters : IBaseParameters
    {

        [JsonProperty("soundlikes")]
        public IReadOnlyList<Soundlike> Soundlikes { get; set; }

        public partial class Soundlike
        {
            [JsonProperty("element")]
            public Element Element { get; set; }
        }

        public partial class Element
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("id_base")]
            public ulong IdBase { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("absolute_picture_path")]
            public Uri PhotoPath { get; set; }
        }

        public IReadOnlyCollection<Element> _Characters => Soundlikes.Select(a => a.Element).ToList();
    }
}