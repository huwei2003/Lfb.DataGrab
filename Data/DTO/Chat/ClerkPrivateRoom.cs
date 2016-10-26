namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// 陪购私聊聊天室
    /// </summary>
    public class ClerkPrivateRoom : PrivateRoom
    {
       
        /// <summary>
        /// 客服业务员
        /// </summary>
        public ClerkMan Clerk { get; set; }
    }
}
