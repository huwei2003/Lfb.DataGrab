namespace Comm.Global.DTO.Chat
{
    /// <summary>
    /// 普通群聊室
    /// </summary>
    public class PublicRoom : RoomBase
    {
        /// <summary>
        /// 群大小
        /// </summary>
        public int MaxCount { get; set; }

        /// <summary>
        /// 物理群号
        /// </summary>
        public string GroupId { get; set; }

       

    }
}
