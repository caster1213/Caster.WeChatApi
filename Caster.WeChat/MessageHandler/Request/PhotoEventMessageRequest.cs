using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class PhotoEventMessageRequest : MessageRequest
    {
        /// <summary>
        /// key
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// md5
        /// </summary>
        public List<string> Item { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            Item = new List<string>();
            var rootNode = xmlDocument.FirstChild;
            var sendNode = xmlDocument.FirstChild.SelectSingleNode("SendPicsInfo");
            EventKey = rootNode.SelectSingleNode("EventKey").InnerText;
            Count = Convert.ToInt32(sendNode.SelectSingleNode("Count").InnerText);
            var itemNodes = rootNode.SelectNodes("/item");
            for (int i = 0; i <= itemNodes.Count - 1; i++)
            {
                var md5 = itemNodes[i].SelectSingleNode("PicMd5Sum").InnerText;

                Item.Add(md5);
            }
            base.SerializationMessage(xmlDocument);
        }
    }
}