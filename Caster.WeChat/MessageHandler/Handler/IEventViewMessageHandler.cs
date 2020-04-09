using System.Threading.Tasks;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;

namespace Caster.WeChat.MessageHandler.Handler
{
    public interface IEventViewMessageHandler:IMessageHandler
    {
        Task<MessageResponse> Processed(ViewEventMessageRequest request);
    }
}