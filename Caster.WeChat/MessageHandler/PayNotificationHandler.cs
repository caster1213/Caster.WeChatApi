using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Caster.WeChat.Common;
using Caster.WeChat.MessageHandler.Handler;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;
using Caster.WeChat.ServiceException;

namespace Caster.WeChat.MessageHandler
{
    public class PayNotificationHandler
    {
        private readonly string _securityKey;
        private readonly string _mchId;

        private readonly List<object> _handlers;

        public PayNotificationHandler(string mchId, string securityKey)
        {
            _mchId = mchId;
            _securityKey = securityKey;
            _handlers = new List<object>();
        }


        public PayNotificationHandler AddHandler<TRequest>(IPayNotificationHandler<TRequest> handler)
            where TRequest : PayNotificationRequest
        {
            _handlers.Add(handler);
            return this;
        }

        /// <summary>
        /// 支付异步通知处理
        /// </summary>
        /// <param name="input">微信发送的数据</param>
        /// <returns></returns>
        /// <exception cref="WeChatPayException"></exception>
        public async Task<string> ExecutedAsync(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            input.Position = 0;
            input.Read(buffer);
            var xmlStr = Encoding.UTF8.GetString(buffer);

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xmlStr);

            var node = xmlDocument.FirstChild;
            PayResponse response = null;

            if (node.SelectSingleNode("return_code").InnerText == WeChatConstant.PaySuccess)
            {
                foreach (var handler in _handlers)
                {
                    if (handler is IRefundNotificationHandler refundHandler &&
                        node.SelectSingleNode("req_info") != null)
                    {
                        var encrypt = node.SelectSingleNode("req_info").InnerText;
                        var mchId = node.SelectSingleNode("mch_id").InnerText;
                        var appId = node.SelectSingleNode("appid").InnerText;
                        var nonce = node.SelectSingleNode("nonce_str").InnerText;
                        var notification = RefundParameterCheck(encrypt, mchId, appId, nonce);
                        response = await refundHandler.SuccessExecuted(notification);
                    }

                    if (handler is IUnifiedOrderNotificationHandler unifiedHandler &&
                        node.SelectSingleNode("result_code").InnerText == WeChatConstant.PaySuccess)
                    {
                        if (Check.PaySignCheck(xmlDocument, _securityKey) == false)
                        {
                            throw new WeChatPayException("签名验证失败");
                        }

                        var notification =
                            XmlSerializeHelper.StringToObject<UnifiedOrderNotificationRequest>(xmlStr);

                        response = await unifiedHandler.SuccessExecuted(notification);
                    }
                }
            }
            else
            {
                string errorCode = node.SelectSingleNode("err_code").InnerText;
                string errorMsg = node.SelectSingleNode("err_code_des").InnerText;
                return Failed(errorCode, errorMsg);
            }

            if (response == null)
            {
                throw new WeChatPayException("未找到可用的Handler");
            }

            return Succeed();
        }

        private string Failed(string code, string msg)
        {
            StringBuilder error = new StringBuilder();
            error.Append("<xml>");
            error.Append($"<return_code><![CDATA[{code}]]></return_code>");
            error.Append($"<return_msg><![CDATA[{msg}]]></return_msg>");
            error.Append("</xml>");
            return error.ToString();
        }

        private string Succeed()
        {
            return "<xml></xml>";
        }

        private RefundNotificationRequest RefundParameterCheck(string input, string mchId, string appId, string nonce)
        {
            using (MD5 md5 = MD5.Create())
            {
                var base64 = Convert.FromBase64String(input);
                StringBuilder output = new StringBuilder();
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(_securityKey));
                foreach (var b in data)
                {
                    output.Append(b.ToString("x2"));
                }

                string key = output.ToString();
                byte[] keyBuffer = Encoding.UTF8.GetBytes(key);
                byte[] encryptBuffer = base64;
                RijndaelManaged rDel = new RijndaelManaged
                {
                    Key = keyBuffer, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultBuffer =
                    cTransform.TransformFinalBlock(encryptBuffer, 0, encryptBuffer.Length);
                string xml = Encoding.UTF8.GetString(resultBuffer);
                RefundNotificationRequest notification =
                    XmlSerializeHelper.StringToObject<RefundNotificationRequest>(xml);
                notification.MchId = mchId;
                notification.AppId = appId;
                notification.Nonce = nonce;
                return notification;
            }
        }
    }
}