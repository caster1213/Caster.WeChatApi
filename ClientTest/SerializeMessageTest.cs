using System.Xml;
using Caster.WeChat.MessageHandler.Request;
using Xunit;

namespace ClientTest
{
    public class SerializeMessageTest
    {
        [Fact]
        public void TextMessage()
        {
            string str = @"<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[fromUser]]></FromUserName>
  <CreateTime>1348831860</CreateTime>
  <MsgType><![CDATA[text]]></MsgType>
  <Content><![CDATA[this is a test]]></Content>
  <MsgId>1234567890123456</MsgId>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new TextMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void ImageMessage()
        {
            string str = @"<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[fromUser]]></FromUserName>
  <CreateTime>1348831860</CreateTime>
  <MsgType><![CDATA[image]]></MsgType>
  <PicUrl><![CDATA[this is a url]]></PicUrl>
  <MediaId><![CDATA[media_id]]></MediaId>
  <MsgId>1234567890123456</MsgId>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new ImageMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void VoiceMessage()
        {
            string str = @"<xml>
                         <ToUserName><![CDATA[toUser]]></ToUserName>
                <FromUserName><![CDATA[fromUser]]></FromUserName>
                <CreateTime>1357290913</CreateTime>
                <MsgType><![CDATA[voice]]></MsgType>
                <MediaId><![CDATA[media_id]]></MediaId>
                <Format><![CDATA[Format]]></Format>
                <MsgId>1234567890123456</MsgId>
                </xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new VoiceMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void VideoMessage()
        {
            string str = @"<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[fromUser]]></FromUserName>
  <CreateTime>1357290913</CreateTime>
  <MsgType><![CDATA[video]]></MsgType>
  <MediaId><![CDATA[media_id]]></MediaId>
  <ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
  <MsgId>1234567890123456</MsgId>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new VideoMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void MiniVideoMessage()
        {
            string str = @"<xml>
                         <ToUserName><![CDATA[toUser]]></ToUserName>
                <FromUserName><![CDATA[fromUser]]></FromUserName>
                <CreateTime>1357290913</CreateTime>
                <MsgType><![CDATA[shortvideo]]></MsgType>
                <MediaId><![CDATA[media_id]]></MediaId>
                <ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
                <MsgId>1234567890123456</MsgId>
                </xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new MiniVideoMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void LocationMessage()
        {
            string str = @"<xml>
                         <ToUserName><![CDATA[toUser]]></ToUserName>
                <FromUserName><![CDATA[fromUser]]></FromUserName>
                <CreateTime>1351776360</CreateTime>
                <MsgType><![CDATA[location]]></MsgType>
                <Location_X>23.134521</Location_X>
                <Location_Y>113.358803</Location_Y>
                <Scale>20</Scale>
                <Label><![CDATA[位置信息]]></Label>
                <MsgId>1234567890123456</MsgId>
                </xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new LocationMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void LinkMessage()
        {
            string str = @"<xml>
                         <ToUserName><![CDATA[toUser]]></ToUserName>
                <FromUserName><![CDATA[fromUser]]></FromUserName>
                <CreateTime>1351776360</CreateTime>
                <MsgType><![CDATA[link]]></MsgType>
                <Title><![CDATA[公众平台官网链接]]></Title>
                <Description><![CDATA[公众平台官网链接]]></Description>
                <Url><![CDATA[url]]></Url>
                <MsgId>1234567890123456</MsgId>
                </xml>";
            
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new LinkMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void SubscribeEventMessage()
        {
            string str = @"<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[FromUser]]></FromUserName>
  <CreateTime>123456789</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[subscribe]]></Event>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new SubscribeEventMessageRequest().SerializationMessage(xml);
        }
        
        [Fact]
        public void UnsubscribeEventMessage()
        {
            string str = @"<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[FromUser]]></FromUserName>
  <CreateTime>123456789</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[unsubscribe]]></Event>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new UnsubscribeEventMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void ScanSubscribeEventMessage()
        {
            string str = @"<xml>
                        <ToUserName><![CDATA[toUser]]></ToUserName>
                <FromUserName><![CDATA[FromUser]]></FromUserName>
                <CreateTime>123456789</CreateTime>
                <MsgType><![CDATA[event]]></MsgType>
                <Event><![CDATA[subscribe]]></Event>
                <EventKey><![CDATA[qrscene_123123]]></EventKey>
                <Ticket><![CDATA[TICKET]]></Ticket>
                </xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new ScanSubscribeEventMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void UploadLocationEventMessage()
        {
            string str = @"<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[fromUser]]></FromUserName>
  <CreateTime>123456789</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[LOCATION]]></Event>
  <Latitude>23.137466</Latitude>
  <Longitude>113.352425</Longitude>
  <Precision>119.385040</Precision>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new UploadLocationEventMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void ClickMenuEventMessage()
        {
            string str = @"<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[FromUser]]></FromUserName>
  <CreateTime>123456789</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[CLICK]]></Event>
  <EventKey><![CDATA[EVENTKEY]]></EventKey>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new ClickEventMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void ViewMenuEventMessage()
        {
            string str = @"<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[FromUser]]></FromUserName>
  <CreateTime>123456789</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[VIEW]]></Event>
  <EventKey><![CDATA[www.qq.com]]></EventKey>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new ViewEventMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void ScanCodeEventMessage()
        {
            string str = @"<xml><ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
                         <FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
                <CreateTime>1408090502</CreateTime>
                <MsgType><![CDATA[event]]></MsgType>
                <Event><![CDATA[scancode_push]]></Event>
                <EventKey><![CDATA[6]]></EventKey>
                <ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
                <ScanResult><![CDATA[1]]></ScanResult>
                </ScanCodeInfo>
                </xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new ScanCodeEventMessageRequest().SerializationMessage(xml);
            
        }

        [Fact]
        public void PhotoEventMessage()
        {
            string str = @"<xml><ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090651</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_sysphoto]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[1b5f7c23b5bf75682a53e7b6d163e185]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new PhotoEventMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void ClickMiniProgramEventMessage()
        {
            string str = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[view_miniprogram]]></Event>
<EventKey><![CDATA[pages/index/index]]></EventKey>
<MenuId>MENUID</MenuId>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new MiniProgramEventMessageRequest().SerializationMessage(xml);
        }

        [Fact]
        public void LocationSelectEventMessage()
        {
            string str = @"<xml><ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408091189</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[location_select]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendLocationInfo><Location_X><![CDATA[23]]></Location_X>
<Location_Y><![CDATA[113]]></Location_Y>
<Scale><![CDATA[15]]></Scale>
<Label><![CDATA[ 广州市海珠区客村艺苑路 106号]]></Label>
<Poiname><![CDATA[]]></Poiname>
</SendLocationInfo>
</xml>";
            var xml = new XmlDocument();
            xml.LoadXml(str);
            new LocationEventMessageRequest().SerializationMessage(xml);
        }
    }
}