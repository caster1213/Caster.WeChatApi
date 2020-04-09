using Newtonsoft.Json.Linq;

namespace Caster.WeChat.Parameter.Menu
{
    public class ChildrenMenu:Menu
    {
        public override JObject ToJObject()
        {
            JObject jObject = new JObject();

            jObject["name"] = Name;

            return jObject;
        }

        public ChildrenMenu(string name) : base(name)
        {
        }
    }
}