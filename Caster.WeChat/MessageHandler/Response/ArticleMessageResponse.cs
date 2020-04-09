using System.Collections.Generic;
using System.Text;

namespace Caster.WeChat.MessageHandler.Response
{
    public class ArticleMessageResponse : MessageResponse
    {
        public class ArticleItem
        {
            public string Title { get; set; }
            public string PicUrl { get; set; }
            public string Url { get; set; }
            public string Description { get; set; }
        }

        public int ArticleCount { get; set; }

        public List<ArticleItem> Items { get; set; }

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
            xml.Append("news");
            xml.Append("</MsgType>");
            xml.Append("<Articles>");
            foreach (var item in Items)
            {
                xml.Append("<Item>");
                xml.Append("<Title>");
                xml.Append(item.Title);
                xml.Append("</Title>");
                xml.Append("<PicUrl>");
                xml.Append(item.PicUrl);
                xml.Append("</PicUrl>");
                xml.Append("<Url>");
                xml.Append(item.Url);
                xml.Append("</Url>");
                xml.Append("<Description>");
                xml.Append(item.Description);
                xml.Append("</Description>");
                xml.Append("</Item>");
            }

            xml.Append("</Articles>");

            return xml.ToString();
        }
    }
}