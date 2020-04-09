using System;
using System.Xml;
using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Request
{
    public class ScanCodeEventMessageRequest:MessageRequest
    {
        [XmlElement("ScanResult")]
        public string Result { get; set; }
        [XmlElement("ScanType")]
        public string ScanType { get; set; }
        public string EventKey { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            var infoNode = rootNode.SelectSingleNode("ScanCodeInfo");
            EventKey = rootNode.SelectSingleNode("EventKey").InnerText;
            ScanType = infoNode.SelectSingleNode("ScanType").InnerText;
            Result = infoNode.SelectSingleNode("ScanResult").InnerText;
        }
    }
}