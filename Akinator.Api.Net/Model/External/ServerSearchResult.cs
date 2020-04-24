using System.Xml.Serialization;

namespace Akinator.Api.Net.Model.External
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    [XmlRoot(ElementName = "RESULT")]
    public class ServerSearchResult
    {
        [XmlElement(ElementName = "COMPLETION")]
        public string COMPLETION { get; set; }
        [XmlElement(ElementName = "CODE_PAYS")]
        public string CODE_PAYS { get; set; }
        [XmlElement(ElementName = "PARAMETERS")]
        public ServerSearchResultParameters PARAMETERS { get; set; }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore InconsistentNaming
}