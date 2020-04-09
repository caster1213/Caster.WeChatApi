using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class QueryPayToBankRequest
    {
        /// <summary>
        /// 商户号
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
        /// 商户订单号
        /// </summary>
        [XmlElement("partner_trade_no")]
        public string OrderCode { get; set; }
    }
}