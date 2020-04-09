using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;
using Caster.WeChat.Parameter;

namespace Caster.WeChat.Service
{
    /// <summary>
    /// 素材服务
    /// </summary>
    public class FileService
    {
        private readonly IClient _client;

        public FileService(IClient httpClient)
        {
            _client = httpClient;
        }

        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="stream"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> UploadTempFileAsync(FileType fileType, FileStream stream, string accessToken)
        {
            var param = new Dictionary<string, string>
            {
                {
                    "type",
                    fileType.ToString()
                },
                {
                    "access_token", accessToken
                }
            };
            var response = await _client.ExecuteUploadFileRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/media/upload"), param, new Dictionary<string, string>(),
                stream);
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result;
        }

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<Stream> GetTempFileAsync(string fileId, string accessToken)
        {
            var param = new Dictionary<string, string>
            {
                {
                    "media_id",
                    fileId
                },
                {
                    "access_token", accessToken
                }
            };
            return await _client.ExecuteDownloadFileRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/media/get"),
                param);
        }

        /// <summary>
        /// 上传图文素材
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> UploadArticleAsync(List<UpdateArticleParameter> parameters, string accessToken)
        {
            var param = new Dictionary<string, string>
            {
                {
                    WeChatConstant.AccessToken,
                    accessToken
                }
            };

            var root = new
            {
                articles = parameters
            };

            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/add_news"), param,
                JsonConvert.SerializeObject(root));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 上传文章图片
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> UploadArticleImageAsync(FileStream stream, string accessToken)
        {
            var response = await _client.ExecuteUploadFileRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/media/uploadimg"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                },
                new Dictionary<string, string>(), stream);
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        public async Task<dynamic> UpdateArticleAsync(UpdateArticleParameter article, string mediaId, int index,
            string token)
        {
            var root = new
            {
                media_id = mediaId,
                index,
                articles = article
            };
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/add_news"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, token
                    }
                },
                JsonConvert.SerializeObject(root));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result;
        }

        /// <summary>
        /// 上传永久素材
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="stream"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> UploadPermanentFile(FileType fileType, FileStream stream, string accessToken)
        {
            var response = await _client.ExecuteUploadFileRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/add_material"), new Dictionary<string, string>
                {
                    {
                        "type",
                        fileType.ToString().ToLower()
                    },
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, null, stream);
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result;
        }

        /// <summary>
        /// 上传永久素材视频
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="stream"></param>
        /// <param name="accessToken"></param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public async Task<dynamic> UploadPermanentFile(FileType fileType, FileStream stream, string accessToken,
            string title, string desc)
        {
            var urlParam = new Dictionary<string, string>
            {
                {
                    WeChatParameterKey.Access_Token,
                    accessToken
                },
                {
                    "type",
                    fileType.ToString()
                },
                {
                    WeChatConstant.AccessToken, accessToken
                }
            };
            var bodyParam = new Dictionary<string, string>
            {
                {"title", title},
                {"introduction", desc}
            };

            var response = await _client.ExecuteUploadFileRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/add_material"), urlParam, bodyParam, stream);

            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 下载永久素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<Stream> DownloadPermanentFile(string mediaId, string accessToken)
        {
            return await _client.ExecutePostDownloadRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/get_material"), new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                },
                JsonConvert.SerializeObject(new
                {
                    media_id = mediaId
                }));
        }

        /// <summary>
        /// 获取图文或者视频素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> GetPermanent(string mediaId, string accessToken)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/get_material"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                },
                JsonConvert.SerializeObject(new {media_id = mediaId}));

            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }


        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task DeleteFileAsync(string mediaId, string accessToken)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/del_material"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(new {media_id = mediaId}));
            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
        }

        /// <summary>
        /// 统计素材数量
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> CountFileAsync(string accessToken)
        {
            var response = await _client.ExecuteGetRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/get_materialcount"), new Dictionary<string, string>
                {
                    {WeChatConstant.AccessToken, accessToken}
                });

            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result.ToObject<dynamic>();
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> GetFileListAsync(FileType fileType, int pageIndex, int pageSize, string accessToken)
        {
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/material/batchget_material"),
                new Dictionary<string, string>
                {
                    {WeChatConstant.AccessToken, accessToken}
                },
                JsonConvert.SerializeObject(new
                {
                    type = fileType.ToString().ToLower(),
                    offset = pageIndex,
                    count = pageSize
                }));

            JObject result = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(result);
            return result;
        }
    }
}