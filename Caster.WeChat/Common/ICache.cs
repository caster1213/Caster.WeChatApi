namespace Caster.WeChat.Common
{
    public interface ICache
    {
        object Get(string key);

        void Set(string key, object value, int seconds);
    }
}