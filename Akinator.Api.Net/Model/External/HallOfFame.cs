using System;
using System.Xml.Serialization;

namespace Akinator.Api.Net.Model.External
{
    [Serializable, XmlRoot("RESULT")]
    public class HallOfFame
    {
        [XmlElement(ElementName = "COMPLETION")]
        public string Completion { get; set; }
        [XmlElement(ElementName = "NB_AWARDS")]
        public string NbAwards { get; set; }
        [XmlElement(ElementName = "AWARDS")]
        public AwardCollection Awards { get; set; }
    }
}