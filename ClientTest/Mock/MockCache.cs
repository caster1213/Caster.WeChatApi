using Caster.WeChat.Common;

namespace ClientTest.Mock
{
    public class MockCache:ICache
    {
        public object Get(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Set(string key, object value, int seconds)
        {
            throw new System.NotImplementedException();
        }
    }
}