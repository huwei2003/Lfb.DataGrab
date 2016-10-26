using Comm.Global.Enum.Sys;

namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// 从服务端推送到手机端的消息
    /// 用于承载提醒业务，和聊天无关 
    /// </summary>
    public class PushData
    {
        /*/// <summary>
        /// 接收者业务
        /// </summary>
        public ChatRole ChatRole { get; set; }*/

        /// <summary>
        /// 消息类型 
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// 标题 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 关联的实体Id,用于点击时跳转 
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// 消息显示的头像 
        /// </summary>
        public string LogoUrl { get; set; }
    }
}
