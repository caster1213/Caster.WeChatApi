using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;
using Caster.WeChat.Parameter;
using Caster.WeChat.Parameter.Menu;

namespace Caster.WeChat.Service
{
    /// <summary>
    /// 模板
    /// </summary>
    public class TemplateMessageService
    {
        private readonly IClient _client;

        public TemplateMessageService(IClient httpClient)
        {
            _client = httpClient;
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="openId">用户的openId</param>
        /// <param name="templateId">模板id</param>
        /// <param name="accessToken">token</param>
        /// <param name="parameter">参数</param>
        /// <param name="url">跳转的url</param>
        /// <param name="appId">跳转的小程序appid</param>
        /// <param name="path">小程序的Path</param>
        /// <returns></returns>
        public async Task<string> SendAsync(
            string openId,
            string templateId,
            string accessToken,
            TemplateMessageParameter parameter,
            string url = null,
            string appId = null,
            string path = null)
        {
            var root = new JObject();
            var data = new JObject();
            var first = new JObject();
            var remark = new JObject();
            root["touser"] = openId;
            root["template_id"] = templateId;
            first["value"] = parameter.First.Value;
            first["color"] = parameter.First.Color;
            remark["value"] = parameter.Remark.Value;
            remark["color"] = parameter.Remark.Color;
            data["first"] = first;
            for (int i = 0; i <= parameter.Content.Count - 1; i++)
            {
                var content = new JObject
                {
                    ["value"] = parameter.Content[i].Value,
                    ["color"] = parameter.Content[i].Color
                };
                data[$"keyword{i + 1}"] = content;
            }

            data["remark"] = remark;
            root["data"] = data;
            if (string.IsNullOrEmpty(url) == false)
            {
                root["url"] = url;
            }


            if (string.IsNullOrEmpty(appId) && string.IsNullOrEmpty(path))
            {
                var mini = new JObject
                {
                    ["appid"] = appId,
                    ["pagepath"] = path
                };
                root["miniprogram"] = mini;
            }

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/template/send"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());

            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result["msgid"].ToString();
        }

        /// <summary>
        /// 删除模板消息
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string templateId, string accessToken)
        {
            var root = new JObject {["template_id"] = templateId};
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/template/del_private_template"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }

        /// <summary>
        /// 获取模板消息列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> Get(string accessToken)
        {
            var response = await _client.ExecuteGetRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/template/get_all_private_template"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                });
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result["template_list"].ToObject<dynamic>();
        }
    }
}