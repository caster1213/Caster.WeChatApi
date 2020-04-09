using System.Threading.Tasks;
using Caster.WeChat;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class CommentServiceTest
    {
        private readonly ITestOutputHelper _output;
        private readonly WeChatWeb _web;
        private readonly string _token;

        public CommentServiceTest(ITestOutputHelper output)
        {
            _output = output;
            _web = Helper.CreateClient();
            _token = Helper.CreateClient().GetToken().Result;
        }

        [Fact(Skip = "测试号无权限")]
        public async Task SetEssenceTest()
        {
            var result = await _web.CommentService.GetCommentListAsync("-0FYa1byyLETvu-vE_6PvL5ISmWYlfU8017s0qpz70E",
                1, 10, 1, _token);

            _output.WriteLine(result.ToString());
        }
    }
}