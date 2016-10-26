namespace Comm.Global.DTO.Media.Video
{
    /// <summary>
    /// 视频可上传的
    /// </summary>
    public class VideoForPut:DtoVideo
    {
        /// <summary>
        /// 上传链接,带短期令牌
        /// </summary>
        public string UrlForPut { get; set; }

       
    }
}
