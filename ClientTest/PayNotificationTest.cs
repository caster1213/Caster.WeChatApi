using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using ClientTest.Mock;
using Caster.WeChat.Common;
using Caster.WeChat.MessageHandler;
using Xunit;
using Xunit.Abstractions;

namespace ClientTest
{
    public class PayNotificationTest
    {
        private readonly ITestOutputHelper _outputHelper;


        public PayNotificationTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async void PaySuccessNotificationTest()
        {
            var config = new Helper.WeChatConfig();
            string xml = @"<xml>
                <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
                <attach><![CDATA[支付测试]]></attach>
                <bank_type><![CDATA[CFT]]></bank_type>
                <fee_type><![CDATA[CNY]]></fee_type>
                <is_subscribe><![CDATA[Y]]></is_subscribe>
                <mch_id><![CDATA[10000100]]></mch_id>
                <nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str>
                <openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid>
                <out_trade_no><![CDATA[1409811653]]></out_trade_no>
                <result_code><![CDATA[SUCCESS]]></result_code>
                <return_code><![CDATA[SUCCESS]]></return_code>
                <err_code><![CDATA[SUCCESS]]></err_code>
                <err_code_des><![CDATA[SUCCESS]]></err_code_des>
                <sign><![CDATA[B552ED6B279343CB493C5DD0D78AB241]]></sign>
                <time_end><![CDATA[20140903131540]]></time_end>
                <total_fee>1</total_fee>
                <coupon_fee><![CDATA[10]]></coupon_fee>
                <coupon_count><![CDATA[1]]></coupon_count>
                <coupon_type><![CDATA[CASH]]></coupon_type>
                <coupon_id><![CDATA[10000]]></coupon_id>
                <trade_type><![CDATA[JSAPI]]></trade_type>
                <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
                </xml>";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            var signNode = xmlDocument.FirstChild.SelectSingleNode("sign");
            xmlDocument.FirstChild.RemoveChild(signNode);
            string sign = WeChatSignHelper.CreateMd5SignByXml(xmlDocument, config.Value.MerchantSecret);
            xml = xml.Replace("B552ED6B279343CB493C5DD0D78AB241", sign);
            byte[] buffer = Encoding.UTF8.GetBytes(xml);
            Stream stream = new MemoryStream(buffer);
            await new PayNotificationHandler(config.Value.MerchantId, config.Value.MerchantSecret)
                .AddHandler(new UnifiedOrderNotificationHandlerMock())
                .ExecutedAsync(stream);
        }

        [Fact]
        public async void RefundNotificationTest()
        {
            var config = new Helper.WeChatConfig();

            string xml = @"<xml>
                         <return_code>SUCCESS</return_code>
                <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
                <mch_id><![CDATA[10000100]]></mch_id>
                <nonce_str><![CDATA[TeqClE3i0mvn3DrK]]></nonce_str>
                <req_info><![CDATA[req_info_value]]></req_info>
                </xml > ";
            var root = @"<root>
<out_refund_no><![CDATA[131811191610442717309]]></out_refund_no>
<out_trade_no><![CDATA[71106718111915575302817]]></out_trade_no>
<refund_account><![CDATA[REFUND_SOURCE_RECHARGE_FUNDS]]></refund_account>
<refund_fee><![CDATA[3960]]></refund_fee>
<refund_id><![CDATA[50000408942018111907145868882]]></refund_id>
<refund_recv_accout><![CDATA[支付用户零钱]]></refund_recv_accout>
<refund_request_source><![CDATA[API]]></refund_request_source>
<refund_status><![CDATA[SUCCESS]]></refund_status>
<settlement_refund_fee><![CDATA[3960]]></settlement_refund_fee>
<settlement_total_fee><![CDATA[3960]]></settlement_total_fee>
<success_time><![CDATA[2018-11-19 16:24:13]]></success_time>
<total_fee><![CDATA[3960]]></total_fee>
<transaction_id><![CDATA[4200000215201811190261405420]]></transaction_id>
</root>";
            var md5 = MD5.Create();
            var bufferMd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(config.Value.MerchantSecret));
            StringBuilder output = new StringBuilder();
            foreach (var b in bufferMd5)
            {
                output.Append(b.ToString("x2"));
            }
            var rootBuffer = Encoding.UTF8.GetBytes(root);
            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(output.ToString().ToLower()), Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            var bufferResult = cTransform.TransformFinalBlock(rootBuffer, 0, rootBuffer.Length);
            var base64 = Convert.ToBase64String(bufferResult, 0, bufferResult.Length);
            var value = xml.Replace("req_info_value", base64);
            byte[] buffer = Encoding.UTF8.GetBytes(value);
            Stream stream = new MemoryStream(buffer);
            await new PayNotificationHandler(config.Value.MerchantId, config.Value.MerchantSecret)
                .AddHandler(new RefundNotificationHandlerMock())
                .ExecutedAsync(stream);
        }
    }
}