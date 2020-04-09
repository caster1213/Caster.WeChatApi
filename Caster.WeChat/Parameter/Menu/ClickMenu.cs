using Newtonsoft.Json.Linq;

namespace Caster.WeChat.Parameter.Menu
{
    public class ClickMenu:Menu
    {
        public ClickMenu(string key,string name):base(name)
        {
            Key = key;
            TypeName = "click";
        }
        public string Key { get; }
        public override JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject["name"] = Name;
            jObject["key"] = Key;
            jObject["type"] = TypeName;
            return jObject;
        }
    }
}