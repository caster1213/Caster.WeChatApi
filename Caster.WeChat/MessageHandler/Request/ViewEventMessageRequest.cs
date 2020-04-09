using System;
using System.Xml;

namespace Caster.WeChat.MessageHandler.Request
{
    public class ViewEventMessageRequest:MessageRequest
    {
        public string EventKey { get; set; }
        /// <summary>
        /// MenuId 点击个性化菜单推送不为Null
        /// 否则始终为Null
        /// </summary>
        public string MenuId { get; set; }

        public override void SerializationMessage(XmlDocument xmlDocument)
        {
            var rootNode = xmlDocument.FirstChild;
            EventKey = rootNode.SelectSingleNode("EventKey").InnerText;
            if (rootNode.SelectSingleNode("MenuID") != null)
            {
                MenuId = rootNode.SelectSingleNode("MenuID").InnerText;
            }
           
            base.SerializationMessage(xmlDocument);
        }
    }
}