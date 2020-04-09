using System.Collections.Generic;
using System.Xml.Serialization;
using Caster.WeChat.Common;

namespace Caster.WeChat.MessageHandler.Request
{
    [XmlRoot("xml")]
    public class UnifiedOrderNotificationRequest : PayNotificationRequest
    {
        [XmlElement("openid")] public string OpenId { get; set; }
        [XmlElement("is_subscribe")] public string IsSubscribe { get; set; }
        [XmlElement("trade_type")] public string TradeType { get; set; }
        [XmlElement("bank_type")] public string BankType { get; set; }
        [XmlElement("total_fee")] public int Total { get; set; }
        [XmlElement("settlement_total_fee")] public int SettlementTotal { get; set; }
        [XmlElement("fee_type")] public string CoinType { get; set; }
        [XmlElement("cash_fee")] public int CashFee { get; set; }
        [XmlElement("cash_fee_type")] public int CashType { get; set; }
        [XmlElement("coupon_fee")] public int CouponFee { get; set; }
        [XmlElement("coupon_count")] public int CouponCount { get; set; }
        [XmlIgnore] public List<Coupon> Coupons { get; set; }
        [XmlElement("transaction_id")] public string PayCode { get; set; }
        [XmlElement("out_trade_no")]  public string OrderCode { get; set; }
        [XmlElement("attach")]  public string Attach { get; set; }
        [XmlElement("time_end")] public string PayDate { get; set; }
    }
}