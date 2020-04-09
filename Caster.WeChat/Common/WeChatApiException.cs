using System;

namespace Caster.WeChat.Common
{
    public class WeChatApiException:Exception
    {
        private readonly string _message;
        public override string Message
        {
            get
            {
                string str = $" 错误码 {Code}  错误描述 {_message}";

                return str;
            }
        }
        public string Code { get; }
        public string MethodName { get; }

        public WeChatApiException(string code,string message,string methodName)
        {
            _message = message;
            Code = code;
            MethodName = methodName;
        }
    }
}