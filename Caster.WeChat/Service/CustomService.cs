using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;
using Caster.WeChat.Parameter;

namespace Caster.WeChat.Service
{
    /// <summary>
    /// 客服
    /// </summary>
    public class CustomService
    {
        private readonly IClient _client;

        public CustomService(IClient client)
        {
            _client = client;
        }
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="content">文本</param>
        /// <param name="openId">接受消息的OpenId</param>
        /// <param name="accessToken">token</param>
        /// <returns></returns>
        public async Task SendTextMessageAsync(string content, string openId, string accessToken)
        {
            JObject root = new JObject();
            JObject contentJson = new JObject();
            contentJson["content"] = content;
            root["touser"] = openId;
            root["msgtype"] = "text";
            root["text"] = contentJson;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/custom/send"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="mediaId">素材id</param>
        /// <param name="openId">接受消息的OpenId</param>
        /// <param name="accessToken">token</param>
        /// <returns></returns>
        public async Task SendImageMessageAsync(string mediaId, string openId, string accessToken)
        {
            JObject root = new JObject();
            JObject contentJson = new JObject();
            contentJson["media_id"] = mediaId;
            root["touser"] = openId;
            root["msgtype"] = "image";
            root["image"] = contentJson;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/custom/send"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 发送视频
        /// </summary>
        /// <param name="videoMediaId">视频素材id</param>
        /// <param name="thumbMediaId">缩略图id</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="openId">接受消息的OpenId</param>
        /// <param name="accessToken">token</param>
        /// <returns></returns>
        public async Task SendVideoMessageAsync(string videoMediaId, string thumbMediaId, string title,
            string description, string openId, string accessToken)
        {
            JObject root = new JObject();
            JObject contentJson = new JObject();
            contentJson["media_id"] = videoMediaId;
            contentJson["title"] = title;
            contentJson["thumb_media_id"] = thumbMediaId;
            contentJson["description"] = description;
            root["touser"] = openId;
            root["msgtype"] = "video";
            root["video"] = contentJson;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/custom/send"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="openId">接受消息的OpenId</param>
        /// <param name="accessToken">token</param>
        /// <returns></returns>
        public async Task SendVoiceMessageAsync(string mediaId, string openId, string accessToken)
        {
            JObject root = new JObject();
            JObject contentJson = new JObject();
            contentJson["media_id"] = mediaId;
            root["touser"] = openId;
            root["msgtype"] = "voice";
            root["voice"] = contentJson;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/custom/send"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
       /// <summary>
       /// 发送音乐消息
       /// </summary>
       /// <param name="musicUrl">音乐链接</param>
       /// <param name="hqMusicUrl">高清链接</param>
       /// <param name="thumbMediaId">缩略图id</param>
       /// <param name="title">标题</param>
       /// <param name="description">描述</param>
       /// <param name="openId">接受消息的OpenId</param>
       /// <param name="accessToken">token</param>
       /// <returns></returns>
        public async Task SendMusicMessageAsync(string musicUrl, string hqMusicUrl, string thumbMediaId, string title,
            string description, string openId, string accessToken)
        {
            JObject root = new JObject();
            JObject contentJson = new JObject();
            contentJson["musicurl"] = musicUrl;
            contentJson["hqmusicurl"] = hqMusicUrl;
            contentJson["title"] = title;
            contentJson["thumb_media_id"] = thumbMediaId;
            contentJson["description"] = description;
            root["touser"] = openId;
            root["msgtype"] = "music";
            root["music"] = contentJson;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/custom/send"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="openId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task SendArticleMessageAsync(string mediaId, string openId, string accessToken)
        {
            JObject root = new JObject();
            JObject contentJson = new JObject();
            contentJson["media_id"] = mediaId;
            root["touser"] = openId;
            root["msgtype"] = "mpnews";
            root["mpnews"] = contentJson;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/custom/send"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 发送连接消息
        /// </summary>
        /// <param name="linkUrl"></param>
        /// <param name="picUrl"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="openId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task SendUrlMessageAsync(string linkUrl, string picUrl, string title, string description,
            string openId,
            string accessToken)
        {
            JObject root = new JObject();
            JObject contentJson = new JObject();
            JObject articles = new JObject();
            JArray array = new JArray {contentJson};
            articles["articles"] = array;
            contentJson["url"] = linkUrl;
            contentJson["pic_url"] = picUrl;
            contentJson["title"] = title;
            contentJson["description"] = description;
            root["touser"] = openId;
            root["msgtype"] = "news";
            root["news"] = articles;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/custom/send"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 发送菜单消息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="title"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task SendMenuMessage(string openId, string title, string remark,
            List<SendMenuMessageRequestParameter> content, string token)
        {
            JObject root = new JObject();
            JObject body = new JObject();
            JArray list = new JArray();
            root["touser"] = openId;
            root["msgtype"] = "msgmenu";
            body["head_content"] = title;
            body["tail_content"] = remark;
            foreach (var parameter in content)
            {
                var j = new JObject
                {
                    ["id"] = parameter.Id,
                    ["content"] = parameter.Content
                };
                list.Add(j);
            }

            body["list"] = list;
            root["msgmenu"] = body;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/custom/send"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", token
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 添加客服
        /// </summary>
        /// <param name="account"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task AddCustomerServiceAsync(string account, string nickname, string password, string accessToken)
        {
            JObject root = new JObject();
            root["kf_account"] = account;
            root["nickname"] = nickname;
            root["password"] = password;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/customservice/kfaccount/add"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 删除客服
        /// </summary>
        /// <param name="account"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task DeleteCustomerServiceAsync(string account, string nickname, string password,
            string accessToken)
        {
            JObject root = new JObject();
            root["kf_account"] = account;
            root["nickname"] = nickname;
            root["password"] = password;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/customservice/kfaccount/del"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 更新客服信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task UpdateCustomerServiceAsync(string account, string nickname, string password,
            string accessToken)
        {
            JObject root = new JObject();
            root["kf_account"] = account;
            root["nickname"] = nickname;
            root["password"] = password;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/customservice/kfaccount/update"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
        /// <summary>
        /// 客服列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> CustomerServiceListAsync(string accessToken)
        {
            var response = await _client.ExecuteGetRequest(
                WeChatHelper.GetWeChatApiDomain("/customservice/kfaccount/getkflist"), new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                });

            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }
        /// <summary>
        /// 更新客服头像
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="account"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task UploadCustomerServiceAvatarAsync(FileStream stream, string account, string accessToken)
        {
            var response = await _client.ExecuteUploadFileRequest(
                WeChatHelper.GetWeChatApiDomain("/customservice/kfaccount/uploadheadimg"),
                new Dictionary<string, string>
                {
                    {WeChatConstant.AccessToken, accessToken},
                    {"kf_account", account}
                }, new Dictionary<string, string>(),
                stream);
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
    }
}