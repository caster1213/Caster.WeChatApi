using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Caster.WeChat.Common;

namespace Caster.WeChat.Impl
{
    public class HttpClientService : IClient
    {
        private readonly HttpClient _client;
        private HttpClient _certClient;
        private readonly Dictionary<string, HttpClient> _clients;
        
        
        public HttpClientService()
        {
            _client = new HttpClient();
            _clients = new Dictionary<string, HttpClient>();
        }
        

        public async Task<string> ExecutePostRequest(string url, Dictionary<string, string> urlParam, string body,
            string contentType = "application/json")
        {
            string requestUri = WeChatHelper.BuildUrl(url, urlParam);
            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            var message = await _client.PostAsync(requestUri, httpContent);
            return await message.Content.ReadAsStringAsync();
        }

        public async Task<string> ExecutePostRequest(string url, Dictionary<string, string> urlParam, string body, string certPath, string password,
            string contentType = "application/json")
        {
            if (_certClient == null)
            {
                var handler = new HttpClientHandler();
                var cert = new X509Certificate2(certPath, password);
                handler.ClientCertificates.Add(cert);
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.SslProtocols = SslProtocols.Tls12;
                _certClient = new HttpClient(handler);
            }
            
            string requestUri = WeChatHelper.BuildUrl(url, urlParam);
            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            var message = await _certClient.PostAsync(requestUri, httpContent);
            return await message.Content.ReadAsStringAsync();
        }

        public async Task<Stream> ExecutePostDownloadRequest(string url, Dictionary<string, string> urlParam,
            string body,
            string contentType = "application/json")
        {
            string requestUri = WeChatHelper.BuildUrl(url, urlParam);
            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            var message = await _client.PostAsync(requestUri, httpContent);
            return await message.Content.ReadAsStreamAsync();
        }

        public async Task<string> ExecuteGetRequest(string url, Dictionary<string, string> urlParam)
        {
            var message = await _client.GetAsync(WeChatHelper.BuildUrl(url, urlParam));
            return await message.Content.ReadAsStringAsync();
        }

        public async Task<string> ExecuteGetRequest(string url, Dictionary<string, string> urlParam, string certPath, string password)
        {
            if (_certClient == null)
            {
                var handler = new HttpClientHandler();
                var cert = new X509Certificate2(certPath, password);
                handler.ClientCertificates.Add(cert);
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.SslProtocols = SslProtocols.Tls12;
                _certClient = new HttpClient(handler);
            }
            
            var message = await _certClient.GetAsync(WeChatHelper.BuildUrl(url, urlParam));
            return await message.Content.ReadAsStringAsync();
        }

        public async Task<Stream> ExecuteDownloadFileRequest(string url, Dictionary<string, string> urlParam)
        {
            var message = await _client.GetAsync(WeChatHelper.BuildUrl(url, urlParam));
            return await message.Content.ReadAsStreamAsync();
        }

        public async Task<string> ExecuteUploadFileRequest(string url, Dictionary<string, string> urlParam,
            Dictionary<string, string> body, params FileStream[] files)
        {
            var requestUri = WeChatHelper.BuildUrl(url, urlParam);
            var boundary = Guid.NewGuid().ToString();
            MultipartFormDataContent httpContent = new MultipartFormDataContent(boundary);

            httpContent.Headers.Remove("Content-Type");
            httpContent.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);
            foreach (var file in files)
            {
                var streamContent = new StreamContent(file);
                streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("media")
                {
                    FileName = file.Name
                };
                httpContent.Add(streamContent);
            }

            var message = await _client.PostAsync(requestUri, httpContent);
            return await message.Content.ReadAsStringAsync();
        }
    }
}