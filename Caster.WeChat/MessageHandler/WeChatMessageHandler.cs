using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Caster.WeChat.Common;
using Caster.WeChat.Config;
using Caster.WeChat.MessageHandler.Handler;
using Caster.WeChat.MessageHandler.Request;
using Caster.WeChat.MessageHandler.Response;
using Caster.WeChat.ServiceException;

namespace Caster.WeChat.MessageHandler
{
    public class WeChatMessageHandler
    {
        private MessagePushType _currentMessagePushType;
        private MessageType _currentMessageType;
        private readonly ApiOption _messageConfig;
        private readonly List<MessageHandlerMap> _registerMessageHandlers;
        private readonly Dictionary<MessageType, Type> _messageHandler;

        private readonly Dictionary<MessageType, MessageRequest> _messageBody;

        private readonly ICache _cache;

        private string _signature;
        private string _nonce;
        private string _timestamp;
        private bool _openSignValidate;

        public WeChatMessageHandler(ApiOption messageConfig, ICache cache)
        {
            _messageConfig = messageConfig;
            _registerMessageHandlers = new List<MessageHandlerMap>();
            _messageBody = new Dictionary<MessageType, MessageRequest>();
            _messageHandler =  new Dictionary<MessageType, Type>();
            _cache = cache;
            InitHandlerMap();
            InitMessageRequest();
        }

        /// <summary>
        /// 添加消息处理器
        /// </summary>
        /// <param name="handler">处理器</param>
        /// <typeparam name="THandlerService">处理器类型</typeparam>
        /// <returns>消息处理器</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public WeChatMessageHandler AddHandlerService<THandlerService>(THandlerService handler)
            where THandlerService : IMessageHandler
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            _registerMessageHandlers.Add(new MessageHandlerMap
            {
                Handler = handler,
                HandlerType = typeof(THandlerService)
            });
            return this;
        }

        private IMessageHandler GetHandler(Type type)
        {
            foreach (var register in _registerMessageHandlers)
            {
                var interfaces = register.HandlerType.GetInterfaces();
                if (interfaces.Any(x => x == type))
                {
                    return register.Handler;
                }
            }

            return null;
        }


        private void SerializeMessage(string value)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(value);
            XmlNode node = document.FirstChild.SelectSingleNode("MsgType");
            var messageName = string.Empty;
            if (node.InnerText == "event")
            {
                _currentMessagePushType = MessagePushType.Event;
                var eventNode = document.FirstChild.SelectSingleNode("Event");
                var eventKeyNode = document.FirstChild.SelectSingleNode("EventKey");
                if (eventNode.InnerText == MessageType.EventSubscribe.GetDescriptionValue())
                {
                    messageName = eventKeyNode != null
                        ? MessageType.EventScanSubscribe.GetDescriptionValue()
                        : eventNode.InnerText;
                }
                else if (eventNode.InnerText.Equals("location", StringComparison.CurrentCultureIgnoreCase))
                {
                    messageName = MessageType.EventUploadLocation.GetDescriptionValue();
                }
            }
            else
            {
                if (node.InnerText.Equals("location", StringComparison.CurrentCultureIgnoreCase))
                    messageName = MessageType.Location.GetDescriptionValue();
                else messageName = node.InnerText;

                _currentMessagePushType = MessagePushType.Message;
            }

            foreach (MessageType msg in Enum.GetValues(typeof(MessageType)))
            {
                if (msg.GetDescriptionValue().Equals(messageName))
                {
                    _currentMessageType = msg;
                    _messageBody[msg].SerializationMessage(document);
                    return;
                }
            }
        }

        /// <summary>
        /// 设置微信签名数据
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <returns></returns>
        public WeChatMessageHandler SetSignValue(string signature, string timestamp, string nonce)
        {
            _signature = signature;
            _timestamp = timestamp;
            _nonce = nonce;
            _openSignValidate = true;
            return this;
        }

        /// <summary>
        /// 设置数据流
        /// </summary>
        /// <param name="stream">消息的字节流</param>
        /// <param name="sign">消息签名 明文模式下可省略改参数</param>
        /// <exception cref="WeChatSignException"></exception>
        public WeChatMessageHandler SetStreamMessage(Stream stream, string sign = null)
        {
            string msg;

            if (_messageConfig.MessageEncryptModel == EncryptModel.Compatibility ||
                _messageConfig.MessageEncryptModel == EncryptModel.Encrypt)
            {
                if (string.IsNullOrEmpty(sign))
                    throw new WeChatSignException($"微信消息加密模式为加密 {nameof(sign)} 参数不能是Null");
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);
                XmlNode root = xmlDocument.FirstChild;
                Check.WeChatMessageSignCheck(
                    _messageConfig.EncodingAesKey,
                    root["Nonce"].InnerText,
                    root["TimeStamp"].InnerText,
                    root["Encrypt"].InnerText,
                    sign);
                msg = Check.WeChatMessageDecrypt(root["Encrypt"].InnerText, _messageConfig.EncodingAesKey,
                    _messageConfig.AppId);
            }
            else
            {
                byte[] buffer = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(buffer);
                msg = Encoding.UTF8.GetString(buffer);
            }

            SerializeMessage(msg);

            return this;
        }


        private void InitHandlerMap()
        {
            _messageHandler.Add(MessageType.Image, typeof(IImageMessageHandler));
            _messageHandler.Add(MessageType.Link, typeof(ILikeMessageHandler));
            _messageHandler.Add(MessageType.Location, typeof(ILocationMessageHandler));
            _messageHandler.Add(MessageType.Text, typeof(ITextMessageHandler));
            _messageHandler.Add(MessageType.Video, typeof(IVideoMessageHandler));
            _messageHandler.Add(MessageType.Voice, typeof(IVoiceMessageHandler));
            _messageHandler.Add(MessageType.MiniVideo, typeof(IMiniVideoMessageHandler));
            _messageHandler.Add(MessageType.EventClick, typeof(IEventClickMessageHandler));
            _messageHandler.Add(MessageType.EventLocation, typeof(IEventLocationMessageHandler));
            _messageHandler.Add(MessageType.EventMiniProgram, typeof(IEventMiniProgramMessageHandler));
            _messageHandler.Add(MessageType.EventPhoto, typeof(IEventPhotoMessageHandler));
            _messageHandler.Add(MessageType.EventPhotoAlbum, typeof(IEventPhotoAlbumMessageHandler));
            _messageHandler.Add(MessageType.EventPhotoWeChat, typeof(IEventPhotoWeChatMessageHandler));
            _messageHandler.Add(MessageType.EventScanCode, typeof(IEventScanCodeMessageHandler));
            _messageHandler.Add(MessageType.EventScanCoding, typeof(IEventScanCodingMessageHandler));
            _messageHandler.Add(MessageType.EventSendComplete, typeof(IEventSendCompleteMessageHandler));
            _messageHandler.Add(MessageType.EventSubscribe, typeof(IEventSubscribeMessageHandler));
            _messageHandler.Add(MessageType.EventUnsubscribe, typeof(IEventUnsubscribeMessageHandler));
            _messageHandler.Add(MessageType.EventScanSubscribe, typeof(IEventScanSubscribeMessageHandler));
            _messageHandler.Add(MessageType.EventScan, typeof(IEventScanMessageHandler));
            _messageHandler.Add(MessageType.EventUploadLocation, typeof(IEventUploadLocationMessageHandler));
        }

        private void InitMessageRequest()
        {
            _messageBody.Add(MessageType.EventClick, new ClickEventMessageRequest());
            _messageBody.Add(MessageType.EventLocation, new LocationEventMessageRequest());
            _messageBody.Add(MessageType.EventPhoto, new PhotoEventMessageRequest());
            _messageBody.Add(MessageType.EventPhotoAlbum, new PhotoEventMessageRequest());
            _messageBody.Add(MessageType.EventPhotoWeChat, new PhotoEventMessageRequest());
            _messageBody.Add(MessageType.EventView, new ViewEventMessageRequest());
            _messageBody.Add(MessageType.EventMiniProgram, new MiniProgramEventMessageRequest());
            _messageBody.Add(MessageType.EventScanCode, new ScanCodeEventMessageRequest());
            _messageBody.Add(MessageType.EventScanCoding, new ScanCodeEventMessageRequest());
            _messageBody.Add(MessageType.EventSubscribe, new SubscribeEventMessageRequest());
            _messageBody.Add(MessageType.EventUnsubscribe, new UnsubscribeEventMessageRequest());
            _messageBody.Add(MessageType.EventScanSubscribe, new ScanSubscribeEventMessageRequest());
            _messageBody.Add(MessageType.EventUploadLocation, new UploadLocationEventMessageRequest());
            _messageBody.Add(MessageType.EventSendComplete, new EventSendCompleteMessageRequest());
            _messageBody.Add(MessageType.EventScan, new ScanSubscribeEventMessageRequest());
            _messageBody.Add(MessageType.Image, new ImageMessageRequest());
            _messageBody.Add(MessageType.Link, new LinkMessageRequest());
            _messageBody.Add(MessageType.Location, new LocationMessageRequest());
            _messageBody.Add(MessageType.Text, new TextMessageRequest());
            _messageBody.Add(MessageType.Video, new VideoMessageRequest());
            _messageBody.Add(MessageType.Voice, new VoiceMessageRequest());
            _messageBody.Add(MessageType.MiniVideo, new MiniVideoMessageRequest());
            
        }

        /// <summary>
        ///  处理消息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="WeChatMessageDistinctException">消息重复异常</exception>
        /// <exception cref="WeChatSignException">消息签名错误异常</exception>
        /// <exception cref="WeChatException">参数异常</exception>
        public async Task<string> ExecutedAsync()
        {
            if (ValidateDistinctMessage()) throw new WeChatMessageDistinctException("出现重复消息");
            if (ValidateMessageSign()) throw new WeChatSignException("消息签名校验失败");

            if (_messageHandler.Any(x => x.Key == _currentMessageType) == false)
                throw new WeChatException("未找到可匹配的消息类型");
            var map = _messageHandler.First(x => x.Key == _currentMessageType);
            var handler = GetHandler(map.Value);

            if (handler == null)
                throw new WeChatException("未注册的消息处理器");

            var method = handler.GetType().GetMethod("Processed");

            if (method == null)
                throw new WeChatException("Processed 未找到");

            var invokeTask =
                method.Invoke(handler, new object[] {_messageBody[_currentMessageType]}) as Task<MessageResponse>;

            var responseTask = invokeTask;

            if (responseTask == null)
            {
                throw new WeChatException("返回值处理错误");
            }

            var result = await responseTask;
            if (result == null)
            {
                return "SUCCESS";
            }

            return FormatMessage(result);
        }


        private string FormatMessage(MessageResponse response)
        {
            var message = response.ToXml().Replace("\0", string.Empty);

            if (_messageConfig.MessageEncryptModel == EncryptModel.Encrypt)
            {
                var encryptResponse = new EncryptMessageResponse
                {
                    Nonce = Helper.GetNonceStr(),
                    TimeStamp = Helper.GetTimestamp().ToString(CultureInfo.InvariantCulture),
                    Value = MessageCryptography.AesEncrypt(message, _messageConfig.EncodingAesKey, _messageConfig.AppId)
                };
                string sign = Check.GetWeChatMessageSign(_messageConfig.EncodingAesKey, encryptResponse.Nonce,
                    encryptResponse.TimeStamp,
                    message);
                encryptResponse.Sign = sign;
                return XmlSerializeHelper.ObjectToXmlString(encryptResponse).Replace("\0", string.Empty);
            }

            return message;
        }

        private bool ValidateMessageSign()
        {
            if (_openSignValidate)
            {
                return WeChatSignHelper.CreateMessageSign(_nonce, _timestamp, _messageConfig.ValidateToken) ==
                       _signature;
            }

            return false;
        }

        /// <summary>
        /// 消息去重
        /// </summary>
        /// <returns></returns>
        private bool ValidateDistinctMessage()
        {
            if (_messageConfig.Distinct == false) return false;
            string key;
            if (_currentMessagePushType == MessagePushType.Event)
                key = _messageBody[_currentMessageType].FormUserName +
                      _messageBody[_currentMessageType].CreateTime;
            else
                key = _messageBody[_currentMessageType].MsgId.ToString();
            var flag = _cache.Get(key);
            if (flag == null)
            {
                _cache.Set(key, string.Empty, 10);
                return false;
            }

            return true;
        }
    }
}