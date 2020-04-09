using System;

namespace Caster.WeChat.ServiceException
{
    public class WeChatSignException:WeChatException
    {
        public WeChatSignException(string message) : base(message)
        {
            
        }
    }
}