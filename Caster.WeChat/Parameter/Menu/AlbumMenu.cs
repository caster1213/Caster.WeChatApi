using Newtonsoft.Json.Linq;

namespace Caster.WeChat.Parameter.Menu
{
    public class AlbumMenu:Menu
    {
        public AlbumMenu(string key,string name):base(name)
        {
            Key = key;
            TypeName = "pic_photo_or_album";
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