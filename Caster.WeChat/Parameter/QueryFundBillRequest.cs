using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class QueryFundBillRequest : PayRequest
    {
        /// <summary>
        /// 账单日期
        /// </summary>
        [XmlElement("bill_date")]
        public string Date { get; set; }

        /// <summary>
        /// 账户类型
        /// Basic  基本账户
        /// Operation 运营账户
        /// Fees 手续费账户
        /// </summary>
        [XmlElement("account_type")]
        public string AccountType { get; set; }
    }
}