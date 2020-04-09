using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;

namespace Caster.WeChat.Service
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserService
    {
        private readonly IClient _client;

        public UserService(IClient httpClient)
        {
            _client = httpClient;
        }

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> CreateLabelAsync(string labelName, string accessToken)
        {
            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/create"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken,
                        accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    tag = new
                    {
                        name = labelName
                    }
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> GetLabelListAsync(string accessToken)
        {
            var response = await _client.ExecuteGetRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/get"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                });
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="labelName"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> UpdateLabelAsync(string labelId, string labelName, string accessToken)
        {
            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/update"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    tag = new
                    {
                        id = labelId,
                        name = labelName
                    }
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task DeleteLabelAsync(string labelId, string accessToken)
        {
            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/delete"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    tag = new
                    {
                        id = labelId,
                    }
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }

        /// <summary>
        /// 根据标签获取关注的用户
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="accessToken"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public async Task<JObject> FindFansByLabelAsync(string labelId, string accessToken, string nextOpenId = "")
        {
            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/user/tag/get"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    tagId = labelId,
                    next_openid = nextOpenId
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result;
        }

        /// <summary>
        /// 批量为用户进行标签写入
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task BatchWriteLabelForUserAsync(string labelId, string accessToken, List<string> openId)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/members/batchtagging"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    openid_list = openId,
                    tagid = labelId
               }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }

        /// <summary>
        /// 批量取消用户标签
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task BatchCancelLabelForUserAsync(string labelId, string accessToken, List<string> openId)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/members/batchuntagging"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    openid_list = openId,
                    tagid = labelId
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }

        /// <summary>
        /// 更新用户备注
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="name"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task UpdateRemarkForUserAsync(string openId, string name, string accessToken)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/user/info/updateremark"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    openid = openId,
                    remark = name
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }

        /// <summary>
        /// 根据OpenId获取用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="accessToken"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public async Task<dynamic> GetWeChatUserInfoAsync(string openId, string accessToken, string lang = "zh_CN")
        {
            var response = await _client.ExecuteGetRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/user/info"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    },
                    {
                        "openid", openId
                    },
                    {
                        "lang", lang
                    }
                });
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 获取关注和未关注的用户列表
        /// 最多拉取100条
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public async Task<dynamic> GetUserInfoListAsync(string accessToken,List<string> openId, string lang = "zh_CN")
        {
            var ids = new List<object>();
            foreach (var id in openId)
            {
                ids.Add(new
                {
                    openid = id,
                    lang
                });
            }
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/user/info/batchget"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                },JsonConvert.SerializeObject(new
                {
                    user_list = ids
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 获取关注的用户
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="nextOpenId">上次拉取用户的最后的一个openId</param>
        /// <returns></returns>
        public async Task<dynamic> GetWatchOpenIdListAsync(string accessToken, string nextOpenId = "")
        {
            var response = await _client.ExecuteGetRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/user/get"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    },
                    {
                        "next_openid", nextOpenId
                    }
                });
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 获取黑名单
        /// </summary>
        /// <param name="nextOpenId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> GetBlackListAsync(string accessToken,string nextOpenId ="")
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/members/getblacklist"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    begin_openid = nextOpenId
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 根据的OpenId加入黑名单
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task OpenIdByJoinBlackAsync(List<string> openId, string accessToken)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/members/batchblacklist"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    openid_list = openId
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }

        /// <summary>
        /// 取消用户的黑名单
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task OpenIdByRemoveBlackAsync(List<string> openId, string accessToken)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/tags/members/batchunblacklist"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new
                {
                    openid_list = openId
                }));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }
    }
}