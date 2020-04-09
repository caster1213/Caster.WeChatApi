using System.Text;
using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Response
{
    public class ImageMessageResponse : MessageResponse
    {
        public string MediaId { get; set; }

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
            xml.Append("image");
            xml.Append("</MsgType>");
            xml.Append("<Image>");
            xml.Append("<MediaId>");
            xml.Append(MediaId);
            xml.Append("</MediaId>");
            xml.Append("</Image>");

            return xml.ToString();
        }
    }
}