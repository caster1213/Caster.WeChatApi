using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Caster.WeChat.Common;
using Caster.WeChat.Config;
using Caster.WeChat.Parameter;
using Caster.WeChat.ServiceException;

namespace Caster.WeChat.Service
{
    /// <summary>
    /// 支付
    /// </summary>
    public class PayService
    {
        private readonly IClient _client;
        private readonly string _secretKey;
        private readonly string _appId;
        private readonly string _mchId;
        private readonly string _path;
        private readonly string _password;

        public PayService(IClient client, ApiOption option)
        {
            _client = client;
            _appId = option.AppId;
            _mchId = option.MerchantId;
            _secretKey = option.MerchantSecret;
            _path = option.CertPath;
            _password = option.CertPassword;
        }


        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">传入的参数是null</exception>
        /// <exception cref="WeChatPayException">调用微信接口失败时返回的错误信息</exception>
        public async Task<UnifiedOrderResponse> UnifiedOrderAsync(UnifiedOrderRequest parameter)
        {
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            if (parameter.TradeType == WeChatConstant.JsPay && string.IsNullOrEmpty(parameter.OpenId))
                throw new WeChatPayException("支付方式JsPay OpenId 必须传入");
            if (parameter.TradeType == WeChatConstant.NativePay && string.IsNullOrEmpty(parameter.ProductId))
                throw new WeChatPayException("支付方式NativePay ProductId 必须传入");
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");

            var response = XmlSerializeHelper.StringToObject<UnifiedOrderResponse>(result);
            Check(response);
            return response;
        }


        public async Task<ScanPayOrderResponse> ScanPayOrderAsync(ScanPayOrderRequest parameter)
        {
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);

            string url = "https://api.mch.weixin.qq.com/pay/micropay";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");

            var response = XmlSerializeHelper.StringToObject<ScanPayOrderResponse>(result);
            Check(response);
            return response;
        }


        public async Task<CancelScanOrderResponse> CancelScanOrderAsync(CancelScanOrderRequest parameter)
        {
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");

            var response = XmlSerializeHelper.StringToObject<CancelScanOrderResponse>(result);

            Check(response);

            return response;
        }


        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">传入的参数是null</exception>
        /// <exception cref="WeChatPayException">调用微信接口失败时返回的错误信息</exception>
        public async Task<PayResponse> CloseOrderAsync(CloseOrderRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/pay/closeorder";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");

            var response = XmlSerializeHelper.StringToObject<PayResponse>(result);

            Check(response);

            return response;
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">传入的参数是null</exception>
        /// <exception cref="WeChatPayException">调用微信接口失败时返回的错误信息</exception>
        public async Task<RefundOrderResponse> RefundOrder(RefundOrderRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            parameter.Nonce = Helper.GetNonceStr(32);
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/pay/closeorder";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body,_path,_password, "xml/text");

            var response = XmlSerializeHelper.StringToObject<RefundOrderResponse>(result);

            Check(response);

            return response;
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">传入的参数是null</exception>
        /// <exception cref="WeChatPayException">调用微信接口失败时返回的错误信息</exception>
        public async Task<QueryOrderResponse> GetOrderAsync(QueryOrderRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);

            var response = XmlSerializeHelper.StringToObject<QueryOrderResponse>(result);
            Check(response);
            response.CouponType = Helper.FormatXmlField("coupon_type", xml);
            response.CouponId = Helper.FormatXmlField("coupon_refund_id", xml);
            response.SingleCouponAmount = Helper.FormatXmlField("coupon_refund_fee", xml)
                .Select(x => Convert.ToInt32(x)).ToArray();


            return response;
        }

        /// <summary>
        /// 查询退款订单
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <exception cref="ArgumentNullException">传入的参数是null</exception>
        /// <exception cref="WeChatPayException">调用微信接口失败时返回的错误信息</exception>
        public async Task<QueryRefundOrderResponse> GetRefundOrders(QueryRefundOrderRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/pay/refundquery";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");
            XmlDocument xml = new XmlDocument();
            xml.Load(result);
            var response = XmlSerializeHelper.StringToObject<QueryRefundOrderResponse>(result);
            Check(response);
            response.WeChatRefundOrderCode = Helper.FormatXmlField("refund_id", xml);
            response.RefundOrderCode = Helper.FormatXmlField("out_refund_no", xml);
            response.RefundChannel = Helper.FormatXmlField("refund_channel", xml);
            response.RefundAmount = Helper.FormatXmlField("	refund_fee", xml)
                .Select(x => Convert.ToInt32(x))
                .ToArray();
            response.SettlementRefundAmount = Helper.FormatXmlField("settlement_refund_fee", xml)
                .Select(x => Convert.ToInt32(x))
                .ToArray();
            response.CouponType = Helper.FormatXmlField("coupon_type", xml);
            response.CouponAmount = Helper.FormatXmlField("coupon_refund_fee", xml)
                .Select(x => Convert.ToInt32(x))
                .ToArray();
            response.CouponCount = Helper.FormatXmlField("coupon_refund_count", xml)
                .Select(x => Convert.ToInt32(x))
                .ToArray();
            response.CouponId = Helper.FormatXmlField("coupon_refund_id", xml);
            response.SingleCouponAmount = Helper.FormatXmlField("coupon_refund_fee", xml)
                .Select(x => Convert.ToInt32(x))
                .ToArray();
            response.State = Helper.FormatXmlField("refund_status", xml);
            response.Source = Helper.FormatXmlField("refund_account", xml);
            response.RefundAccount = Helper.FormatXmlField("refund_recv_accout", xml);
            response.RefundDate = Helper.FormatXmlField("refund_success_time", xml)
                .Select(Convert.ToDateTime)
                .ToArray();
            return response;
        }

        /// <summary>
        /// 获取对账单
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">传入的参数是null</exception>
        /// <exception cref="WeChatPayException">调用微信接口失败时返回的错误信息</exception>
        public async Task<OrderTable> GetOrderBillAsync(QueryOrderBillRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/pay/downloadbill";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");

            if (result.Contains("return_code"))
            {
                var xml = new XmlDocument();
                xml.Load(result);
                var nodes = xml.FirstChild;
                throw new WeChatApiException(nodes["error_code"].InnerText,
                    nodes["return_msg"].InnerText, "下载对账单");
            }

            return new OrderTable(result);
        }

        /// <summary>
        /// 获取资金对账单
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">传入的参数是null</exception>
        /// <exception cref="WeChatPayException">调用微信接口失败时返回的错误信息</exception>
        public async Task<OrderTable> GetFundBillAsync(QueryFundBillRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/pay/downloadfundflow";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");
            if (result.Contains("error_code"))
            {
                var xml = new XmlDocument();
                xml.Load(result);
                var nodes = xml.FirstChild;
                throw new WeChatApiException(nodes["error_code"].InnerText,
                    nodes["return_msg"].InnerText, "下载对账单");
            }

            return new OrderTable(result);
        }

        /// <summary>
        /// 发送红包
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<SendRedPackResponse> SendRedPack(SendRedPackRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");

            var response = XmlSerializeHelper.StringToObject<SendRedPackResponse>(result);

            Check(response);

            return response;
        }

        /// <summary>
        /// 发送裂变红包
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<SendRedPackResponse> SendFissionRedPack(SendFissionRedPackRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendgroupredpack";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");

            var response = XmlSerializeHelper.StringToObject<SendRedPackResponse>(result);

            Check(response);

            return response;
        }


        /// <summary>
        /// 获取红包发送记录
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<QuerySendRedPackResponse> GetSendRedPackHistory(
            QuerySendRedPackRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);

            string url = "https://api.mch.weixin.qq.com/pay/closeorder";
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, "xml/text");

            var response = XmlSerializeHelper.StringToObject<QuerySendRedPackResponse>(result);

            Check(response);

            return response;
        }

        /// <summary>
        /// 企业付款到零钱
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<PayToWalletResponse> PayToWalletAsync(PayToWalletRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_path))
                throw new InvalidOperationException("证书的路径和密码没有填写");
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);

            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, _path, _password, "xml/text");

            var response = XmlSerializeHelper.StringToObject<PayToWalletResponse>(result);
            Check(response);
            return response;
        }


        /// <summary>
        /// 获取企业付款到零钱支付结果
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<QueryPayToWalletResponse> GetPayToWalletResultAsync(QueryPayToWalletRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";
            parameter.AppId = _appId;
            parameter.MchId = _mchId;
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);

            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, _path, _password, "xml/text");

            var response = XmlSerializeHelper.StringToObject<QueryPayToWalletResponse>(result);
            Check(response);
            return response;
        }


        /// <summary>
        /// 企业付款到银行卡
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<PayToBankResponse> PayToBankAsync(PayToBankRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            string url = "https://api.mch.weixin.qq.com/mmpaysptrans/pay_bank";
            parameter.MchId = _mchId;
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);
            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, _path, _password, "xml/text");

            var response = XmlSerializeHelper.StringToObject<PayToBankResponse>(result);
            Check(response);
            return response;
        }


        /// <summary>
        /// 获取企业付款到银行卡支付结果
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<QueryPayToBankResponse> GetPayToBankResultAsync(QueryPayToBankRequest parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            string url = "https://api.mch.weixin.qq.com/mmpaysptrans/query_bank";
            parameter.MchId = _mchId;
            parameter.Sign = WeChatSignHelper.CreateMd5Sign(parameter, _secretKey);
            string body = XmlSerializeHelper.ObjectToXmlString(parameter);

            string result = await _client.ExecutePostRequest(url, new Dictionary<string, string>(),
                body, _path, _password, "xml/text");

            var response = XmlSerializeHelper.StringToObject<QueryPayToBankResponse>(result);
            Check(response);
            return response;
        }

        public async Task<string> GetPublicKeyAsync()
        {
            string url = "https://fraud.mch.weixin.qq.com/risk/getpublickey";
            XmlDocument xmlDocument = new XmlDocument();

            var root = xmlDocument.CreateElement("xml");
            var idElement = xmlDocument.CreateElement("mch_id");
            idElement.InnerText = _mchId;
            var nonceElement = xmlDocument.CreateElement("nonce_str");
            nonceElement.InnerText = Helper.GetNonceStr(32);
            var signTypeElement = xmlDocument.CreateElement("sign_type");
            signTypeElement.InnerText = "MD5";
            root.AppendChild(idElement);
            root.AppendChild(nonceElement);
            root.AppendChild(signTypeElement);
            xmlDocument.AppendChild(root);

            string sign = WeChatSignHelper.CreateMd5SignByXml(xmlDocument, _secretKey);

            var signElement = xmlDocument.CreateElement("sign");
            signElement.InnerText = sign;
            root.AppendChild(signElement);
            string body = XmlSerializeHelper.XmlToString(xmlDocument);

            var result =
                await _client.ExecutePostRequest(url, new Dictionary<string, string>(), body, _path, _password,
                    "xml/text");

            Check(XmlSerializeHelper.StringToObject<PayResponse>(result));

            XmlDocument res = new XmlDocument();

            res.LoadXml(result);


            return res.FirstChild.SelectSingleNode("pub_key").InnerText;
        }


        private static void Check(PayResponse response)
        {
            if (response.ReturnCode != WeChatConstant.PaySuccess)
                throw new WeChatApiException(response.ReturnCode, response.ReturnMsg, string.Empty);
            if (response.ResultCode != WeChatConstant.PaySuccess)
                throw new WeChatApiException(response.ErrorCode, response.ErrorMsg, string.Empty);
        }
    }
}