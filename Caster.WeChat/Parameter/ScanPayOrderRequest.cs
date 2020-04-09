using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{
    public class ScanPayOrderRequest:PayRequest
    {
        /// <summary>
        /// 商品描述
        /// </summary>
        [XmlElement("body")]
        public string Body { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [XmlElement("out_trade_no")]
        public string OrderCode { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        [XmlElement("total_fee")]
        public int Total { get; set; }
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        [XmlElement("spbill_create_ip")]
        public string Ip { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        [XmlElement("device_info")]
        public string DriverInfo { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        [XmlElement("detail")]
        public string Detail { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        [XmlElement("attach")]
        public string Attach { get; set; }
        /// <summary>
        /// 货币类型
        /// </summary>
        [XmlElement("fee_type")]
        public string FeeType => "CNY";
        /// <summary>
        /// 交易起始时间
        /// </summary>
        [XmlElement("time_start")]
        public string TimeStart { get; set; }
        /// <summary>
        /// 交易结束时间
        /// </summary>
        [XmlElement("time_expire")]
        public string TimeEnd { get; set; }
        /// <summary>
        /// 订单优惠标记
        /// </summary>
        [XmlElement("goods_tag")]
        public string Tag { get; set; }
        /// <summary>
        /// 指定支付方式
        /// </summary>
        [XmlElement("limit_pay")]
        public string LimitPay { get; set; }
        /// <summary>
        /// 电子发票入口开放标识
        /// </summary>
        [XmlElement("receipt")]
        public string Receipt { get; set; }
        /// <summary>
        /// 场景信息
        /// </summary>
        [XmlElement("scene_info")]
        public string SceneInfo { get; set; }
        /// <summary>
        /// 授权码
        /// </summary>
        [XmlElement("auth_code")]
        public string ScanCode { get; set; }
    }
}