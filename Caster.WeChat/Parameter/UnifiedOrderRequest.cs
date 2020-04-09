using System.Xml.Serialization;

namespace Caster.WeChat.Parameter
{

    [XmlRoot("xml")]
    public class UnifiedOrderRequest:PayRequest
    {

        public UnifiedOrderRequest()
        {
            
        }
        
        /// <summary>
        /// 创建 H5Pay JsPay QrCodePay 交易
        /// </summary>
        /// <param name="nonce">随机字符串</param>
        /// <param name="body">商品描述</param>
        /// <param name="orderCode">商户订单号</param>
        /// <param name="total">标价金额</param>
        /// <param name="ip">终端IP</param>
        /// <param name="callUrl">异步通知URL</param>
        /// <param name="tradeType">交易类型</param>
        public UnifiedOrderRequest(string nonce, string body, string orderCode,
            int total, string ip, string callUrl, string tradeType = "JSAPI")
        {
            Nonce = nonce;
            Body = body;
            OrderCode = orderCode;
            Total = total;
            Ip = ip;
            CallUrl = callUrl;
            TradeType = tradeType;
        }


        [XmlElement("body")]
        public string Body { get; set; }
        [XmlElement("out_trade_no")]
        public string OrderCode { get; set; }
        [XmlElement("total_fee")]
        public int Total { get; set; }
        [XmlElement("spbill_create_ip")]
        public string Ip { get; set; }
        [XmlElement("notify_url")]
        public string CallUrl { get; set; } 
        [XmlElement("trade_type")]
        public string TradeType { get; set; }
        [XmlElement("openid")]
        public string OpenId { get; set; }
        [XmlElement("product_id")]
        public string ProductId { get; set; }
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
        public string FeeType{ get; set; }
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