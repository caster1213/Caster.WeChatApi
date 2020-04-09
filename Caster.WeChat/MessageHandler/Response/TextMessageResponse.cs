using System.Text;
using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Response
{
    [XmlRoot("xml")]
    public class TextMessageResponse : MessageResponse
    {
        [XmlElement("Content")] public string Content { get; set; }

        public override string ToXml()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<xml>");
            xml.Append("<ToUserName>");
            xml.Append(ToUserName);
            xml.Append("</ToUserName>");
            xml.Append("<FromUserName>");
            xml.Append(FormUserName);
            xml.Append("</FromUserName>");
            xml.Append("<CreateTime>");
            xml.Append(CreateTime);
            xml.Append("</CreateTime>");
            xml.Append("<MsgType>");
            xml.Append("text");
            xml.Append("</MsgType>");
            xml.Append("<Content>");
            xml.Append(Content);
            xml.Append("</Content>");

            return xml.ToString();
        }
    }
}