using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class SendRedPackRequest
    {
        /// <summary>
        /// 公众号的appId
        /// </summary>
        [XmlElement("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        [XmlElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [XmlElement("nonce_str")]
        public string Nonce { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 商户内部订单号
        /// </summary>
        [XmlElement("mch_billno")]
        public string OrderCode { get; set; }

        /// <summary>
        /// 发送者名称
        /// </summary>
        [XmlElement("send_name")]
        public string SendName { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        [XmlElement("re_openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 发送金额
        /// </summary>
        [XmlElement("total_amount")]
        public int TotalAmount { get; set; }

        /// <summary>
        /// 发送总人数
        /// </summary>
        [XmlElement("total_num")]
        public int SendPeopleCount { get; set; }

        /// <summary>
        /// 祝福语
        /// </summary>
        [XmlElement("wishing")]
        public string Wishing { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        [XmlElement("client_ip")]
        public string Ip { get; set; }


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
        /// 场景id
        /// </summary>
        [XmlElement("scene_id")]
        public string SceneId { get; set; }

        /// <summary>
        /// 活动信息
        /// </summary>
        [XmlElement("risk_info")]
        public string RiskInfo { get; set; }
    }
}