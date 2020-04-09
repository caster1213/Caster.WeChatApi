using System;

namespace Caster.WeChat.ServiceException
{
    /// <summary>
    /// 消息加密解密异常信息
    /// </summary>
    public class WeChatCryptographyException : Exception
    {
        public WeChatCryptographyException(string message)
        {
            Message = message;
        }


        public override string Message { get; }
    }
}