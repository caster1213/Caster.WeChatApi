namespace Caster.WeChat.Parameter
{
    public class MessageValidateRequest
    {
        public string Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
        public string Echostr { get; set; }
    }
}