using System.Text;
using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Response
{
    public class VideoMessageResponse : MessageResponse
    {
        [XmlElement("MediaId")] public string MediaId { get; set; }
        [XmlElement("Url")] public string Title { get; set; }
        [XmlElement("Description")] public string Description { get; set; }

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
            xml.Append("video");
            xml.Append("</MsgType>");
            xml.Append("<Video>");
            xml.Append("<MediaId>");
            xml.Append(MediaId);
            xml.Append("</MediaId>");
            xml.Append("<Title>");
            xml.Append(Title);
            xml.Append("</Title>");
            xml.Append("<Description>");
            xml.Append(Description);
            xml.Append("</Description>");
            xml.Append("</Video>");

            return xml.ToString();
        }
    }
}