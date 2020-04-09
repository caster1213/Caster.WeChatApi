using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class QuerySendRedPackRequest
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
        /// 订单类型
        /// </summary>
        [XmlElement("bill_type")]
        public string OrderType => "MCHT";    
    }
}