using System;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;

namespace Caster.WeChat.Parameter
{
    [XmlRoot("xml")]
    public class QueryPayToWalletResponse:PayResponse
    {
        [XmlElement("partner_trade_no")]
        public string OrderCode { get; set; }
        [XmlElement("appid")]
        public string AppId { get; set; }
        [XmlElement("mch_id")]
        public string MchId { get; set; }
        [XmlElement("detail_id")]
        public string PayCode { get; set; }
        [XmlElement("status")]
        public string Status { get; set; }
        [XmlElement("reason")]
        public string Reason { get; set; }
        [XmlElement("openid")]
        public string OpenId { get; set; }
        [XmlElement("transfer_name")]
        public string TransferName { get; set; }
        [XmlElement("transfer_time")]
        public string TransferTime { get; set; }
        [XmlElement("payment_time")]
        public string PayTime { get; set; }
        [XmlElement("desc")]
        public string Desc { get; set; }
        [XmlElement("payment_amount")]
        public int Amount { get; set; }
    }
}