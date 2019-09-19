using System;
using System.Xml.Serialization;

namespace Akinator.Api.Net.Model.External
{
    [Serializable, XmlRoot("AWARD")]
    public class Award
    {
        [XmlElement(ElementName = "TYPE")]
        public string Type { get; set; }
        [XmlElement(ElementName = "AWARD_ID")]
        public string AwardId { get; set; }
        [XmlElement(ElementName = "NOM")]
        public string CharacterName { get; set; }
        [XmlElement(ElementName = "DESCRIPTION")]
        public string Description { get; set; }
        [XmlElement(ElementName = "PSEUDO")]
        public string WinnerName { get; set; }
        [XmlElement(ElementName = "POS")]
        public string Pos { get; set; }
        [XmlElement(ElementName = "DELAI")]
        public string Delai { get; set; }
    }
}