using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class UploadLocationEventMessageRequest:MessageRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Precision { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            Latitude = Convert.ToDouble(rootNode.SelectSingleNode("Latitude").InnerText);
            Longitude = Convert.ToDouble(rootNode.SelectSingleNode("Longitude").InnerText);
            Precision = Convert.ToDouble(rootNode.SelectSingleNode("Precision").InnerText);
            base.SerializationMessage(xmlDocument);
        }
    }
}