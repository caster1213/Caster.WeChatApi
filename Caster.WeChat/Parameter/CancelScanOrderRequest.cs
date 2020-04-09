using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class CancelScanOrderRequest:PayRequest
    {
        [XmlElement("transaction_id")]
        public string PayCode { get; set; }
        [XmlElement("out_trade_no")]
        public string OrderCode { get; set; }
    }
}