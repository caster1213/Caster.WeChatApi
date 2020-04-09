using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class PayToBankRequest
    {
        [XmlElement("mch_id")]
        public string MchId { get; set; }
        [XmlElement("partner_trade_no")]
        public string OrderCode { get; set; }
        [XmlElement("nonce_str")]
        public string Nonce { get; set; }
        [XmlElement("enc_bank_no")]
        public string BankCode { get; set; }
        [XmlElement("bank_code")]
        public string BankName { get; set; }
        [XmlElement("enc_true_name")]
        public string AccountName { get; set; }
        [XmlElement("amount")]
        public int Amount { get; set; }
        [XmlElement("desc")]
        public string Desc { get; set; }
        [XmlElement("sign")]
        public string Sign { get; set; }
    }
}