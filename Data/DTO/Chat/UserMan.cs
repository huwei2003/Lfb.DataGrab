using Comm.Global.DTO.Interface;
using Comm.Global.Enum.Sys;

namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// User聊天人
    /// </summary>
    public class UserMan : IAccount
    {
        /// <summary>
        /// 聊天账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public ChatRole ChatRole { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色头像
        /// </summary>
        public string LogoUrl { get; set; }
     }
}
