using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class SendRedPackResponse:PayResponse
    {


        /// <summary>
        /// 商户内部订单号
        /// </summary>
        [XmlElement("mch_billno")]
        public string OrderCode { get; set; }
        
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
        /// 微信单号
        /// </summary>
        [XmlElement("send_listid")]
        public string WeChatPayCode { get; set; }
    }
}