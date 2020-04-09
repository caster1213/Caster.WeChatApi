using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;

namespace Caster.WeChat.Service
{
    public class CommentService
    {
        private readonly IClient _client;

        public CommentService(IClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 关闭评论
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="accessToken"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task CommentClosedAsync(string messageId, string accessToken, int index = 0)
        {
            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/comment/close"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    msg_data_id = messageId, index
                }));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }

        /// <summary>
        /// 打开评论
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="accessToken"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task CommentOpenedAsync(string messageId, string accessToken, int index = 0)
        {
            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/comment/open"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    msg_data_id = messageId, index
                }));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }

        /// <summary>
        /// 获取文章评论
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type"></param>
        /// <param name="accessToken"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<dynamic> GetCommentListAsync(string messageId, int pageIndex, int pageSize, int type,
            string accessToken, int index = 0)
        {
            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/comment/list"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    msg_data_id = messageId,
                    index,
                    begin = pageIndex,
                    count = pageSize,
                    type
                }));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
            return root.ToObject<dynamic>();
        }

        /// <summary>
        /// 设置为精选
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="commentId"></param>
        /// <param name="accessToken"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task SetEssenceAsync(string messageId, string commentId, string accessToken, int index = 0)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/comment/markelect"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    msg_data_id = messageId,
                    index,
                    user_comment_id = commentId
                }));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }

        /// <summary>
        /// 取消精选
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="commentId"></param>
        /// <param name="accessToken"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task CancelEssenceAsync(string messageId, string commentId, string accessToken, int index = 0)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/comment/unmarkelect"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    msg_data_id = messageId,
                    index,
                    user_comment_id = commentId
                }));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="commentId"></param>
        /// <param name="accessToken"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task DeleteCommentAsync(string messageId, string commentId, string accessToken, int index = 0)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/comment/delete"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    msg_data_id = messageId,
                    index,
                    user_comment_id = commentId
                }));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }

        /// <summary>
        /// 删除回复
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="commentId"></param>
        /// <param name="accessToken"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task DeleteCommentReplyAsync(string messageId, string commentId, string accessToken, int index = 0)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/comment/reply/delete"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    msg_data_id = messageId,
                    index,
                    user_comment_id = commentId
                }));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="commentId"></param>
        /// <param name="content"></param>
        /// <param name="accessToken"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task ReplyAsync(string messageId, string commentId, string content, string accessToken,
            int index = 0)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/comment/reply/add"),
                new Dictionary<string, string>
                {
                    {
                        "access_token", accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    msg_data_id = messageId,
                    index,
                    user_comment_id = commentId,
                    content
                }));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }
    }
}