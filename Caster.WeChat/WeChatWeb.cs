using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Caster.WeChat.Common;
using Caster.WeChat.TokenManager;
using Caster.WeChat.Config;
using Caster.WeChat.MessageHandler;
using Caster.WeChat.Parameter;
using Caster.WeChat.Service;

namespace Caster.WeChat
{
    /// <summary>
    /// 微信web接口
    /// </summary>
    public sealed class WeChatWeb
    {
        private readonly IClient _client;
        private readonly ICache _cache;
        private readonly ApiOption _config;
        private readonly IAccessTokenManager _accessTokenManager;


        /// <summary>
        /// 创建Web接口
        /// </summary>
        /// <param name="options">配置参数</param>
        /// <param name="client">HttpClient接口</param>
        /// <param name="cache">缓存接口</param>
        /// <param name="accessTokenManager">Token存储接口</param>
        public WeChatWeb(IOptions<ApiOption> options,
            IClient client,
            ICache cache,
            IAccessTokenManager accessTokenManager)
        {
            _config = options.Value;
            _client = client;
            _cache = cache;
            _accessTokenManager = accessTokenManager;
            InitializeWeChatService();
        }

        /// <summary>
        /// 初始化接口服务
        /// </summary>
        private void InitializeWeChatService()
        {
            CommonService = new CommonService(_client, _config.AppId, _config.AppSecret);
            CommentService = new CommentService(_client);
            CustomService = new CustomService(_client);
            FileService = new FileService(_client);
            MenuService = new MenuService(_client);
            MessageAllService = new MessageAllService(_client);
            TemplateMessageService = new TemplateMessageService(_client);
            UserService = new UserService(_client);
            PayService = new PayService(_client, _config);
        }

        /// <summary>
        /// 评论接口
        /// </summary>
        public CommentService CommentService { get; private set; }

        /// <summary>
        /// 基础接口
        /// </summary>
        public CommonService CommonService { get; private set; }

        /// <summary>
        /// 客服消息接口
        /// </summary>
        public CustomService CustomService { get; private set; }

        /// <summary>
        /// 素材接口
        /// </summary>
        public FileService FileService { get; private set; }

        /// <summary>
        /// 菜单接口
        /// </summary>
        public MenuService MenuService { get; private set; }

        /// <summary>
        /// 群发接口
        /// </summary>
        public MessageAllService MessageAllService { get; private set; }

        /// <summary>
        /// 模板消息接口
        /// </summary>
        public TemplateMessageService TemplateMessageService { get; private set; }

        /// <summary>
        /// 用户接口
        /// </summary>
        public UserService UserService { get; private set; }

        /// <summary>
        /// 支付接口
        /// </summary>
        public PayService PayService { get; private set; }

        public async Task<string> GetToken()
        {
            string token = await _accessTokenManager.Get();

            if (string.IsNullOrEmpty(token))
            {
                token = await RefreshToken();
            }

            return token;
        }


        private async Task<string> RefreshToken()
        {
            string accessToken = await CommonService.GetAccessToken();
            await _accessTokenManager.Save(accessToken);
            return accessToken;
        }

        /// <summary>
        /// 获取一个微信支付处理程序
        /// </summary>
        /// <returns>消息Handler</returns>
        public PayNotificationHandler GetPayNotificationHandler()
        {
            return new PayNotificationHandler(_config.MerchantId, _config.MerchantSecret);
        }

        /// <summary>
        /// 回去一个微信公众号消息处理程序
        /// </summary>
        /// <returns>消息Handler</returns>
        public WeChatMessageHandler GetWeChatMessageHandler()
        {
            return new WeChatMessageHandler(_config, _cache);
        }
    }
}