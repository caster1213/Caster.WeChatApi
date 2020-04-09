using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Caster.WeChat.Common
{
    public class Helper
    {
        public static string[] FormatXmlField(string fieldName, XmlDocument root)
        {
            var nodes = root.FirstChild;
            int max = 3;
            List<string> values = new List<string>();
            for (int index = 1; index <= max; index++)
            {
                var node = nodes[fieldName + "_" + index];
                if (node == null) break;
                values.Add(node.InnerText);
            }

            return values.ToArray();
        }


        public static string GetNonceStr(int length = 16)
        {
            char[] chars =
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
                'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                var index = random.Next(chars.Length - 1);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        public static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}