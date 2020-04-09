using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class LocationEventMessageRequest : MessageRequest
    {
        public string EventKey { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public int Scale { get; set; }
        public string Label { get; set; }
        public string PoiName { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            var locationNode = rootNode.SelectSingleNode("SendLocationInfo");
            EventKey = rootNode.SelectSingleNode("EventKey").InnerText;
            X = locationNode.SelectSingleNode("Location_X").InnerText;
            Y = locationNode.SelectSingleNode("Location_Y").InnerText;
            Scale = Convert.ToInt32(locationNode.SelectSingleNode("Scale").InnerText);
            Label = locationNode.SelectSingleNode("Label").InnerText;
            PoiName = locationNode.SelectSingleNode("Poiname").InnerText;
            base.SerializationMessage(xmlDocument);
        }
    }
}