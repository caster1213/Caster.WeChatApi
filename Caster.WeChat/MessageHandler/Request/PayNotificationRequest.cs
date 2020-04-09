using System.Xml.Serialization;

namespace Caster.WeChat.MessageHandler.Request
{
    public class PayNotificationRequest
    {
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }
        [XmlElement("return_msg")]
        public string ReturnMsg { get; set; }
        [XmlElement("result_code")]
        public string ResultCode { get; set; }
        [XmlElement("err_code")]
        public string ErrorCode { get; set; }
        [XmlElement("err_code_des")]
        public string ErrorMsg { get; set; }
    }
}