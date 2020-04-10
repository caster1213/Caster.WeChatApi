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
                    AppId = "", 
                    AppSecret = "",
                    MerchantId = "",
                    MerchantSecret = "",
                    CertPath = "",
                    CertPassword = "",
                };
        }
    }
}