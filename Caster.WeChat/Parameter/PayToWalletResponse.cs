using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class PayToWalletResponse : PayResponse
    {
        [XmlElement("err_code_des")] public override string ErrorMsg { get; set; }

        /// <summary>
        /// 商户账号appid
        /// </summary>
        [XmlElement("mch_appid")]
        public string AppId { get; set; }

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
        /// 微信支付订单号
        /// </summary>
        [XmlElement("payment_no")]
        public string WeChatPayCode { get; set; }
        
        /// <summary>
        /// 付款时间
        /// </summary>
        [XmlElement("payment_time")]
        public string PayTime { get; set; }
    }
}