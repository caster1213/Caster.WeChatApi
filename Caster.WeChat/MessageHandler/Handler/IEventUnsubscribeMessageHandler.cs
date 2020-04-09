using System.Threading.Tasks;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;

namespace Caster.WeChat.MessageHandler.Handler
{
    public interface IEventUnsubscribeMessageHandler : IMessageHandler
    {
        Task<MessageResponse> Processed(UnsubscribeEventMessageRequest request);
    }
}