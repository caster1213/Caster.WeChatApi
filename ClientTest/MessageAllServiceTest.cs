using System.Threading.Tasks;
using Caster.WeChat;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class MessageAllServiceTest
    {
        private readonly ITestOutputHelper _output;
        private readonly WeChatWeb _web;
        private readonly string _token;

        public MessageAllServiceTest(ITestOutputHelper output)
        {
            _output = output;
            _web = Helper.CreateClient();
            _token = Helper.CreateClient().GetToken().Result;
        }

        [Fact]
        public async Task SendPreviewMessageByOpenIdTest()
        {
            var result = await _web.MessageAllService.PreviewAsync(
                "-0FYa1byyLETvu-vE_6PvL5ISmWYlfU8017s0qpz70E", _token, "oVJAUwVamS4JkLukifvZ-fFzMvKI");
            _output.WriteLine(result.ToString());
        }
    }
}