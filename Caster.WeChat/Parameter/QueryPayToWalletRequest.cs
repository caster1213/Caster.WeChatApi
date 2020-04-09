using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class QueryPayToWalletRequest
    {
        [XmlElement("nonce_str")] public string Nonce { get; set; }
        [XmlElement("partner_trade_no")] public string OrderCode { get; set; }
        [XmlElement("sign")] public string Sign { get; set; }
        [XmlElement("mch_id")] public string MchId { get; set; }
        [XmlElement("appid")] public string AppId { get; set; }
    }
}