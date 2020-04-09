using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Caster.WeChat.Common
{
    public interface IClient
    {

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="urlParam">URL参数</param>
        /// <param name="body">数据</param>
        /// <param name="contentType">类型默认 application/json</param>
        /// <returns></returns>
        Task<string> ExecutePostRequest(string url, Dictionary<string, string> urlParam, string body,
            string contentType = "application/json");

        /// <summary>
        /// 使用证书发送POST请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="urlParam">URL参数</param>
        /// <param name="body">数据</param>
        /// <param name="certPath">证书路径</param>
        /// <param name="password">密码</param>
        /// <param name="contentType">类型默认 application/json</param>
        /// <returns></returns>
        Task<string> ExecutePostRequest(string url, Dictionary<string, string> urlParam, string body, string certPath,
            string password, string contentType = "application/json");

        Task<Stream> ExecutePostDownloadRequest(string url, Dictionary<string, string> urlParam, string body,
            string contentType = "application/json");

        Task<string> ExecuteGetRequest(string url, Dictionary<string, string> urlParam);
        
        Task<string> ExecuteGetRequest(string url, Dictionary<string, string> urlParam,string certPath,
            string password);
        Task<Stream> ExecuteDownloadFileRequest(string url, Dictionary<string, string> urlParam);

        Task<string> ExecuteUploadFileRequest(string url, Dictionary<string, string> urlParam,
            Dictionary<string, string> body, params FileStream[] files);
    }
}