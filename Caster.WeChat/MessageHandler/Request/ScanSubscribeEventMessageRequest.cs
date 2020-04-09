using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class ScanSubscribeEventMessageRequest:MessageRequest
    {
        public string EventKey { get; set; }
        public string Ticket { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            EventKey = rootNode.SelectSingleNode("EventKey").InnerText;
            Ticket = rootNode.SelectSingleNode("Ticket").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}