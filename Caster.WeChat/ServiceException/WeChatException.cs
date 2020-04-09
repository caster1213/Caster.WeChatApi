using System;

namespace Caster.WeChat.ServiceException
{
    public class WeChatException:Exception
    {
        public override string Message { get; }


        public WeChatException(string message)
        {
            Message = message;
        }
    }
}