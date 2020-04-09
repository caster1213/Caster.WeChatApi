using System.Collections.Generic;
using System.Threading.Tasks;
using Caster.WeChat;
using Caster.WeChat.Parameter.Menu;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class MenuServiceTest
    {
        private readonly WeChatWeb _web;
        private readonly string _token;
        private readonly ITestOutputHelper _output;

        public MenuServiceTest(ITestOutputHelper output)
        {
            _web = Helper.CreateClient();
            _output = output;
            _token = _web.GetToken().Result;
        }

        [Fact]
        public async Task CreateConditionMenuTest()
        {
            await _web.MenuService.CreateMenuAsync(new List<Menu>
            {
                new ChildrenMenu("测试1")
                {
                    Children = new List<Menu>
                    {
                        new ViewMenu("http://www.baidu.com", "测试101"),
                        new ViewMenu("http://www.baidu.com", "测试102"),
                        new ViewMenu("http://www.baidu.com", "测试103"),
                    }
                },
                new ChildrenMenu("测试2")
                {
                    Children = new List<Menu>
                    {
                        new ViewMenu("http://www.baidu.com", "测试女201"),
                        new ViewMenu("http://www.baidu.com", "测试女202"),
                        new ViewMenu("http://www.baidu.com", "测试女203"),
                    }
                },
                new ChildrenMenu("测试3")
                {
                    Children = new List<Menu>
                    {
                        new ViewMenu("http://www.baidu.com", "测试301"),
                        new ViewMenu("http://www.baidu.com", "测试302"),
                        new ViewMenu("http://www.baidu.com", "测试303"),
                    }
                }
            }, new MenuConditionParameter
            {
                Sex = "2"
            }, _token);
        }

        [Fact]
        public async Task CreateMenuTest()
        {
           var result =  await _web.MenuService.CreateMenuAsync(new List<Menu>
            {
                new ChildrenMenu("测试1")
                {
                    Children = new List<Menu>
                    {
                        new ViewMenu("http://www.baidu.com", "测试101"),
                        new ViewMenu("http://www.baidu.com", "测试102"),
                        new ViewMenu("http://www.baidu.com", "测试103"),
                    }
                },
                new ChildrenMenu("测试2")
                {
                    Children = new List<Menu>
                    {
                        new ViewMenu("http://www.baidu.com", "测试女201"),
                        new ViewMenu("http://www.baidu.com", "测试女202"),
                        new ViewMenu("http://www.baidu.com", "测试女203"),
                    }
                },
                new ChildrenMenu("测试3")
                {
                    Children = new List<Menu>
                    {
                        new ViewMenu("http://www.baidu.com", "测试301"),
                        new ViewMenu("http://www.baidu.com", "测试302"),
                        new ViewMenu("http://www.baidu.com", "测试303"),
                    }
                }
            }, _token);
           
           _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task GetMenuTest()
        {
            var result = await _web.MenuService.GetMenuAsync(_token);

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task DeleteMenuTest()
        {
            await _web.MenuService.DeleteConditionMenuAsync("434668644", _token);
        }

        [Fact]
        public async Task DeleteAllMenu()
        {
            await _web.MenuService.DeleteMenuAsync(_token);
        }
    }
}