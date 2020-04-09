using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class LocationMessageRequest:MessageRequest
    {
        public string X { get; set; }
        public string Y { get; set; }
        public int Scale { get; set; }
        public string Label { get; set; }
        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            X = rootNode.SelectSingleNode("Location_X").InnerText;
            Y = rootNode.SelectSingleNode("Location_Y").InnerText;
            Scale = Convert.ToInt32(rootNode.SelectSingleNode("Scale").InnerText);
            Label = rootNode.SelectSingleNode("Label").InnerText;
        }
    }
}