using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Caster.WeChat.Common;
using Caster.WeChat.MessageHandler.Response;

namespace Caster.WeChat.MessageHandler
{
    public class WeChatActionResult : ActionResult
    {
        private readonly MessageResponse _value;

        public WeChatActionResult(MessageResponse response)
        {
            _value = response;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.OK;
            if (_value == null)
            {
                context.HttpContext.Response.ContentType = "text/plain";
                context.HttpContext.Response.Body =  new MemoryStream(Encoding.UTF8.GetBytes("success"));
            }
            else
            {
                string xml = XmlSerializeHelper.ObjectToXmlString(_value);
                context.HttpContext.Response.ContentType = "text/xml";
                context.HttpContext.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            }
            return Task.CompletedTask;
        }
    }
}