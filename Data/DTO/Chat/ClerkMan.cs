using Comm.Global.DTO.Interface;
using Comm.Global.Enum.Sys;

namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// 客服聊天人
    /// </summary>
    public class ClerkMan : IAccount
    {
        /// <summary>
        /// 聊天账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 客服编号
        /// </summary>
        public string ClerkId { get; set; }

        /// <summary>
        /// 客服自己的商家Id
        /// </summary>
        public int SellerId { get; set; }

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
