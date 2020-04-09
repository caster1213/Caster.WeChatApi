using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Parameter.Menu;

namespace Caster.WeChat.Common
{
    public static class MenuHelper
    {
        public static JObject MenuBuild(this List<Menu> menus, MenuConditionParameter menuCondition = null)
        {
            JObject root = new JObject();
            JArray array = new JArray();
            foreach (var menu in menus)
            {
                if (menu is ChildrenMenu)
                {
                    var first = menu.ToJObject();
                    JArray children = new JArray();
                    foreach (var child in menu.Children)
                    {
                        children.Add(child.ToJObject());
                    }

                    first["name"] = menu.Name;
                    first["sub_button"] = children;
                    array.Add(first);
                }
                else
                {
                    array.Add(menu.ToJObject());
                }
            }

            root["button"] = array;
            if (menuCondition != null)
            {
                root["matchrule"] = MenuCondition(menuCondition);
            }

            return root;
        }

        private static JObject MenuCondition(MenuConditionParameter condition)
        {
            JObject rule = new JObject();
            if (string.IsNullOrEmpty(condition.City) == false)
            {
                rule["city"] = condition.City;
            }

            if (string.IsNullOrEmpty(condition.Country) == false)
            {
                rule["province"] = condition.Country;
            }

            if (string.IsNullOrEmpty(condition.Lanuage) == false)
            {
                rule["language"] = condition.Lanuage;
            }

            if (string.IsNullOrEmpty(condition.Province) == false)
            {
                rule["province"] = condition.Province;
            }

            if (string.IsNullOrEmpty(condition.Sex) == false)
            {
                rule["sex"] = condition.Sex;
            }

            if (string.IsNullOrEmpty(condition.ClientPlatform) == false)
            {
                rule["client_platform_type"] = condition.ClientPlatform;
            }

            if (string.IsNullOrEmpty(condition.TagId) == false)
            {
                rule["tag_id"] = condition.TagId;
            }

            return rule;
        }
    }
}