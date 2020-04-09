using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class QueryOrderRequest:PayRequest
    {
        [XmlElement("transaction_id")]
        public string WeChatOrderCode { get; set; }
        [XmlElement("out_trade_no")]
        public string OrderCode { get; set; }
    }
}