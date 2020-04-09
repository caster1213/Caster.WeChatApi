### Caster.WeChatApi
Caster.WeChatApi使用微软新一代跨平台框架dotnet core进行开发 ,集成了微信公众号和微信支付经常使用的接口，方便我们对接微信进行开发。

例如 模板消息、菜单、消息群发、素材上传、文章评论、客服消息、用户接口、支付、红包、提现。    
    支付回调处理 消息回调处理    


### 背景
本人经常开发微信相关的项目，而且每次需要对接的接口都不一样，导致每次踩过坑的接口分布在不同的项里。
有的接口每次都掺杂了业务导致每次都要复制黏贴，为了统一标准就重新对微信接口封装了一遍。
方便与以后的项目开发所使用。

### 如何安装
```
> dotnet add package caster.wechatapi
```
### 如何使用
注入 WeChatClient 
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // 使用默认ApiClient
    services.AddWeChatClient();
    
    // 使用自定义AccessToken存储ApiClient
    services.AddWeChatClient<ITokenRepository>();
    
    //使用自定义AccessToken存储和自定义缓存ApiClient
    services.AddWeChatClient<ITokenRepository,ICache>();
}
```
接受微信消息和微信支付通知
```csharp
public class MessageController:Controller
{
    private readonly WeChatWeb _client;
    public MessageController(WeChatWeb client)
    {
        _client = client
    }
    
    [HttpPost]
    public async Task<IActionResult> Push()
    {
         // 获取消息Handler  
         var handler = _client.GetWeChatMessageHandler();
         var stream = Request.Body;
         var sign = Request.Query["signature"].ToString();//消息签名
         var timestamp = Request.Query["timestamp"].ToString();//时间戳
         var nonce = Request.Query["nonce"].ToString();//随机字符串
         
         /*
         * 添加了2个Handler
         * 一个用于处理文本消息的Handler
         * 一个用于处理菜单点击事件的Handler
         * 设置微信推送来的数据 SetStreamMessage
         * SetSignValue 效验微信消息签名
         */
         var result = await handler.AddHandlerService(new TextMessageHandler())
             .AddHandlerService(new ClickMenuHandler())
             .SetStreamMessage(stream)
             .SetSignValue(sign,timestamp,nonce)
             .Executed();
         return Content(result, "text/xml");
    }
    [HttpGet]
    public IActionResult Push([FormQuery] SignRequest reqeust)
    {
        //为了让微信测试通过，这里直接返回微信发送来的echostr即可
        return Content(Request.Query["echostr"].ToString());
    }
    
    /// <summary>
    /// 微信支付和退款通知
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PayCallback()
    {
        var body = Request.Body;
        var handler = _client.GetPayNotificationHandler();
        /*
        * 添加了2个Handler
        * 一个用于微信支付通知的的Handler
        * 一个用于微信支付退款通知的的Handler
        */
        var result = await handler.AddHandler(new PayCallbackHandler())
                .AddHandler(new RefundCallbackHandler())
                .ExecutedAsync(body);
        return Content(result, "text/xml");
    }
    /// <summary>
    /// 发送模板消息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> SendTemplateMessage()
    {
        string openId = string.Empty;
        string templateId = string.Empty;
        TemplateMessageParameter parameter = new TemplateMessageParameter
        {
            First = new FieldConfig("blue", "您的订单通知"),
            Content =
            {
                new FieldConfig("blue", "订单编号"),
                new FieldConfig("blue", "快递单号")
            },
            Remark = new FieldConfig("red", "谢谢使用")
        };
        string token = await _client.GetToken();
        var result = await _client.TemplateMessageService.SendAsync(openId,templateId, token, parameter);
        return Content(result);
 }

    
}
public class TextMessageHandler : ITextMessageHandler
{
    public async Task<MessageResponse> Processed(TextMessageRequest request)
    {
        return new TextMessageResponse();
    }
}

public  class ClickMenuHandler : IEventClickMessageHandler
{
    public async Task<MessageResponse> Processed(ClickEventMessageRequest request)
    {
        return new TextMessageResponse();
    }
}

public class PayCallbackHandler:IUnifiedOrderNotificationHandler
{
    public async Task<PayResponse> SuccessExecuted(UnifiedOrderNotificationRequest notification)
    {
        return new PayResponse();
    }
}
public class RefundCallbackHandler : IRefundNotificationHandler
{
    public async Task<PayResponse> SuccessExecuted(RefundNotificationRequestnotification)
    {
        return new PayResponse();
    }
}

```
 
### 消息处理接口
 接口名称 | 接口描述 | 参数 
 ------------------- | --------- | ------
 ITextMessageHandler | 文本消息 |TextMessageRequest
 IImageMessageHandler| 图片消息 |ImageMessageRequest
 IVideoMessageHandler| 视频消息 |VideoMessageRequest
 IVoiceMessageHandler| 语音消息 |VoiceMessageRequest
 ILikeMessageHandler| 超链接消息 |LikeMessageRequest
 ILocationMessageHandler| 位置消息 | LocationMessageRequest
 IMiniVideoMessageHandler| 小视频消息 | MiniVideoMessageRequest
 IEventSubscribeMessageHandler| 用户关注 | SubscribeEventMessageRequest
 IEventUnsubscribeMessageHandler| 用户取消关注 | UnsubscribeEventMessageRequest
 IEventScanSubscribeMessageHandler| 用户扫码关注 |ScanSubscribeEventMessageRequest
 IEventScanMessageHandler| 用户扫码 |ScanEventMessageRequest
 IEventUploadLocationMessageHandler| 上传地理位置 |UploadLocationEventMessageRequest
 IEventClickMessageHandler|点击菜单|ClickEventMessageRequest
 IEventLocationMessageHandler| 地理位置|LocationEventMessageRequest
 IEventMiniProgramMessageHandler |小程序跳转|MiniProgramEventMessageRequest
 IEventPhotoAlbumMessageHandler| 弹出拍照或者相册发图|PhotoAlbumEventMessageRequest
 IEventPhotoMessageHandler|弹出系统拍照发图事件|PhotoEventMessageRequest
 IEventPhotoWeChatMessageHandler|弹出微信相册发图事件|PhotoWeChatEventMessageRequest
 IEventScanCodeMessageHandler|扫码事件 |ScanCodeEventMessageRequest
 IEventScanCodingMessageHandler|扫码事件弹出提示框事件 |ScanCodingEventMessageRequest
 IEventSendCompleteMessageHandler|模板消息通知事件| EventSendCompleteMessageRequest
 IEventViewMessageHandler|菜单URL跳转事件|ViewEventMessageRequest
 
### 支付通知接口
 
 接口名称 | 接口描述 | 参数 
 ------------------- | --------- | ------
 IEventViewMessageHandler|微信支付通知|UnifiedOrderNotificationRequest
 IPayNotificationHandler|微信支付退款通知|ViewEventMessageRequest

### Api服务
接口名称 | 接口描述
------------------- | ---------
CommentService | 文章评论
CommonService | 基础
CustomService | 客服消息
FileService | 素材
MenuService | 菜单
MessageAllService | 群发
TemplateMessageService |模板消息
UserService|用户
PayService |支付

### 相关项目
[![standard-readme compliant](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

### 维护者
[@Caster](https://github.com/caster1213)
### 使用许可
MIT




