using System.Threading.Tasks;
using Caster.WeChat.MessageHandler.Handler;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;

namespace ClientTest.Mock
{
    public class UnifiedOrderNotificationHandlerMock:IUnifiedOrderNotificationHandler
    {
        public Task<PayResponse> SuccessExecuted(UnifiedOrderNotificationRequest notification)
        {
            return Task.FromResult(new PayResponse());
        }
    }
}