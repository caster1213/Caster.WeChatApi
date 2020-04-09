using Caster.WeChat.Common;

namespace Caster.WeChat.Config
{
    public class ApiOption
    {
        /// <summary>
        /// 公众号id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 公众号私钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public string MerchantId { get; set; }
        
        /// <summary>
        /// 用于验证消息token
        /// </summary>
        public string ValidateToken { get; set; }

        /// <summary>
        /// 微信支付商户私钥
        /// </summary>
        public string MerchantSecret { get; set; }

        /// <summary>
        /// 是否开启消息去重
        /// </summary>
        public bool Distinct { get; set; }

        /// <summary>
        /// 消息加密模式
        /// </summary>
        public EncryptModel MessageEncryptModel { get; set; }

        /// <summary>
        /// 用于消息加密解密的key
        /// </summary>
        public string EncodingAesKey { get; set; }

        /// <summary>
        /// CA证书路径
        /// </summary>
        public string CertPath { get; set; }
        /// <summary>
        /// CA证书密码
        /// </summary>
        public string CertPassword { get; set; }
        
    }
}