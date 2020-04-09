using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Caster.WeChat.Common;
using Caster.WeChat.Parameter.Menu;

namespace Caster.WeChat.Service
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class MenuService
    {
        private readonly IClient _client;

        public MenuService(IClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> CreateMenuAsync(List<Menu> menus, string accessToken)
        {
            var menuNode = menus.MenuBuild();
            var response = await _client.ExecutePostRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/menu/create"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, menuNode.ToString());

            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
            return root.ToObject<dynamic>();
        }

        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="condition"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> CreateMenuAsync(List<Menu> menus, MenuConditionParameter condition,
            string accessToken)
        {
            var menuNode = menus.MenuBuild(condition);
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/menu/addconditional"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, menuNode.ToString());
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
            return root.ToObject<dynamic>();
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<dynamic> GetMenuAsync(string accessToken)
        {
            var response = await _client.ExecuteGetRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/menu/get"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                });
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
            return root;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task DeleteMenuAsync(string accessToken)
        {
            var response = await _client.ExecuteGetRequest(WeChatHelper.GetWeChatApiDomain("/cgi-bin/menu/delete"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                });
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task DeleteConditionMenuAsync(string menuId, string accessToken)
        {
            var jsonObject = new
            {
                menuid = menuId
            };
            var response = await _client.ExecutePostRequest(
                WeChatHelper.GetWeChatApiDomain("/cgi-bin/menu/delconditional"),
                new Dictionary<string, string>
                {
                    {
                        WeChatConstant.AccessToken, accessToken
                    }
                }, JsonConvert.SerializeObject(jsonObject));
            JObject root = JObject.Parse(response);
            WeChatHelper.CheckResponseStatus(root);
        }
    }
}