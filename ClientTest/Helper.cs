using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Caster.WeChat;
using Caster.WeChat.Config;
using Caster.WeChat.Impl;
using Caster.WeChat.TokenManager;

namespace ClientTest
{
    public class Helper
    {
        public static WeChatWeb CreateClient()
        {
            return new WeChatWeb(new Helper.WeChatConfig(), new HttpClientService(), null,
                new MemoryCacheDefaultManager(new MemoryCache(new MemoryCacheOptions())));
        }

        public class WeChatConfig : IOptions<ApiOption>
        {
            public ApiOption Value =>
                new ApiOption
                {
                    AppId = "wx538de1690b3f1a1f", //wxd2dc006bb81427b9
                    AppSecret = "9097fb0d8a12908aad60424e986493f4",
                    MerchantId = "1498409722",
                    MerchantSecret = "ulLf7a3JwwAIEFWuLk465R1rAHZw340e",
                    CertPath = "/Users/boshaobo/cert/1498409722_20200320_cert/apiclient_cert.p12",
                    CertPassword = "1498409722",
                };
        }
    }
}