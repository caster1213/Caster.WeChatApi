using System.Threading.Tasks;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;

namespace Caster.WeChat.MessageHandler.Handler
{
    public interface IPayNotificationHandler<in TNotification>  where TNotification:PayNotificationRequest
    {
        Task<PayResponse> SuccessExecuted(TNotification notification);
    }
}