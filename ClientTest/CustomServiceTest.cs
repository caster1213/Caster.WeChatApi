using System.Collections.Generic;
using System.Threading.Tasks;
using Caster.WeChat;
using Caster.WeChat.Parameter;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class CustomServiceTest
    {
        private readonly ITestOutputHelper _output;
        private readonly WeChatWeb _web;
        private readonly string _token;

        public CustomServiceTest(ITestOutputHelper output)
        {
            _output = output;
            _web = Helper.CreateClient();
            _token = Helper.CreateClient().GetToken().Result;
        }

        [Fact]
        public async Task SendTextMessageText()
        {
            await _web.CustomService.SendTextMessageAsync("测试消息", "oVJAUwVamS4JkLukifvZ-fFzMvKI", _token);
        }

        [Fact]
        public async Task SendImageMessageTest()
        {
            await _web.CustomService.SendImageMessageAsync(
                "OMWqi5UZHHD9Hj-rF3G6qwucDZVDPVr7lxzcHj1IcFyhm-3i4AyP3PfUK6tanJPc", "oVJAUwVamS4JkLukifvZ-fFzMvKI",
                _token);
        }

        [Fact]
        public async Task SendVoiceMessageTest()
        {
            await _web.CustomService.SendVoiceMessageAsync(
                "qekLqzQvtDP1L9xtuqJXnY3y0oAMjgm4VFPdWRASuSjwRe0zm6F7x5NDk3DIcS_D", "oVJAUwVamS4JkLukifvZ-fFzMvKI",
                _token);
        }

        [Fact]
        public async Task SendVideoMessageTest()
        {
            await _web.CustomService.SendVideoMessageAsync(
                "NSjU7UPQn44gCaKyarW9X0RHnCOmdE-sAmVzxi6EQCxbkkHILl6yU58OGHp_vZy1",
                "ncFRzpAdocujE21PvhGIBUUhfh8qMZJTQ95n1_HAhG_9WSGQ-Yxf-6omNfmUUlvI", "测试描述",
                "测试视频", "oVJAUwVamS4JkLukifvZ-fFzMvKI", _token);
        }

        [Fact]
        public async Task SendMusicMessageTest()
        {
            await _web.CustomService.SendMusicMessageAsync(
                "https://music.163.com/song?id=1404885266&userid=271079735",
                "https://music.163.com/song?id=1404885266&userid=271079735",
                "ncFRzpAdocujE21PvhGIBUUhfh8qMZJTQ95n1_HAhG_9WSGQ-Yxf-6omNfmUUlvI", "好音乐", "好音乐的介绍",
                "oVJAUwVamS4JkLukifvZ-fFzMvKI", _token);
        }

        [Fact]
        public async Task SendUrlMessageTest()
        {
            await _web.CustomService.SendUrlMessageAsync("http://skrshop.tech", "", "架构", "电商架构介绍",
                "oVJAUwVamS4JkLukifvZ-fFzMvKI", _token);
        }

        [Fact]
        public async Task SendArticleMessageTest()
        {
            await _web.CustomService.SendArticleMessageAsync("-0FYa1byyLETvu-vE_6PvL5ISmWYlfU8017s0qpz70E",
                "oVJAUwVamS4JkLukifvZ-fFzMvKI", _token);
        }

        [Fact(Skip = "测试号无法进行测试")]
        public async Task SendMenuMessage()
        {
            await _web.CustomService.SendMenuMessage("oVJAUwVamS4JkLukifvZ-fFzMvKI", "sdk使用的方便嘛", "欢迎反馈",
                new List<SendMenuMessageRequestParameter>
                {
                    new SendMenuMessageRequestParameter
                    {
                        Id = "101",
                        Content = "一般"
                    },
                    new SendMenuMessageRequestParameter
                    {
                        Id = "102",
                        Content = "很好"
                    }
                }, _token);
        }

        [Fact(Skip = "测试号无法进行测试")]
        public async Task AddCustomerServiceTest()
        {
            await _web.CustomService.AddCustomerServiceAsync("qishi", "骑士", "111111", _token);
        }

        [Fact(Skip = "测试号无法进行测试")]
        public async Task UpdateCustomerServiceTest()
        {
            await _web.CustomService.UpdateCustomerServiceAsync("qishi", "骑士", "111111", _token);
        }

        [Fact(Skip = "测试号无法进行测试")]
        public async Task DeleteCustomerServiceTest()
        {
            await _web.CustomService.DeleteCustomerServiceAsync("qishi", "骑士", "111111", _token);
        }

        [Fact(Skip = "测试号无法进行测试")]
        public async Task GetCustomServerListTest()
        {
            var result = await _web.CustomService.CustomerServiceListAsync(_token);

            _output.WriteLine(result.ToString());
        }
    }
}