using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class PayToWalletRequest
    {
        /// <summary>
        /// 商户账号appid
        /// </summary>
        [XmlElement("mch_appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [XmlElement("mchid")]
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
        /// 设备号
        /// </summary>
        [XmlElement("device_info")]
        public string DriverInfo { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [XmlElement("partner_trade_no")]
        public string OrderCode { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 校验用户姓名选项
        /// NO_CHECK：不校验真实姓名
        /// FORCE_CHECK：强校验真实姓名
        /// </summary>
        [XmlElement("check_name")]
        public string CheckType { get; set; }

        /// <summary>
        /// 汇款人姓名
        /// </summary>
        [XmlElement("re_user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [XmlElement("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("desc")]
        public string Remark { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        [XmlElement("spbill_create_ip")]
        public string Ip { get; set; }
    }
}