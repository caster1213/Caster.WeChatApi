using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class VideoMessageRequest:MessageRequest
    {
        public string MediaId { get; set; }
        public string ThumbMediaId { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            MediaId = rootNode.SelectSingleNode("MediaId").InnerText;
            ThumbMediaId = rootNode.SelectSingleNode("ThumbMediaId").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}