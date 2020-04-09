using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Caster.WeChat.Common;

namespace Caster.WeChat.TokenManager
{
    public class MemoryCacheDefaultManager : IAccessTokenManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheDefaultManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<string> Get()
        {
            string result = _memoryCache.Get<string>(WeChatConstant.AccessToken);

            return Task.FromResult(result);
        }

        public Task Save(string token)
        {
            _memoryCache.Set(WeChatConstant.AccessToken, token, new TimeSpan(0, 0, 7100));
            return Task.CompletedTask;
        }
    }
}