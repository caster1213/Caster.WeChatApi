using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class QueryOrderResponse:PayResponse
    {
        /// <summary>
        /// 设备号
        /// </summary>
        [XmlElement("device_info")]
        public string DriverInfo { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        [XmlElement("is_subscribe")]
        public string Subscribe { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [XmlElement("trade_type")]
        public string TradeType { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        [XmlElement("trade_state")]
        public string TradeState { get; set; }
        
        /// <summary>
        /// 付款银行
        /// </summary>
        [XmlElement("bank_type")]
        public string BankType { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        [XmlElement("total_fee")]
        public int TotalAmount { get; set; }
        /// <summary>
        /// 应结订单金额
        /// </summary>
        [XmlElement("settlement_total_fee")]
        public int SettlementTotalAmount { get; set; }
        
        /// <summary>
        /// 货币种类
        /// </summary>
        [XmlElement("fee_type")]
        public string CoinType { get; set; }
        
        /// <summary>
        /// 现金支付金额
        /// </summary>
        [XmlElement("cash_fee")]
        public int CashAmount { get; set; }
        
        /// <summary>
        /// 现金支付币种
        /// </summary>
        [XmlElement("cash_fee_type")]
        public int CashType { get; set; }
        
        /// <summary>
        /// 代金券金额
        /// </summary>
        [XmlElement("coupon_fee")]
        public int CouponAmount { get; set; }
        
        /// <summary>
        /// 代金券使用数量
        /// </summary>
        [XmlElement("coupon_count")]
        public int UseCouponCount { get; set; }
        
        
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
        /// 附加数据
        /// </summary>
        [XmlElement("attach")]
        public string Attach { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        [XmlElement("time_end")]
        public string PayCompleteTime { get; set; }
        
        
        /// <summary>
        /// 交易状态描述
        /// </summary>
        [XmlElement("trade_state_desc")]
        public string TradeStateDesc { get; set; }
        
        
        /// <summary>
        /// 优惠卷类型
        /// </summary>
        [XmlIgnore]
        public string[] CouponType { get; set; }
        
        /// <summary>
        /// 代金券id
        /// </summary>
        [XmlIgnore]
        public string[] CouponId { get; set; }
        
        /// <summary>
        /// 单个代金券金额
        /// </summary>
        [XmlIgnore]
        public int[] SingleCouponAmount { get; set; }
    }
}