using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Caster.WeChat.Parameter.Menu
{
    public abstract class Menu
    {

        public Menu(string name)
        {
            Name = name;
            Children = new List<Menu>();
        }
        
        public string Name { get; set; }
        protected string TypeName { get; set; }
        public List<Menu> Children { get; set; }

        public abstract JObject ToJObject();
    }
}