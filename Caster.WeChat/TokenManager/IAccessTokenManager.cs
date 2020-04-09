using System.Threading.Tasks;

namespace Caster.WeChat.TokenManager
{
    /// <summary>
    /// Token存储
    /// </summary>
    public interface IAccessTokenManager
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        Task<string> Get();
        /// <summary>
        /// 保存Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task Save(string token);
    }
}