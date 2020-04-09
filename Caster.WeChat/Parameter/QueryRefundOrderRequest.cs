using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class QueryRefundOrderRequest : PayRequest
    {
        /// <summary>
        /// 微信商户订单号
        /// </summary>
        [XmlElement("transaction_id")]
        public string WeChatOrderCode { get; set; }

        /// <summary>
        /// 商户内部订单号
        /// </summary>
        [XmlElement("out_trade_no")]
        public string OrderCode { get; set; }
        
        
        /// <summary>
        /// 微信商户退款订单号
        /// </summary>
        [XmlElement("refund_id")]
        public string WeChatRefundOrderCode { get; set; }

        /// <summary>
        /// 商户内部退款订单号
        /// </summary>
        [XmlElement("out_refund_no")]
        public string RefundOrderCode { get; set; }
        
        /// <summary>
        /// 分页偏移量
        /// </summary>
        [XmlElement("offset")]
        public string Offset { get; set; }
    }
}