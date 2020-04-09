using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Response
{
    public abstract class MessageResponse
    {
        [XmlElement("ToUserName")]
        public string ToUserName { get; set; }
        [XmlElement("FromUserName")]
        public string FormUserName { get; set; }
        [XmlElement("CreateTime")]
        public long CreateTime { get; set; }


        public abstract string ToXml();



    }
}