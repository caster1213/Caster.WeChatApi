using Newtonsoft.Json.Linq;

namespace Caster.WeChat.Parameter.Menu
{
    public class ViewMenu:Menu
    {
        public ViewMenu(string url,string name):base(name)
        {
            Url = url;
            TypeName = "view";
        }
        public string Url { get; }
        public override JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject["name"] = Name;
            jObject["url"] = Url;
            jObject["type"] = TypeName;
            return jObject;
        }
    }
}