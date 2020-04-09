using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class RefundOrderResponse:PayResponse
    {
        [XmlElement("transaction_id")] public string WeChatOrderCode { get; set; }
        [XmlElement("out_trade_no")] public string OrderCode { get; set; }
        [XmlElement("out_refund_no")] public string RefundOrderCode { get; set; }
        [XmlElement("refund_id")] public string WeChatRefundOrderCode { get; set; }
        [XmlElement("total_fee")] public int RefundTotalAmount { get; set; }
        [XmlElement("settlement_refund_fee")] public int SettlementAmount { get; set; }
        [XmlElement("refund_fee_type")] public string CoinType { get; set; }
        [XmlElement("cash_fee_type")] public string CashCoinType { get; set; }
        [XmlElement("cash_fee")] public int CashTotalAmount { get; set; }
        [XmlElement("cash_refund_fee")] public int CashRefundTotalAmount { get; set; }
        [XmlIgnore] public int[] CouponType { get; set; }
        [XmlElement("coupon_refund")] public int CouponRefundAmount { get; set; }
        [XmlIgnore] public int[] SingleCouponRefundAmount { get; set; }
        [XmlElement("coupon_refund_count")] public int UseCouponCount { get; set; }
        [XmlIgnore] public int[] CouponId { get; set; }
    }
}