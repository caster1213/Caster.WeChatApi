using System.ComponentModel;

namespace Caster.WeChat.MessageHandler
{
    public enum MessageType
    {
        [Description("text")]
        Text,
        [Description("image")]
        Image,
        [Description("voice")]
        Video,
        [Description("video")]
        Voice,
        [Description("shortvideo")]
        MiniVideo,
        [Description("SendLocation")]
        Location,
        [Description("link")]
        Link,
        [Description("CLICK")]
        EventClick,
        [Description("VIEW")]
        EventView,
        [Description("scancode_push")]
        EventScanCode,
        [Description("scancode_waitmsg")]
        EventScanCoding,
        [Description("pic_sysphoto")]
        EventPhoto,
        [Description("pic_photo_or_album")]
        EventPhotoAlbum,
        [Description("pic_weixin")]
        EventPhotoWeChat,
        [Description("location_select")]
        EventLocation,
        [Description("view_miniprogram")]
        EventMiniProgram,
        [Description("TEMPLATESENDJOBFINISH")]
        EventSendComplete,
        [Description("subscribe")]
        EventSubscribe,
        [Description("unsubscribe")]
        EventUnsubscribe,
        [Description("ScanSubscribe")]
        EventScanSubscribe,
        [Description("UploadLocation")]
        EventUploadLocation,
        [Description("Scan")]
        EventScan

    }

    public enum MessagePushType
    {
       Message,
       Event
    }
}