using System;

namespace Caster.WeChat.ServiceException
{
    public class WeChatPayException:Exception
    {

        public WeChatPayException(string message)
        {
            Message = message;
        }
        
        public override string Message { get; }
        
        
    }
}