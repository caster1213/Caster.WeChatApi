using System.IO;
using System.Text;
using System.Threading.Tasks;
using ClientTest.Mock;
using Caster.WeChat.Common;
using Caster.WeChat.Config;
using Caster.WeChat.MessageHandler;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class MessageHandlerTest
    {
        private readonly ITestOutputHelper _outputHelper;


        public MessageHandlerTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }


        [Fact]
        public async Task CleartextMessage()
        {
            var config = new ApiOption
            {
                AppId = "wxd2dc006bb81427b9",
                MessageEncryptModel = EncryptModel.Cleartext,
                Distinct = false,
                EncodingAesKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C"
            };
            string input =
                @"<xml><ToUserName><![CDATA[oFQVa1aoCaRWMHh-y15eg32JXhmU]]></ToUserName><FromUserName><![CDATA[fromUser]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[888888]]></Content><MsgId>1234567890123456</MsgId></xml>";
            var buffer = Encoding.UTF8.GetBytes(input);
            Stream stream = new MemoryStream(buffer);
            var result = await new WeChatMessageHandler(config, new MockCache())
                .AddHandlerService(new MockTextMessageHandler())
                .SetStreamMessage(stream)
                .ExecutedAsync();
            Assert.True(string.IsNullOrEmpty(result) == false, "string.IsNullOrEmpty(result) == false");
            _outputHelper.WriteLine(result);
        }

        [Fact]
        public async Task EncryptMessage()
        {
            var config = new Helper.WeChatConfig().Value;
            string cleartext =
                @"<xml><ToUserName><![CDATA[oFQVa1aoCaRWMHh-y15eg32JXhmU]]></ToUserName><FromUserName><![CDATA[fromUser]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[888888]]></Content><MsgId>1234567890123456</MsgId></xml>";
            var encrypt = MessageCryptography.AesEncrypt(cleartext, config.EncodingAesKey, config.AppId);
            var sign = Check.GetWeChatMessageSign(config.EncodingAesKey, "123456789", "1411034505", encrypt);
            string input = $"<xml><Encrypt>{encrypt}</Encrypt><MsgSignature>{sign}</MsgSignature><TimeStamp>1411034505</TimeStamp><Nonce>123456789</Nonce></xml>";
            var buffer = Encoding.UTF8.GetBytes(input);
            Stream stream = new MemoryStream(buffer);
            var result = await new WeChatMessageHandler(config, new MockCache())
                .AddHandlerService(new MockTextMessageHandler())
                .SetStreamMessage(stream,sign)
                .ExecutedAsync();
            Assert.True(string.IsNullOrEmpty(result) == false, "string.IsNullOrEmpty(result) == false");
            _outputHelper.WriteLine(result);
        }
    }
}