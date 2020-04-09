using Newtonsoft.Json.Linq;

namespace Caster.WeChat.Parameter.Menu
{
    public class ScanCodePushMenu:Menu
    {
        public ScanCodePushMenu(string key,string name):base(name)
        {
            Key = key;
            TypeName = "scancode_waitmsg";
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