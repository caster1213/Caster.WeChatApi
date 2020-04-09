using System.Xml;
using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Request
{
    [XmlRoot("xml")]
    public class TextMessageRequest:MessageRequest
    {
        public string Content { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            Content = rootNode.SelectSingleNode("Content").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}