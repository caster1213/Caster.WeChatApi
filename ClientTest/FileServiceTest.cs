using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Caster.WeChat;
using Caster.WeChat.Common;
using Caster.WeChat.Parameter;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class FileServiceTest
    {
        private readonly ITestOutputHelper _output;
        private readonly WeChatWeb _web;
        private readonly string _token;

        public FileServiceTest(ITestOutputHelper output)
        {
            _output = output;
            _web = Helper.CreateClient();
            _token = Helper.CreateClient().GetToken().Result;
        }

        [Fact]
        public async Task UploadTempFileTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var stream = File.Open("/Users/Shared/Image/t1.jpeg", FileMode.Open);
            var result = await client.FileService.UploadTempFileAsync(FileType.Thumb, stream, token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task GetTempFileTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var stream =
                await client.FileService.GetTempFileAsync(
                    "9WNaZ10-JhMBiwqM62ov__k0YNKRXd-doM-ALJvQwwShzXUG1VXCWhARh4ONd9GG", token);
            Assert.True(stream.Length > 0, "字节长度0 文件获取失败");
        }

        [Fact]
        public async Task UploadPermanentFileTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            _output.WriteLine(token);
            var stream = File.Open("/Users/Shared/Image/t1.jpeg", FileMode.Open);
            var result = await client.FileService.UploadPermanentFile(FileType.Thumb, stream, token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task UploadArticleImageTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var stream = File.Open("/Users/Shared/Image/t1.jpeg", FileMode.Open);
            var result = await _web.FileService.UploadArticleImageAsync(stream, token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task UploadArticleTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var result = await _web.FileService.UploadArticleAsync(new List<UpdateArticleParameter>
            {
                new UpdateArticleParameter
                {
                    Author = "测试1",
                    Content = "你第一次接触和用户相关的互联网产品时，或者曾今在我眼里。用户体系无非就是“登录”和“注册”，“修改用户信息”这些，等。简单来做的话，无非我们需要一张表去记录用户的身份信息：注册时(insert操作)，往表里插入一个数据；登录时(select&update操作)，通过用户标识(手机号、邮箱等)判断用户的密码是否正确；修改用户信息(select&update操作)，就是直接update这个uid的用户信息(头像、昵称等)。",
                    ContentSourceUrl = "http://www.baidu.com",
                    Digest = "图文消息的摘要",
                    FileId = "-0FYa1byyLETvu-vE_6PvD_MHqzcQoK5nvOoDYzzM74",
                    NeedOpenComment = 1,
                    OnlyFansOpenComment = 0,
                    ShowCoverPic = 0,
                    Title = "测试文章"
                }
            }, token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task GetFileListTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var result = await _web.FileService.GetFileListAsync(FileType.Image, 1, 10, token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task DownloadPermanentFileTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var result =
                await client.FileService.DownloadPermanentFile("-0FYa1byyLETvu-vE_6PvD_MHqzcQoK5nvOoDYzzM74", token);

            Assert.True(result.Length > 0, "result.Length > 0");
        }

        [Fact]
        public async Task DeletePermanentFileTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            await client.FileService.DeleteFileAsync("-0FYa1byyLETvu-vE_6PvD_MHqzcQoK5nvOoDYzzM74", token);
        }

        [Fact]
        public async Task CountFileTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var result = await client.FileService.CountFileAsync(token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task UpdateArticleTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var result = await client.FileService.UpdateArticleAsync(new UpdateArticleParameter
            {
                Title = "修改后的标题",
                Content = "你第一次接触和用户相关的互联网产品时，或者曾今在我眼里。用户体系无非就是“登录”和“注册”，“修改用户信息”这些，等。简单来做的话，无非我们需要一张表去记录用户的身份信息：注册时(insert操作)，往表里插入一个数据；登录时(select&update操作)，通过用户标识(手机号、邮箱等)判断用户的密码是否正确；修改用户信息(select&update操作)，就是直接update这个uid的用户信息(头像、昵称等)。",
                Author = "bsg",
                Digest = "摘要已被修改",
                ContentSourceUrl = "http://www.jd.com",
                FileId = "-0FYa1byyLETvu-vE_6PvD_MHqzcQoK5nvOoDYzzM74",
                ShowCoverPic = 0,
                NeedOpenComment = 1,
                OnlyFansOpenComment = 0
            }, "-0FYa1byyLETvu-vE_6PvL5ISmWYlfU8017s0qpz70E", 0, token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task GetArticleTest()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            var result = await client.FileService.GetPermanent("-0FYa1byyLETvu-vE_6PvL5ISmWYlfU8017s0qpz70E", token);
            _output.WriteLine(result.ToString());
        }
    }
}