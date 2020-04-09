using Newtonsoft.Json;

namespace Caster.WeChat.Parameter
{
    public class UpdateArticleParameter
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("thumb_media_id")]
        public string FileId { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("digest")]
        public string Digest { get; set; }
        [JsonProperty("show_cover_pic")]
        public int ShowCoverPic { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("content_source_url")]
        public string ContentSourceUrl { get; set; }
        [JsonProperty("need_open_comment")]
        public int NeedOpenComment { get; set; }
        [JsonProperty("only_fans_can_comment")]
        public int OnlyFansOpenComment { get; set; }
    }
}