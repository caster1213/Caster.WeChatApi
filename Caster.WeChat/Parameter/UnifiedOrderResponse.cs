using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class UnifiedOrderResponse : PayResponse
    {
        /// <summary>
        /// 支付类型
        /// </summary>
        [XmlElement("trade_type")]
        public string TradeType { get; set; }
        /// <summary>
        /// 已支付会话id
        /// </summary>
        [XmlElement("prepay_id")]
        public string PrepayId { get; set; }
        /// <summary>
        /// 二维码支付连接
        /// </summary>
        [XmlElement("code_url")]
        public string CodeUrl { get; set; }
    }
}