using System.Threading.Tasks;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;

namespace Caster.WeChat.MessageHandler.Handler
{
    public interface IEventScanCodeMessageHandler : IMessageHandler
    {
        Task<MessageResponse> Processed(ScanCodeEventMessageRequest request);
    }
}