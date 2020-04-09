using System;
using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Request
{
    [XmlRoot("root")]
    public class RefundNotificationRequest:PayNotificationRequest
    {
        [XmlIgnore]
        public string MchId { get; set; }
        [XmlIgnore]
        public string AppId { get; set; }
        [XmlIgnore]
        public string Nonce { get; set; }
        
        [XmlElement("transaction_id")]
        public string WeChatPayCode { get; set; }
        [XmlElement("out_trade_no")]
        public string OrderCode { get; set; }
        [XmlElement("refund_id")]
        public string WeChatRefundCode { get; set; }
        [XmlElement("out_refund_no")]
        public string OrderRefundCode { get; set; }
        [XmlElement("total_fee")]
        public int OrderTotal { get; set; }
        [XmlElement("settlement_total_fee")]
        public int PayAmount { get; set; }
        [XmlElement("refund_fee")]
        public string ApplyRefundAmount { get; set; }
        [XmlElement("settlement_refund_fee")]
        public int RefundAmount { get; set; }
        [XmlElement("success_time")]
        public string RefundTime { get; set; }
        [XmlElement("refund_status")]
        public string Status { get; set; }
        [XmlElement("refund_recv_accout")]
        public string AccountSource { get; set; }
        [XmlElement("refund_account")]
        public string FundSource { get; set; }
        [XmlElement("refund_request_source")]
        public string RequestSource { get; set; }
    }
    
}