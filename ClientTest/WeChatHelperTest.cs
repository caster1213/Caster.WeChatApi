using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class WeChatHelperTest
    {
        [Fact]
        public void UrlBuilder()
        {
            string result = WeChatHelper.BuildUrl("http://www.baidu.com", new Dictionary<string, string>()
            {
                {"aa", "11"},
                {"bb", "22"}
            });

            var match = Regex.Match(result, "(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]");

            Assert.True(match.Success, result);
        }

        [Fact]
        public void CheckResponseStatus()
        {
            JObject root = new JObject();
            root["errcode"] = "40012";
            root["errmsg"] = "40012";
            try
            {
                WeChatHelper.CheckResponseStatus(root);
            }
            catch (WeChatApiException e)
            {
                Assert.True(e.Code == "40012","e.Code == '40012' && e.Message == '40012'");
            }
        }
    }
}