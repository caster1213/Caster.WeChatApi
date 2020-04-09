using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class QuerySendRedPackResponse : PayResponse
    {

        /// <summary>
        /// 商户内部订单号
        /// </summary>
        [XmlElement("mch_billno")]
        public string OrderCode { get; set; }

        /// <summary>
        /// 微信单号
        /// </summary>
        [XmlElement("detail_id")]
        public string WeChatPayCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("status")]
        public string State { get; set; }

        /// <summary>
        /// 发放类型
        /// </summary>
        [XmlElement("send_type")]
        public string SendType { get; set; }

        /// <summary>
        /// 红包类型
        /// </summary>
        [XmlElement("hb_type")]
        public string RedPackType { get; set; }

        /// <summary>
        /// 红包个数
        /// </summary>
        [XmlElement("total_num")]
        public int Count { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        [XmlElement("total_amount")]
        public int TotalAmount { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        [XmlElement("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [XmlElement("send_time")]
        public string SendTime { get; set; }

        /// <summary>
        /// 退款时间
        /// </summary>
        [XmlElement("refund_time")]
        public string RefundTime { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        [XmlElement("refund_amount")]
        public int RefundAmount { get; set; }
        
        /// <summary>
        /// 活动名称
        /// </summary>
        [XmlElement("act_name")]
        public string ActivityName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("remark")]
        public string Remark { get; set; }
        /// <summary>
        /// 祝福语
        /// </summary>
        [XmlElement("wishing")]
        public string Wishing { get; set; }
        /// <summary>
        /// openid
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }
        
        /// <summary>
        /// 领取金额
        /// </summary>
        [XmlElement("amount")]
        public int Amount { get; set; }
        
        /// <summary>
        /// 接受时间
        /// </summary>
        [XmlElement("rcv_time")]
        public string ReceiveTime { get; set; }
    }
}