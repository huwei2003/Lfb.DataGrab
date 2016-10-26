namespace Comm.Global.DTO.Media.Photo
{
    /// <summary>
    /// 相片可上传的
    /// </summary>
    public class PhotoForPut:DtoPhoto
    {
        /// <summary>
        /// 上传链接,带短期令牌
        /// </summary>
        public string UrlForPut { get; set; }

       
    }
}
