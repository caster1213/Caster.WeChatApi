using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class PayToBankResponse:PayResponse
    {
        [XmlElement("payment_no")]
        public string PayCode { get; set; }
        [XmlElement("cmms_amt")]
        public int Fee { get; set; }
    }
}