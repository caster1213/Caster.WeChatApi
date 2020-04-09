using System.Collections.Generic;
using System.Threading.Tasks;
using Caster.WeChat;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class UserServiceTest
    {
        private readonly ITestOutputHelper _output;
        private readonly WeChatWeb _web;
        private readonly string _token;

        public UserServiceTest(ITestOutputHelper output)
        {
            _output = output;
            _web = Helper.CreateClient();
            _token = Helper.CreateClient().GetToken().Result;
        }

        [Fact]
        public async Task CreateLabelTest()
        {
            var result = await _web.UserService.CreateLabelAsync("骑士1", _token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task GetLabelsTest()
        {
            var result = await _web.UserService.GetLabelListAsync(_token);

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task UpdateLabelTest()
        {
            var result = await _web.UserService.UpdateLabelAsync("100", "星法师", _token);

            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task DeleteLabelTest()
        {
            await _web.UserService.DeleteLabelAsync("100", _token);
        }

        [Fact]
        public async Task BatchWriteLabelForUserTest()
        {
            await _web.UserService.BatchWriteLabelForUserAsync("103", _token, new List<string>
            {
                "oVJAUwQcq8Yr_Aj3dJ8ppszxq7A8",
                "oVJAUwVamS4JkLukifvZ-fFzMvKI"
            });
        }

        [Fact]
        public async Task FindFansByLabelTest()
        {
            var result = await _web.UserService.FindFansByLabelAsync("103", _token);
            
            _output.WriteLine(result.ToString());
        }
        
        [Fact]
        public async Task BatchCancelLabelForUserTest()
        {
            await _web.UserService.BatchCancelLabelForUserAsync("103", _token, new List<string>
            {
                "oVJAUwQcq8Yr_Aj3dJ8ppszxq7A8",
                "oVJAUwVamS4JkLukifvZ-fFzMvKI"
            });
        }

        [Fact]
        public async Task UpdateRemarkForUserTest()
        {
            await _web.UserService.UpdateRemarkForUserAsync("oVJAUwVamS4JkLukifvZ-fFzMvKI", "哈哈哈", _token);
        }

        [Fact]
        public async Task GetWeChatUserInfoAsync()
        {
            var result = await _web.UserService.GetWeChatUserInfoAsync("oVJAUwVamS4JkLukifvZ-fFzMvKI", _token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task GetUserListTest()
        {
            var result = await _web.UserService.GetUserInfoListAsync(_token,new List<string>
            {
                "oVJAUwQcq8Yr_Aj3dJ8ppszxq7A8",
                "oVJAUwZoWLxfS_JaSrku0_znfNh8",
                "oVJAUwYLm8eCIrBMpG4VbQVfCwzs"
            });
            
            _output.WriteLine(result.ToString());
        }
        [Fact]
        public async Task GetWatchOpenIdListTest()
        {
            var result = await _web.UserService.GetWatchOpenIdListAsync(_token);
            
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task OpenIdByJoinBlackTest()
        {
            await _web.UserService.OpenIdByJoinBlackAsync(new List<string>
            {
                "oVJAUwZoWLxfS_JaSrku0_znfNh8",
                "oVJAUwYLm8eCIrBMpG4VbQVfCwzs"
            }, _token);
        }
        
        [Fact]
        public async Task GetBlackListTest()
        {
            var result = await _web.UserService.GetBlackListAsync(_token);
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task OpenIdByRemoveBlackTest()
        {
            await _web.UserService.OpenIdByRemoveBlackAsync(new List<string>
            {
                "oVJAUwZoWLxfS_JaSrku0_znfNh8",
                "oVJAUwYLm8eCIrBMpG4VbQVfCwzs"
            }, _token);
        }
    }
}