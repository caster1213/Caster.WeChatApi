using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class EventSendCompleteMessageRequest:MessageRequest
    {
        public string Status { get; set; }
        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            Status = rootNode.SelectSingleNode("Status").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}