using Newtonsoft.Json.Linq;

namespace Caster.WeChat.Parameter.Menu
{
    public class MiniMenu:Menu
    {
        public MiniMenu(string appId,string pagePath,string url,string name):base(name)
        {
            AppId = appId;
            PagePath = pagePath;
            Url = url;
            TypeName = "miniprogram";
        }
        public string AppId { get; }
        public string PagePath { get; }
        public string Url { get; }
        
        public override JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject["name"] = Name;
            jObject["url"] = Url;
            jObject["type"] = TypeName;
            jObject["appid"] = TypeName;
            jObject["pagepath"] = TypeName;
            return jObject;
        }
    }
}