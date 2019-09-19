using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Akinator.Api.Net.Model.External
{
    [Serializable, XmlRoot("AWARDS")]
    public class AwardCollection
    {
        [XmlElement(ElementName = "AWARD")]
        public List<Award> Award { get; set; }
    }
}