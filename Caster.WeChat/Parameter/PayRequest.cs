using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class PayRequest
    {
        [XmlElement("appid")]
        public  string AppId { get; set; }
        [XmlElement("mch_id")]
        public string MchId { get; set; }
        [XmlElement("nonce_str")]
        public string Nonce { get; set; }
        [XmlElement("sign")]
        public string Sign { get; set; }
    }
}