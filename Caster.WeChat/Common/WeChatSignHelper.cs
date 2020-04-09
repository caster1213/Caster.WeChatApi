using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;

namespace Caster.WeChat.Common
{
    public class WeChatSignHelper
    {
        public static string CreateMd5Sign(object request, string key)
        {
            var type = request.GetType();
            var properties = type.GetProperties();

            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();

            foreach (var property in properties)
            {
                if (property.Name == "Sign") continue;
                if (property.GetValue(request) == null) continue;
                var attrArray = property.GetCustomAttributes(typeof(XmlElementAttribute), true);
                if (attrArray.Length == 0) continue;
                foreach (var attr in attrArray)
                {
                    if (attr is XmlElementAttribute ele)
                    {
                        string value = property.GetValue(request).ToString();
                        if (string.IsNullOrEmpty(value)) continue;
                        sortedDictionary.Add(ele.ElementName, value);
                    }
                }
            }

            return CreateSign(key, sortedDictionary);
        }

        public static string CreateMd5SignByXml(XmlDocument xml, string key)
        {
            
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
            
            var nodes =  xml.FirstChild.ChildNodes;

            for (int i = 0; i <=nodes.Count-1; i++)
            {
                if(nodes[i].Name == "sign") continue;
                
                sortedDictionary.Add(nodes[i].Name, nodes[i].InnerText);
            }

            return CreateSign(key, sortedDictionary);

        }

        private static string CreateSign(string key, SortedDictionary<string, string> sortedDictionary)
        {
            StringBuilder input = new StringBuilder();
            StringBuilder output = new StringBuilder();
            foreach (var map in sortedDictionary)
            {
                input.Append($"{map.Key}={map.Value}&");
            }

            input.Append("key=" + key);

            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
                foreach (var b in data)
                {
                    output.Append(b.ToString("x2"));
                }
            }

            return output.ToString().ToUpper();
        }

        public static string CreateHash256Sign(object request, string key)
        {
            var type = request.GetType();
            var properties = type.GetProperties();

            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();

            foreach (var property in properties)
            {
                if (property.Name == "Sign") continue;
                string value = property.GetValue(request).ToString();
                if (string.IsNullOrEmpty(value)) continue;
                sortedDictionary.Add(property.Name, value);
            }

            return CreateSign(key, sortedDictionary);
        }
        
        
        public static string CreateMessageSign(string nonce,string timestamp,string token)
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>
            {
                {nonce, ""},
                {timestamp, ""},
                {token, ""}
            };
            StringBuilder storeStr = new StringBuilder();

            foreach (var dic in dictionary)
            {
                storeStr.Append(dic.Key);
            }

            SHA1 sha1 = SHA1.Create();

            var buffer = sha1.ComputeHash(Encoding.UTF8.GetBytes(storeStr.ToString()));

            var sign = Encoding.UTF8.GetString(buffer);

            return sign;
        }
    }
}