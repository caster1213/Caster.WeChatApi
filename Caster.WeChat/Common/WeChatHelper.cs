using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Caster.WeChat.Common
{
    public static class WeChatHelper
    {
        public static string BuildUrl(string url, Dictionary<string, string> param = null)
        {
            var result = url;
            if (param == null) return result;
            if (param.Count == 0) return result;
            result += "?";
            foreach (var dic in param)
            {
                result += $"{dic.Key}={dic.Value}&";
            }

            return result.Substring(0, result.Length - 1);
        }

        public static void CheckResponseStatus(JObject root, string methodName = "")
        {
            if (root.Property("errcode") != null)
            {
                if (root.GetValue("errcode").ToString().Equals("0") == false)
                {
                    throw new WeChatApiException(root.GetValue("errcode").ToString(),
                        root.GetValue("errmsg").ToString(), methodName);
                }
            }
        }

        public static string GetWeChatApiDomain(string value)
        {
            return "https://api.weixin.qq.com" + value;
        }

        public static string GetDescriptionValue(this Enum source)
        {
           var type = source.GetType();
           string name = Enum.GetName(type, source);
           var field = type.GetField(name);
           var attributeValues = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

           var value = attributeValues as DescriptionAttribute;

           return value?.Description;
        }
    }
}