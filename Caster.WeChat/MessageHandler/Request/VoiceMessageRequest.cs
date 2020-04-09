using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class VoiceMessageRequest : MessageRequest
    {
        public string MediaId { get; set; }
        public string Format { get; set; }

        /// <summary>
        /// 语音识别结果
        /// </summary>
        public string Result { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            MediaId = rootNode.SelectSingleNode("MediaId").InnerText;
            Format = rootNode.SelectSingleNode("Format").InnerText;
            if (rootNode.SelectSingleNode("Recognition") != null)
            {
                Result = rootNode.SelectSingleNode("Recognition").InnerText;
            }

            base.SerializationMessage(xmlDocument);
        }
    }
}