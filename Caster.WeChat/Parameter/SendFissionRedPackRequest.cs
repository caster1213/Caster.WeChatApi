using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class SendFissionRedPackRequest:SendRedPackRequest
    {
        /// <summary>
        /// 红包金额设置方式
        /// </summary>
        [XmlElement("amt_type")] public string SetModel => "ALL_RAND";
    }
}