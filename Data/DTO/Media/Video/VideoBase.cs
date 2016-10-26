namespace Comm.Global.DTO.Media.Video
{
    /// <summary>
    /// 视频基本类
    /// </summary>
    public class VideoBase:MediaBase
    {
        /// <summary>
        /// 快照
        /// </summary>
        public string Snap { get; set; }

        /// <summary>
        /// 时长秒数
        /// </summary>
        public int Seconds { get; set; }
    }
}
