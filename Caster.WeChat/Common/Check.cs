using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Caster.WeChat.ServiceException;

namespace Caster.WeChat.Common
{
    public class Check
    {
        /// <summary>
        /// 解密微信发送的消息数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        /// <exception cref="WeChatCryptographyException"></exception>
        public static string WeChatMessageDecrypt(string value, string key, string appId)
        {
            try
            {
                (string message, string sendAppId) = MessageCryptography.AesDecrypt(value, key);

                if (sendAppId != appId)
                {
                    throw new WeChatCryptographyException("接收到的AppId与配置中的AppId不相符");
                }

                return message;
            }
            catch (Exception)
            {
                throw new WeChatCryptographyException("解密数据出现错误，请核对秘钥是否正确");
            }
        }


        /// <summary>
        /// 验证微信消息签名是否正确
        /// </summary>
        /// <param name="securityKey">微信后台填写的token</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="value">加密的消息</param>
        /// <param name="sign">发送的签名</param>
        /// <exception cref="WeChatSignException"></exception>
        public static void WeChatMessageSignCheck(string securityKey, string nonce, string timestamp, string value,
            string sign)
        {
            var s = GetWeChatMessageSign(securityKey, nonce, timestamp, value);
            if (s != sign.ToLower())
            {
                throw new WeChatSignException("签名验证失败");
            }
        }

        public static bool PaySignCheck(XmlDocument document,string key)
        {
           var sign = WeChatSignHelper.CreateMd5SignByXml(document, key);

           var requestSign = document.FirstChild.SelectSingleNode("sign").InnerText;

           return sign == requestSign;
        }

        /// <summary>
        /// 获取消息签名
        /// </summary>
        /// <param name="securityKey">私钥</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="value">内容</param>
        /// <returns></returns>
        public static string GetWeChatMessageSign(string securityKey, string nonce, string timestamp, string value)
        {
            var dictionary = new SortedDictionary<string, string>
            {
                {securityKey, string.Empty},
                {nonce, string.Empty},
                {timestamp, string.Empty},
                {value, string.Empty}
            };

            StringBuilder data = new StringBuilder();

            foreach (var str in dictionary)
            {
                data.Append(str.Key);
            }

            var sha1 = new SHA1CryptoServiceProvider();
            var buffer = sha1.ComputeHash(Encoding.ASCII.GetBytes(data.ToString()));

            return BitConverter.ToString(buffer).Replace("-", "").ToLower();
        }
    }
}