using System.Collections.Generic;
using System.Xml.Serialization;

namespace Akinator.Api.Net.Model.External
{
	// ReSharper disable InconsistentNaming
	// ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
	[XmlRoot(ElementName = "LANGUAGE")]
    public class LANGUAGE
    {
		[XmlElement(ElementName = "LANG_ID")]
		public string LANG_ID { get; set; }
	}

	[XmlRoot(ElementName = "SUBJECT")]
	public class SUBJECT
	{
		[XmlElement(ElementName = "SUBJ_ID")]
		public string SUBJ_ID { get; set; }
		[XmlElement(ElementName = "SUBJ_LABEL")]
		public string SUBJ_LABEL { get; set; }
		[XmlElement(ElementName = "SUBJ_PRICE")]
		public string SUBJ_PRICE { get; set; }
		[XmlElement(ElementName = "SUBJ_PICTURE_URL")]
		public string SUBJ_PICTURE_URL { get; set; }
		[XmlElement(ElementName = "SUBJ_ICON_URL")]
		public string SUBJ_ICON_URL { get; set; }
	}

	[XmlRoot(ElementName = "CANDIDATS")]
	public class CANDIDATS
	{
		[XmlElement(ElementName = "URL")]
		public List<string> URL { get; set; }
	}

	[XmlRoot(ElementName = "INSTANCE")]
	public class INSTANCE
	{
		[XmlElement(ElementName = "RANKING_LIMIT")]
		public string RANKING_LIMIT { get; set; }
		[XmlElement(ElementName = "SERVICE_ID")]
		public string SERVICE_ID { get; set; }
		[XmlElement(ElementName = "BASE_LOGIQUE_ID")]
		public string BASE_LOGIQUE_ID { get; set; }
		[XmlElement(ElementName = "TRADS_APP_ID")]
		public string TRADS_APP_ID { get; set; }
		[XmlElement(ElementName = "TYPE_SESSION")]
		public string TYPE_SESSION { get; set; }
		[XmlElement(ElementName = "NOM_RESEAU_ENTROPIQUE")]
		public string NOM_RESEAU_ENTROPIQUE { get; set; }
		[XmlElement(ElementName = "NOM_EXTERNE_VAR_CIBLE")]
		public string NOM_EXTERNE_VAR_CIBLE { get; set; }
		[XmlElement(ElementName = "TRANSLATED_SUBJECT_NAME")]
		public string TRANSLATED_SUBJECT_NAME { get; set; }
        [XmlElement(ElementName = "LANGUAGE")]
		public LANGUAGE LANGUAGE { get; set; }
		[XmlElement(ElementName = "NB_FREE_GAMES_ALLOWED")]
		public string NB_FREE_GAMES_ALLOWED { get; set; }
		[XmlElement(ElementName = "SUBJECT")]
		public SUBJECT SUBJECT { get; set; }
		[XmlElement(ElementName = "URL_BASE_WS")]
		public string URL_BASE_WS { get; set; }
		[XmlElement(ElementName = "ENGINE_VERSION")]
		public string ENGINE_VERSION { get; set; }
		[XmlElement(ElementName = "LOAD_RATE")]
		public string LOAD_RATE { get; set; }
		[XmlElement(ElementName = "VERSION_ANDROID")]
		public string VERSION_ANDROID { get; set; }
		[XmlElement(ElementName = "CANDIDATS")]
		public CANDIDATS CANDIDATS { get; set; }
		[XmlElement(ElementName = "PRIO_AVAILABLE")]
		public string PRIO_AVAILABLE { get; set; }
		[XmlElement(ElementName = "STATE")]
		public string STATE { get; set; }
		[XmlElement(ElementName = "CAPS_DISABLED")]
		public CAPS_DISABLED CAPS_DISABLED { get; set; }
	}

	[XmlRoot(ElementName = "CAPS_DISABLED")]
	public class CAPS_DISABLED
	{
		[XmlElement(ElementName = "CAP")]
		public List<string> CAP { get; set; }
	}

	[XmlRoot(ElementName = "PARAMETERS")]
	public class PARAMETERS
	{
		[XmlElement(ElementName = "INSTANCE")]
		public List<INSTANCE> INSTANCE { get; set; }
	}

	[XmlRoot(ElementName = "RESULT")]
	public class ServerSearchResult
	{
		[XmlElement(ElementName = "COMPLETION")]
		public string COMPLETION { get; set; }
		[XmlElement(ElementName = "CODE_PAYS")]
		public string CODE_PAYS { get; set; }
		[XmlElement(ElementName = "PARAMETERS")]
        public PARAMETERS PARAMETERS { get; set; }
    }
    // ReSharper restore UnusedMember.Global
	// ReSharper restore ClassNeverInstantiated.Global
	// ReSharper restore InconsistentNaming
}
