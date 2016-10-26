using Comm.Global.DTO.Interface;

namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// 聊天室基类
    /// 聊天室无群主概念
    /// </summary>
    public class RoomBase:IRoomId
    {
      
        /// <summary>
        /// 聊天室号
        /// 小写字母和数字，不超过16字
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 聊天室名称，不超过16字
        /// </summary>
        public string Name { get; set; }
    }
}
