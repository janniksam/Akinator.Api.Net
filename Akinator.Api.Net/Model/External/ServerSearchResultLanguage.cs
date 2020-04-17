using System.Xml.Serialization;

namespace Akinator.Api.Net.Model.External
{
	// ReSharper disable InconsistentNaming
	// ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
	[XmlRoot(ElementName = "LANGUAGE")]
    public class ServerSearchResultLanguage
    {
		[XmlElement(ElementName = "LANG_ID")]
		public string LANG_ID { get; set; }
	}
	// ReSharper restore UnusedMember.Global
	// ReSharper restore ClassNeverInstantiated.Global
	// ReSharper restore InconsistentNaming
}
