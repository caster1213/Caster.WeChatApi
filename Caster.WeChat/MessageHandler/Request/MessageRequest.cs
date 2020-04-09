using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public abstract class MessageRequest
    {
        public string ToUserName { get; set; }
        public string FormUserName { get; set; }
        public long CreateTime { get; set; }
        public long MsgId { get; set; }
        public string MessageType { get; set; }

        public virtual void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            CreateTime = Convert.ToInt64(xmlDocument.FirstChild.SelectSingleNode("CreateTime").InnerText);
            ToUserName = rootNode.SelectSingleNode("ToUserName").InnerText;
            FormUserName = rootNode.SelectSingleNode("FromUserName").InnerText;
            MessageType = rootNode.SelectSingleNode("MsgType").InnerText;
            if (MessageType.Equals("event", StringComparison.InvariantCultureIgnoreCase) == false)
            {
                MsgId = Convert.ToInt64(xmlDocument.FirstChild.SelectSingleNode("MsgId").InnerText);
            }
        }
    }
}