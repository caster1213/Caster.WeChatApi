using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class ImageMessageRequest:MessageRequest
    {
        public string ImageUrl { get; set; }
        public string MediaId { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            MediaId = rootNode.SelectSingleNode("PicUrl").InnerText;
            ImageUrl = rootNode.SelectSingleNode("MediaId").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}