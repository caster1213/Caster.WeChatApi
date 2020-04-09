using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Response
{
    [XmlRoot("xml")]
    public class EncryptMessageResponse
    {
        [XmlElement("MsgSignature")]
        public string Sign { get; set; }
        [XmlElement("Nonce")]
        public string Nonce { get; set; }
        [XmlElement("TimeStamp")]
        public string TimeStamp { get; set; }
        [XmlElement("Encrypt")]
        public string Value { get; set; }
    }
}