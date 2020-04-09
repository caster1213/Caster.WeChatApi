using System;
using Microsoft.Extensions.DependencyInjection;
using Caster.WeChat.Common;
using Caster.WeChat.Config;
using Caster.WeChat.Impl;
using Caster.WeChat.TokenManager;

namespace Caster.WeChat
{
    public static class WeChatServiceCollectionExtensions
    {
        /// <summary>
        /// 添加一个默认的Api服务
        /// </summary>
        /// <param name="service"></param>
        /// <param name="action"></param>
        public static void AddWeChatWeb(this IServiceCollection service, Action<ApiOption> action)
        {
            service.AddTransient<IAccessTokenManager, MemoryCacheDefaultManager>();
            service.AddSingleton<IClient, HttpClientService>();
            service.AddTransient<WeChatWeb>();
            service.Configure(action);
        }

        /// <summary>
        /// 添加一个自定义Token存储的Api服务
        /// </summary>
        /// <param name="service"></param>
        /// <typeparam name="TToken">Token存储实现</typeparam>
        public static void AddWeChatWeb<TToken>(this IServiceCollection service)
            where TToken : class, IAccessTokenManager
        {
            service.AddTransient<IAccessTokenManager, TToken>();
            service.AddSingleton<IClient, HttpClientService>();
            service.AddTransient<WeChatWeb>();
        }

        /// <summary>
        /// 添加一个自定义Token存储和自定义缓存的Api服务
        /// </summary>
        /// <param name="service"></param>
        /// <typeparam name="TToken">Token存储实现</typeparam>
        /// <typeparam name="TCache">Cache实现</typeparam>
        public static void AddWeChatWeb<TToken, TCache>(this IServiceCollection service)
            where TToken : class, IAccessTokenManager
            where TCache : class, ICache
        {
            service.AddTransient<IAccessTokenManager, TToken>();
            service.AddSingleton<ICache, TCache>();
            service.AddSingleton<IClient, HttpClientService>();
            service.AddTransient<WeChatWeb>();
        }
    }
}