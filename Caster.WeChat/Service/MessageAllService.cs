using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;

namespace Caster.WeChat.Service
{
    /// <summary>
    /// 消息群发服务
    /// </summary>
    public class MessageAllService
    {
        private readonly IClient _client;

        public MessageAllService(IClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 通过标签群发文本消息
        /// </summary>
        /// <param name="all"></param>
        /// <param name="tagId"></param>
        /// <param name="content"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> SendTextMessageByLabelAsync(bool all, string tagId, string content,
            string accessToken)
        {
            JObject root = new JObject();
            JObject filter = new JObject();
            JObject text = new JObject();
            filter["is_to_all"] = all;
            filter["tag_id"] = tagId;
            text["content"] = content;
            root["filter"] = filter;
            root["text"] = text;
            root["msgtype"] = "text";

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/sendall"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过标签群发文章
        /// </summary>
        /// <param name="all">是否群发</param>
        /// <param name="tagId"></param>
        /// <param name="mediaId">文档id</param>
        /// <param name="accessToken">token</param>
        /// <param name="reprint">被转载是是否继续群发</param>
        /// <returns></returns>
        public async Task<dynamic> SendArticleMessageByLabelAsync(bool all, string tagId, string mediaId,
            string accessToken,
            int reprint = 0)
        {
            JObject root = new JObject();
            JObject filter = new JObject();
            JObject media = new JObject();
            filter["is_to_all"] = all;
            filter["tag_id"] = tagId;
            media["media_id"] = mediaId;
            root["filter"] = filter;
            root["mpnews"] = media;
            root["msgtype"] = "mpnews";
            root["send_ignore_reprint"] = reprint;

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/sendall"),
                new Dictionary<string, string>
                {
                    {
                        accessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过标签群发音频
        /// </summary>
        /// <param name="all"></param>
        /// <param name="tagId"></param>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> SendVoiceMessageByLabelAsync(bool all, string tagId, string mediaId,
            string accessToken)
        {
            JObject root = new JObject();
            JObject filter = new JObject();
            JObject voice = new JObject();
            filter["is_to_all"] = all;
            filter["tag_id"] = tagId;
            voice["media_id"] = mediaId;
            root["filter"] = filter;
            root["voice"] = voice;
            root["msgtype"] = "voice";

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/sendall"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过标签群发图片
        /// </summary>
        /// <param name="all"></param>
        /// <param name="tagId"></param>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> SendImageMessageByLabelAsync(bool all, string tagId, string mediaId,
            string accessToken)
        {
            JObject root = new JObject();
            JObject filter = new JObject();
            JObject image = new JObject();
            filter["is_to_all"] = all;
            filter["tag_id"] = tagId;
            image["media_id"] = mediaId;
            root["filter"] = filter;
            root["image"] = image;
            root["msgtype"] = "image";

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/sendall"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过标签群视频
        /// </summary>
        /// <param name="all"></param>
        /// <param name="tagId"></param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> SendVideoMessageByLabelAsync(bool all, string tagId, string mediaId,
            string title, string description, string accessToken)
        {
            JObject root = new JObject();
            JObject filter = new JObject();
            JObject video = new JObject
            {
                ["media_id"] = mediaId,
                ["title"] = title,
                ["description"] = description
            };
            filter["is_to_all"] = all;
            filter["tag_id"] = tagId;
            root["filter"] = filter;
            root["mpvideo"] = video;
            root["msgtype"] = "mpvideo";

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/sendall"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过标签群发卡卷
        /// </summary>
        /// <param name="all"></param>
        /// <param name="tagId"></param>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> SendCardMessageByLabelAsync(bool all, string tagId, string mediaId,
            string accessToken)
        {
            JObject root = new JObject();
            JObject filter = new JObject();
            JObject video = new JObject();
            filter["is_to_all"] = all;
            filter["tag_id"] = tagId;
            video["card_id"] = mediaId;
            root["filter"] = filter;
            root["wxcard"] = video;
            root["msgtype"] = "wxcard";

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/sendall"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过OpenId群发文本消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="accessToken"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<dynamic> SendTextMessageByOpenIdAsync(string content, string accessToken,
            params string[] user)
        {
            JObject root = new JObject();
            JObject text = new JObject {["content"] = content};
            root["text"] = text;
            root["msgtype"] = "text";
            root["touser"] = new JArray(user);

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/send"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过OpenId群发图片
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<dynamic> SendImageMessageByOpenIdAsync(string mediaId, string accessToken,
            params string[] user)
        {
            JObject root = new JObject();
            JObject image = new JObject {["media_id"] = mediaId};
            root["image"] = image;
            root["msgtype"] = "image";
            root["touser"] = new JArray(user);

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/send"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过OpenId群发视频
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="accessToken"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<dynamic> SendVideoMessageByOpenIdAsync(string mediaId, string title, string description,
            string accessToken, params string[] user)
        {

            JObject root = new JObject();
            JObject filter = new JObject();
            JObject video = new JObject
            {
                ["media_id"] = mediaId,
                ["title"] = title,
                ["description"] = description
            };
            root["filter"] = filter;
            root["mpvideo"] = video;
            root["msgtype"] = "mpvideo";
            root["touser"] = new JArray(user);

            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/send"), new Dictionary<string, string>
            {
                {
                    WeChatConstant.AccessToken, accessToken
                }
            }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过OpenId群发音频
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<dynamic> SendVoiceMessageByOpenIdAsync(string mediaId, string accessToken,
            params string[] user)
        {
            JObject root = new JObject();
            JObject filter = new JObject();
            JObject voice = new JObject {["media_id"] = mediaId};
            root["filter"] = filter;
            root["voice"] = voice;
            root["msgtype"] = "voice";
            root["touser"] = new JArray(user);
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/send"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 通过OpenId群发文章
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <param name="reprint"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<dynamic> SendArticleMessageByOpenIdAsync(string mediaId, string accessToken, int reprint = 0,
            params string[] user)
        {
            JObject root = new JObject();
            JObject media = new JObject {["media_id"] = mediaId};
            root["mpnews"] = media;
            root["msgtype"] = "mpnews";
            root["send_ignore_reprint"] = reprint;
            root["touser"] = new JArray(user);
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/send"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken,
                        accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 删除群发的消息
        /// 只支持删除图文消息和视频消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="token"></param>
        /// <param name="articleIndex"></param>
        /// <returns></returns>
        public async Task<dynamic> DeleteSendMessageAsync(string messageId, string token, int articleIndex = 0)
        {
            JObject root = new JObject
            {
                ["msg_id"] = messageId,
                ["article_idx"] = articleIndex
            };
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/delete"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, token
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }


        public async Task<dynamic> PreviewAsync(string mediaId, string accessToken,string openId, int reprint = 0)
        {
            JObject root = new JObject();
            JObject media = new JObject {["media_id"] = mediaId};
            root["mpnews"] = media;
            root["msgtype"] = "mpnews";
            root["send_ignore_reprint"] = reprint;
            root["touser"] = openId;
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/message/mass/preview"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken,
                        accessToken
                    }
                }, root.ToString());
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }
    }
}