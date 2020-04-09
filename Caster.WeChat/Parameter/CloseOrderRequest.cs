using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class CloseOrderRequest:PayRequest
    {
        [XmlElement("out_trade_no")]
        public string OrderCode { get; set; }
    }
}