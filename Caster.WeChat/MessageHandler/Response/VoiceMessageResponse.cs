using System.Text;

namespace Caster.WeChat.MessageHandler.Response
{
    public class VoiceMessageResponse : MessageResponse
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
            xml.Append("voice");
            xml.Append("</MsgType>");
            xml.Append("<Voice>");
            xml.Append("<MediaId>");
            xml.Append(MediaId);
            xml.Append("</MediaId>");
            xml.Append("</Voice>");

            return xml.ToString();
        }
    }
}