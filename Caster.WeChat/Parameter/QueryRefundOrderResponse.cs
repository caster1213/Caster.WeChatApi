using System;
using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class QueryRefundOrderResponse : PayResponse
    {
        /// <summary>
        /// 退款总次数
        /// </summary>
        [XmlElement("total_refund_count")]
        public int RefundTotal { get; set; }

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
        /// 退款笔数
        /// </summary>
        [XmlElement("refund_count")]
        public int CurrentIndex { get; set; }

        /// <summary>
        /// 微信商户退款订单号
        /// </summary>
        [XmlIgnore]
        public string[] WeChatRefundOrderCode { get; set; }

        /// <summary>
        /// 商户内部退款订单号
        /// </summary>
        [XmlIgnore]
        public string[] RefundOrderCode { get; set; }

        /// <summary>
        /// 退款渠道
        /// </summary>
        [XmlIgnore]
        public string[] RefundChannel { get; set; }
        /// <summary>
        /// 申请退款金额
        /// </summary>
        [XmlIgnore]
        public int[] RefundAmount { get; set; }
        
        /// <summary>
        /// 退款金额
        /// </summary>
        [XmlIgnore]
        public int[] SettlementRefundAmount { get; set; }
        /// <summary>
        /// 优惠卷类型
        /// </summary>
        [XmlIgnore]
        public string[] CouponType { get; set; }
        /// <summary>
        /// 代金券金额
        /// </summary>
        [XmlIgnore]
        public int[] CouponAmount { get; set; }
        
        /// <summary>
        /// 退款代金券使用数量
        /// </summary>
        [XmlIgnore]
        public int[] CouponCount { get; set; }
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
        
        
        /// <summary>
        /// 退款状态
        /// </summary>
        [XmlIgnore]
        public string[] State { get; set; }
        
        /// <summary>
        /// 资金来源
        /// </summary>
        [XmlIgnore]
        public string[] Source { get; set; }
        
        /// <summary>
        /// 退款账户
        /// </summary>
        [XmlIgnore]
        public string[] RefundAccount { get; set; }
        
        /// <summary>
        /// 退款时间
        /// </summary>
        [XmlIgnore]
        public DateTime[] RefundDate { get; set; }
    }
}