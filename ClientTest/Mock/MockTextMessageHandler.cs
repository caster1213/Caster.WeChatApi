using System.Threading.Tasks;
using Caster.WeChat.MessageHandler.Handler;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;

namespace ClientTest.Mock
{
    public class MockTextMessageHandler:ITextMessageHandler
    {
        public Task<MessageResponse> Processed(TextMessageRequest request)
        {
            MessageResponse response = new TextMessageResponse
            {
                ToUserName = request.ToUserName,
                FormUserName = request.FormUserName,
                CreateTime = request.CreateTime,
                Content = "回复"
            };
            return Task.FromResult(response);
        }
    }
}