namespace Comm.Global.DTO.Media.Audio
{
    /// <summary>
    /// 音频可上传的
    /// </summary>
    public class AudioForPut : DtoAudio
    {
        /// <summary>
        /// 上传链接,带短期令牌
        /// </summary>
        public string UrlForPut { get; set; }

       
    }
}
