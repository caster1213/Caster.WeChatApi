using System;

namespace Caster.WeChat.ServiceException
{
    public class WeChatMessageDistinctException:WeChatException
    {
        public WeChatMessageDistinctException(string message) : base(message)
        {
        }
    }
}