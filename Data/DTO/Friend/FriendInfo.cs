using System;
using Comm.Global.Enum.Business;

namespace Comm.Global.DTO.Friend
{
    /// <summary>
    /// 双方好友关系
    /// </summary>
    public class FriendInfo
    {
        /// <summary>
        /// 对方用户
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 关系
        /// </summary>
        public FriendStage FriendStage { get; set; }

        /// <summary>
        /// 朋友备注
        /// </summary>
        public string Commit { get; set; }
    }
}