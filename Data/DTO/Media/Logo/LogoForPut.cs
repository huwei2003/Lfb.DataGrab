namespace Comm.Global.DTO.Media.Logo
{
    /// <summary>
    /// logo可上传的
    /// </summary>
    public class LogoForPut:DtoLogo
    {
        /// <summary>
        /// 上传链接,带短期令牌
        /// </summary>
        public string UrlForPut { get; set; }

       
    }
}
