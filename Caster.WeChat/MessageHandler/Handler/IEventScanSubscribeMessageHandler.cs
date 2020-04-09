using System.Threading.Tasks;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;

namespace Caster.WeChat.MessageHandler.Handler
{
    public interface IEventScanSubscribeMessageHandler:IMessageHandler
    {
        Task<MessageResponse> Processed(ScanSubscribeEventMessageRequest request);
    }
}