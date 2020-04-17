using System.Collections.Generic;
using System.Xml.Serialization;

namespace Akinator.Api.Net.Model.External
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    [XmlRoot(ElementName = "CANDIDATS")]
    public class ServerSearchResultCandidats
    {
        [XmlElement(ElementName = "URL")]
        public List<string> URL { get; set; }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore InconsistentNaming
}