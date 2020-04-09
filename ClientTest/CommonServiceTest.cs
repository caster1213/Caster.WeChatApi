using System.Threading.Tasks;
using Caster.WeChat;
using Xunit;

namespace ClientTest
{
    public class CommonServiceTest
    {
        [Fact]
        public async Task GetAccessTokenTest()
        {
            WeChatWeb web = Helper.CreateClient();

            string token = await web.GetToken();


            Assert.True(string.IsNullOrEmpty(token) == false, "string.IsNullOrEmpty(token)");
        }

        [Fact]
        public async Task GerIpAddress()
        {
            WeChatWeb web = Helper.CreateClient();

            string token = await web.GetToken();

            var result = await web.CommonService.GetIpAddress(token);

            Assert.True(result.Length > 0, "result.Length > 0");
        }
    }

    
}