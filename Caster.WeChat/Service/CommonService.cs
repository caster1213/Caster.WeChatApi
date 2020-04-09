using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;

namespace Caster.WeChat.Service
{
    public class CommonService
    {
        private readonly IClient _client;
        private readonly string _appId;
        private readonly string _secret;

        public CommonService(IClient client, string appId, string secret)
        {
            _client = client;
            _appId = appId;
            _secret = secret;
        }


        public async Task<string> GetAccessToken()
        {
            var str = await _client.ExecuteGetRequest("https://api.weixin.qq.com/cgi-bin/token",
                new Dictionary<string, string>
                {
                    {"appid", _appId},
                    {"secret", _secret},
                    {"grant_type", "client_credential"}
                });
            var root = JObject.Parse(str);
            WeChatHelper.CheckResponseStatus(root);
            return root["access_token"].ToString();
        }


        public async Task<string[]> GetIpAddress(string token)
        {
            string result = await _client.ExecuteGetRequest("https://api.weixin.qq.com/cgi-bin/getcallbackip",
                new Dictionary<string, string>
                {
                    {"access_token", token}
                });

            return JObject.Parse(result)["ip_list"].ToObject<string[]>();
        }
    }
}