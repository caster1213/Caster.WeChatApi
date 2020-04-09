using System.Threading.Tasks;
using Caster.WeChat.Parameter;
using Xunit;

namespace ClientTest
{
    public class TemplateMessageServiceTest
    {
        [Fact]
        public async Task GetTest()
        {
            var client = Helper.CreateClient();

            var token = await client.GetToken();

            var result = await client.TemplateMessageService.Get(token);

            Assert.True(result.Count > 0, "result.Count less zero");
        }

        [Fact]
        public async Task Delete()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            await client.TemplateMessageService.DeleteAsync("7v9jYlFj8JrkAMs20EocPCSMSX90Ny5sxsPJVO1OAws", token);
        }

        [Fact]
        public async Task Send()
        {
            var client = Helper.CreateClient();
            var token = await client.GetToken();
            string openId = "oVJAUwVamS4JkLukifvZ-fFzMvKI";
            string templateId = "qOhDO6ybGLQ7rFiF9fACSz_gi1Gf6ZkpoJWX8ID0Lzw";
            var parameter = new TemplateMessageParameter
            {
                First = new FieldConfig("#173177", "您的企业资料审核通过"),
                Remark = new FieldConfig("#173177", "马上开始使用吧")
            };
            parameter.Content.Add(new FieldConfig("#173177", "TX29289231"));
            parameter.Content.Add(new FieldConfig("#173177", "腾讯科技"));
            parameter.Content.Add(new FieldConfig("#173177", "缺少资料"));
            var result = await client.TemplateMessageService.SendAsync(openId, templateId, token, parameter);
            Assert.True(string.IsNullOrEmpty(result) == false, "string.IsNullOrEmpty(result) == false");
        }
    }
}