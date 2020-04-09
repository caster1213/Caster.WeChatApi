using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class RefundOrderRequest : PayRequest
    {
        [XmlElement("transaction_id")] public string WeChatOrderCode { get; set; }
        [XmlElement("out_trade_no")] public string OrderCode { get; set; }
        [XmlElement("out_refund_no")] public string RefundOrderCode { get; set; }
        [XmlElement("total_fee")] public int Total { get; set; }
        [XmlElement("refund_fee")] public int RefundAmount { get; set; }
        [XmlElement("refund_fee_type")] public string CoinType { get; set; }
        [XmlElement("refund_desc")] public string Remark { get; set; }
        [XmlElement("notify_url")] public string NotifyCallUrl { get; set; }
        [XmlElement("refund_account")] public string AccountSource { get; set; }
    }
}