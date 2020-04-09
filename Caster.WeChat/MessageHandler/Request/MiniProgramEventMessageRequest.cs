using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class MiniProgramEventMessageRequest:MessageRequest
    {
        public string EventKey { get; set; }
        public string MenuId { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            EventKey = rootNode.SelectSingleNode("EventKey").InnerText;
            MenuId = rootNode.SelectSingleNode("MenuId").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}