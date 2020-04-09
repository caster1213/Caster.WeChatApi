using System;
using Caster.WeChat.MessageHandler.Handler;
using Caster.WeChat.MessageHandler.Request;

namespace Caster.WeChat.MessageHandler
{
    public class MessageHandlerMap
    {
        public IMessageHandler Handler { get; set; }
        public Type HandlerType { get; set; }
    }
}