using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class LinkMessageRequest : MessageRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            Title = rootNode.SelectSingleNode("Title").InnerText;
            Description = rootNode.SelectSingleNode("Description").InnerText;
            Url = rootNode.SelectSingleNode("Url").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}