using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class QueryOrderBillRequest:PayRequest
    {
        /// <summary>
        /// 下载对账单的日期 格式：20140603
        /// </summary>
        [XmlElement("bill_date")]
        public string Date { get; set; }
        /// <summary>
        /// ALL（默认值），返回当日所有订单信息（不含充值退款订单）
        /// SUCCESS，返回当日成功支付的订单（不含充值退款订单）
        /// REFUND，返回当日退款订单（不含充值退款订单）
        /// RECHARGE_REFUND，返回当日充值退款订单
        /// </summary>
        [XmlElement("bill_type")]
        public string Type { get; set; }
    }
}