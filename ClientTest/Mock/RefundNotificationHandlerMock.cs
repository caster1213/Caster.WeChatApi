using System.Threading.Tasks;
using Caster.WeChat.MessageHandler.Handler;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;

namespace ClientTest.Mock
{
    public class RefundNotificationHandlerMock:IRefundNotificationHandler
    {
        public Task<PayResponse> SuccessExecuted(RefundNotificationRequest notification)
        {
            return Task.FromResult(new PayResponse());
        }
    }
}