using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class ClickEventMessageRequest : MessageRequest
    {
        public string EventKey { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            EventKey = rootNode.SelectSingleNode("EventKey").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}