namespace Comm.Global.DTO.Service.Gateway
{
    /// <summary>
    /// 知识库
    /// </summary>
    public class KnowledgeInfo
    {
        /// <summary>
        /// 当前版本号，登录时告知
        /// </summary>
        public string KnowledgeVersion;

        /// <summary>
        /// 当前知识库name
        /// </summary>
        public string KnowledgeName;
        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl;
    }
}