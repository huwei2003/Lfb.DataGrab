using System.Collections.Generic;

namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// 私聊聊天室
    /// </summary>
    public class PrivateRoom : RoomBase
    {
        /// <summary>
        /// 主人
        /// </summary>
        public UserMan Host { get; set; }

        /// <summary>
        /// 客人
        /// </summary>
        public UserMan Guest { get; set; }

        /// <summary>
        /// 亲友0-4人左右
        /// </summary>
        public List<UserMan> Friends { get; set; }


       
    }
}
