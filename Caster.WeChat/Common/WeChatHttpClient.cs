// using System.Collections.Generic;
// using System.IO;
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;
// using Newtonsoft.Json.Linq;
//
// namespace Caster.WeChat.Common
// {
//     public sealed class WeChatHttpClient
//     {
//         private readonly HttpClient _httpClient;
//
//         public WeChatHttpClient(HttpClient httpClient)
//         {
//             _httpClient = httpClient;
//         }
//
//         public async Task<dynamic> ExecuteGetRequestAsync(string apiUrl, string methodName,
//             Dictionary<string, string> param)
//         {
//             var message = await _httpClient.GetAsync(WeChatHelper.BuildUrl(apiUrl, param));
//             var result = await message.Content.ReadAsStringAsync();
//             var json = JObject.Parse(result);
//             if (InspectException(result))
//             {
//                 throw new WeChatApiException(json.GetValue(WeChatParameterKey.Error_Code).ToObject<int>(),
//                     json.GetValue(WeChatParameterKey.Error_Message).ToString(), methodName);
//             }
//
//             return json.ToObject<dynamic>();
//         }
//
//         public async Task<dynamic> ExecuteGetRequestAsync(WeChatUrl weChatUrl,
//             Dictionary<string, string> param)
//         {
//             if (string.IsNullOrEmpty(weChatUrl.Url) == false)
//             {
//                 param.Add("access_token", weChatUrl.AccessToken);
//             }
//
//             var message = await _httpClient.GetAsync(WeChatHelper.BuildUrl(weChatUrl.Url, param));
//             var result = await message.Content.ReadAsStringAsync();
//             var json = JObject.Parse(result);
//             if (InspectException(result))
//             {
//                 throw new WeChatApiException(json.GetValue(WeChatParameterKey.Error_Code).ToObject<int>(),
//                     json.GetValue(WeChatParameterKey.Error_Message).ToString(), weChatUrl.Name);
//             }
//
//             return json.ToObject<dynamic>();
//         }
//
//         public async Task<string> ExecutePostRequestAsync(string apiUrl, string methodName,
//             Dictionary<string, string> urlParam,
//             string body, string contentType = "application/json")
//         {
//             string url = WeChatHelper.BuildUrl(apiUrl, urlParam);
//             HttpContent httpContent = new StringContent(body);
//             httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
//             var message = await _httpClient.PostAsync(url, httpContent);
//             return await message.Content.ReadAsStringAsync();
//         }
//
//         public async Task<dynamic> ExecutePostJsonRequestAsync(WeChatUrl weChatUrl, string jsonObject,
//             Dictionary<string, string> urlParam = null)
//         {
//             if (urlParam == null)
//             {
//                 urlParam = new Dictionary<string, string>();
//             }
//
//             if (string.IsNullOrEmpty(weChatUrl.AccessToken) == false)
//             {
//                 urlParam.Add("access_token", weChatUrl.AccessToken);
//             }
//
//             var result =  await ExecutePostRequestAsync(weChatUrl.Url, weChatUrl.Name, urlParam, jsonObject);
//
//             return result;
//         }
//
//         public async Task<Stream> DownloadFileAsync(WeChatUrl weChatUrl, Dictionary<string, string> param)
//         {
//             return await DownloadFileAsync(weChatUrl.Url, weChatUrl.Name, param);
//         }
//
//         public async Task<Stream> DownloadFileAsync(WeChatUrl weChatUrl, Dictionary<string, string> param,
//             string jsonObject)
//         {
//             return await DownloadFileAsync(weChatUrl.Url, weChatUrl.Name, param, jsonObject);
//         }
//
//         private async Task<Stream> DownloadFileAsync(string apiUrl, string methodName, Dictionary<string, string> param)
//         {
//             var message = await _httpClient.GetAsync(WeChatHelper.BuildUrl(apiUrl, param));
//             if (message.StatusCode == HttpStatusCode.NotFound)
//                 throw new WeChatApiException(-1, "ticket非法", methodName);
//             return await message.Content.ReadAsStreamAsync();
//         }
//
//         private async Task<Stream> DownloadFileAsync(string apiUrl, string methodName,
//             Dictionary<string, string> urlParam, string jsonObject)
//         {
//             HttpContent httpContent = new StringContent(jsonObject);
//             httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//             var message = await _httpClient.PostAsync(WeChatHelper.BuildUrl(apiUrl, urlParam), httpContent);
//             if (message.StatusCode == HttpStatusCode.NotFound)
//                 throw new WeChatApiException(-1, "ticket非法", methodName);
//             return await message.Content.ReadAsStreamAsync();
//         }
//
//         public async Task<dynamic> UploadFileAsync(string apiUrl,
//             string methodName,
//             Dictionary<string, string> urlParam,
//             Dictionary<string, string> bodyParam,
//             params Stream[] streams)
//         {
//             var url = WeChatHelper.BuildUrl(apiUrl, urlParam);
//             MultipartFormDataContent httpContent = new MultipartFormDataContent();
//             foreach (var stream in streams)
//             {
//                 httpContent.Add(new StreamContent(stream));
//             }
//
//             foreach (var body in bodyParam)
//             {
//                 httpContent.Add(new StringContent(body.Value), body.Key);
//             }
//
//             var message = await _httpClient.PostAsync(url, httpContent);
//             var result = await message.Content.ReadAsStringAsync();
//             var json = JObject.Parse(result);
//             if (InspectException(result))
//             {
//                 throw new WeChatApiException(json.GetValue(WeChatParameterKey.Error_Code).ToObject<int>(),
//                     json.GetValue(WeChatParameterKey.Error_Message).ToString(), methodName);
//             }
//
//             return json.ToObject<dynamic>();
//         }
//
//         public async Task<dynamic> UploadFileAsync(WeChatUrl weChatUrl,
//             Dictionary<string, string> urlParam,
//             Dictionary<string, string> bodyParam,
//             params Stream[] streams)
//         {
//             if (string.IsNullOrEmpty(weChatUrl.AccessToken))
//             {
//                 urlParam.Add("access_token", weChatUrl.AccessToken);
//             }
//
//             return await UploadFileAsync(weChatUrl.Url, weChatUrl.Name, urlParam, bodyParam, streams);
//         }
//
//
//         private bool InspectException(string value)
//         {
//             JObject json = JObject.Parse(value);
//
//             if (json.GetValue("errcode").HasValues)
//             {
//                 if (json.GetValue("errcode").ToString().Equals("0") == false)
//                 {
//                     return true;
//                 }
//             }
//
//             return false;
//         }
//     }
// }