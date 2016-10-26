using Comm.Global.Enum.Sys;

namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// 聊天人登录信息
    /// </summary>
    public class ChatManLoginInfo
    {
        /// <summary>
        /// 聊天平台
        /// </summary>
        public ChatPlatform ChatPlatform { get; set; }

        /// <summary>
        /// 物理账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 物理账号密码
        /// </summary>
        public string Password { get; set; }
    }
}