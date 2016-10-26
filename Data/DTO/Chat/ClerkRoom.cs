namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// 客服群聊聊天室
    /// </summary>
    public class ClerkRoom : PublicRoom
    {
       
        /// <summary>
        /// 客服
        /// </summary>
        public ClerkMan ClerkMan { get; set; }
    }
}
