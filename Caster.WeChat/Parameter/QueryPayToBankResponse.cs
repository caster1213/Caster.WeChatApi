using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class QueryPayToBankResponse : PayResponse
    {
        [XmlElement("mch_id")] public string MchId { get; set; }
        [XmlElement("partner_trade_no")] public string OrderCode { get; set; }
        [XmlElement("payment_no")] public string PayCode { get; set; }
        [XmlElement("bank_no_md5")] public string BankCode { get; set; }
        [XmlElement("true_name_md5")] public string AccountName { get; set; }
        [XmlElement("amount")] public int Amount { get; set; }
        [XmlElement("status")] public string Status { get; set; }
        [XmlElement("cmms_amt")] public int Fee { get; set; }
        [XmlElement("create_time")] public string CreateDate { get; set; }
        [XmlElement("pay_succ_time")] public string SuccessDate { get; set; }
        [XmlElement("reason")] public string Reason { get; set; }
    }
}