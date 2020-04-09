using System.Reflection;
using System.Threading.Tasks;
using Caster.WeChat;
using Caster.WeChat.Common;
using Caster.WeChat.Parameter;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class PayServicesTest
    {
        private readonly WeChatWeb _web;
        private readonly ITestOutputHelper _output;

        public PayServicesTest(ITestOutputHelper output)
        {
            _web = Helper.CreateClient();
            _output = output;
        }

        [Fact]
        public async Task UnifiedOrder()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            string orderCode = Caster.WeChat.Common.Helper.GetTimestamp().ToString();
            var parameter = new UnifiedOrderRequest(nonce, "测试", orderCode, 100, "123.185.181.156",
                "http://www.eyu360.com/WeChatPay/NormalPayCallback")
            {
                OpenId = "oFQVa1aoCaRWMHh-y15eg32JXhmU"
            };
            var result = await _web.PayService.UnifiedOrderAsync(parameter);

            Assert.True(
                result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess,
                "result.ReturnCode ==WeChatConstant.PaySuccess && result.ResultCode ==WeChatConstant.PaySuccess");
            _output.WriteLine(orderCode);
        }

        [Fact]
        public async Task UnifiedOrderNative()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            string orderCode = Caster.WeChat.Common.Helper.GetTimestamp().ToString();
            var parameter = new UnifiedOrderRequest(nonce, "测试", orderCode, 100, "123.185.181.156",
                "http://www.eyu360.com/WeChatPay/NormalPayCallback", "NATIVE")
            {
                ProductId = "0213"
            };
            var result = await _web.PayService.UnifiedOrderAsync(parameter);

            Assert.True(
                result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess,
                "result.ReturnCode ==WeChatConstant.PaySuccess && result.ResultCode ==WeChatConstant.PaySuccess");
            _output.WriteLine(result.CodeUrl);
        }

        [Fact]
        public async Task CloseOrder()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            string orderCode = "1582626752709";
            var parameter = new CloseOrderRequest
            {
                OrderCode = orderCode,
                Nonce = nonce
            };

            var result = await _web.PayService.CloseOrderAsync(parameter);
            Assert.True(
                result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess,
                "result.ReturnCode ==WeChatConstant.PaySuccess && result.ResultCode ==WeChatConstant.PaySuccess");
        }

        [Fact]
        public async Task GetOrder()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            string orderCode = "1582626752709";
            var parameter = new QueryOrderRequest
            {
                OrderCode = orderCode,
                Nonce = nonce
            };
            var result = await _web.PayService.GetOrderAsync(parameter);
            Assert.True(
                result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess,
                "result.ReturnCode ==WeChatConstant.PaySuccess && result.ResultCode ==WeChatConstant.PaySuccess");
        }

        [Fact]
        public async Task DownBill()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            string date = "20200224";
            var parameter = new QueryOrderBillRequest
            {
                Date = date,
                Nonce = nonce,
                Type = "ALL"
            };

            var result = await _web.PayService.GetOrderBillAsync(parameter);

            Assert.True(result != null, "result!=null");
        }

        [Fact]
        public async Task GetFundBill()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            string date = "20200224";
            var parameter = new QueryFundBillRequest
            {
                Date = date,
                Nonce = nonce,
                AccountType = "Basic"
            };
            var result = await _web.PayService.GetFundBillAsync(parameter);

            Assert.True(result != null, "result!=null");
        }

        [Fact]
        public async Task PayToBank()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            string orderCode = "15826267527091";
            string value =
                "jlThTKK4wYy1TEubwX+rQDCKSVQtFf2nt0BYN2i7w5S3/1d3T8YhmbUAQX9CJ3fqmEZ3CZBiQ6UCZovllhci/W8+6UsZoYwTVJtBdY6TX1Olkpx7cm/IkR7vpTcIZraMdY0mifA7GBN55w9DuSlcm133u7T8LqzPuH1lcxb4KklC98Zc3mSYIekroU+8rMCT1DrH2K4/1jS/YsBnV/N6TNf/xxXDv3r4UWVgLlmm5BaiLwtLrvPZ0z0apqb1i3TMPPoDOavMBCwk9m9lwgCj+NLkYTE2oIwC/qxWpSf1jooXAEkNa050J/NLKTKGNxlg2+BG0wDCVz6HA5yP+ZQlCA==";
            var parameter = new PayToBankRequest
            {
                Nonce = nonce,
                OrderCode = orderCode,
                Amount = 10,
                BankCode = "IM/CPmlhq/omTjOS7wT+TOIoqfsOB+fnPZyNXkNkBOw0vZqTJ/o/6All21GOCHiYk1MqEOzUvvAhrBeXIcoUQcqttQDhFwTJ9BQKl0B5/YvpEfgfk6jwf8tVIEm4miiqvvQ5a5uYpBO0fwQuW+6vTpXN3EkLLoT/FRTk9VHaG+XnvST3vmhU7PT24H6AjAeVZ2HFLfMfY4R7Lp884RHNqQDJSLETmXRvsQ2mCF8e9KkcGpgGSPNWgUSqO9j5a2ueCUMqap2rhrzfAm/+YycuO28FREk0pgPwNzJ22tUuoREjOgnV8G+m7ZWBoTZSRKMP7DAdH+RB+XOo2epsVkXj6g==",
                AccountName = "0adTBPwIjBxR6x5EDElXwZh7pWekhlrJWS9+yZ38ziPe0f3vCoq+iaCllDX2GN0/pSHlAoc82p/BRBO1srXENlzQ2eKM2qU79Uv8hplsVNcvEhvBCiyq1gmd33aEHUiTXd1c0UCGs7Qka59+W7/4wWhIjxlDJife5HxmdfSYL4kmRzOGO6G2N1qMui+SW6qb6RV7miN2IytEys2oPeOle5U/L9Npg3SlSRuuAaG9rfl6uenvyLdWjR5MKL6qBNXbPT+IK/WWsEqJYZ7gCUi9Y4wi+RcW/aWFjBi+gN1pdTXyJ2n0II3BlvVU4S2QXhESzAhKg+lkGyv1EKFt++YsLg==",
                BankName = "1026",
                Desc = "测试1分钱"
            };
            var result = await _web.PayService.PayToBankAsync(parameter);
            
            Assert.True(
                result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess,
                "result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess");
        }

        [Fact]
        public async Task PayToChange()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            string orderCode = "1582626752709";
            var parameter = new PayToWalletRequest
            {
                Nonce = nonce,
                OrderCode = orderCode,
                Amount = 100,
                Remark = "测试1分钱",
                CheckType = "NO_CHECK",
                UserName = "光头强",
                Ip = "123.185.180.28",
                OpenId = "oFQVa1dHuhLqYvvRk1NcJOyuS_sQ",
                DriverInfo = ""
            };
            var result = await _web.PayService.PayToWalletAsync(parameter);

            Assert.True(result != null, "result!=null");
        }

        [Fact]
        public async Task QueryPayToChangeResult()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            var result = await _web.PayService.GetPayToWalletResultAsync(new QueryPayToWalletRequest
            {
                OrderCode = "1582626752709",
                Nonce = nonce
            });

            Assert.True(
                result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess,
                "result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess");
        }

        [Fact]
        public async Task QueryPayToBankResult()
        {
            string nonce = Caster.WeChat.Common.Helper.GetNonceStr(32);
            var result = await _web.PayService.GetPayToBankResultAsync(new QueryPayToBankRequest()
            {
                OrderCode = "15826267527091",
                Nonce = nonce
            });
            Assert.True(
                result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess,
                "result.ReturnCode == WeChatConstant.PaySuccess && result.ResultCode == WeChatConstant.PaySuccess");
        }

        [Fact]
        public async Task GetPublicKey()
        {
            string result = await _web.PayService.GetPublicKeyAsync();
            _output.WriteLine(result);
            Assert.True(string.IsNullOrEmpty(result) == false);
        }
    }
}