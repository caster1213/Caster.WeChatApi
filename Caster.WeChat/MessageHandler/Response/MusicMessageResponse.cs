using System.Text;
using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Response
{
    public class MusicMessageResponse : MessageResponse
    {
        [XmlElement("Title")] public string Title { get; set; }
        [XmlElement("Description")] public string Description { get; set; }
        [XmlElement("MusicUrl")] public string MusicUrl { get; set; }
        [XmlElement("HQMusicUrl")] public string HQMusicUrl { get; set; }
        [XmlElement("ThumbMediaId")] public string ThumbMediaId { get; set; }

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
            xml.Append("music");
            xml.Append("</MsgType>");
            xml.Append("<Music>");
            xml.Append("<Title>");
            xml.Append(Title);
            xml.Append("</Title>");
            xml.Append("<HQMusicUrl>");
            xml.Append(HQMusicUrl);
            xml.Append("</HQMusicUrl>");
            xml.Append("<MusicUrl>");
            xml.Append(MusicUrl);
            xml.Append("</MusicUrl>");
            xml.Append("<Description>");
            xml.Append(Description);
            xml.Append("</Description>");
            xml.Append("<ThumbMediaId>");
            xml.Append(ThumbMediaId);
            xml.Append("</ThumbMediaId>");
            xml.Append("</Music>");

            return xml.ToString();
        }
    }
}