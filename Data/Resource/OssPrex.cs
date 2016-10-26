namespace Comm.Global.Resource
{
    /// <summary>
    /// oss对象存储,按服务独立
    /// </summary>
    public static class OssPrex
    {


        public static string KnowledgeNames(int serviceName)
        {
            return string.Format("download/{0}", serviceName);
      
        }


        /// <summary>
        /// Logo库对应的存储路径
        /// 统一格式为小写
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <param name="segName">段名</param>
        /// <returns></returns>
        public static string LogoGroupNames(int serviceName, string segName)
        {
            return string.Format("logo/{0}/{1}", serviceName, segName.ToLower());
        }

        /// <summary>
        /// 图片库对应的存储路径
        /// 统一格式为小写
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <param name="segName">段名</param>
        /// <returns></returns>
        public static string PhotoGroupNames(int serviceName,string segName)
        {
            return string.Format("photo/{0}/{1}", serviceName,segName.ToLower());
        }

        /// <summary>
        /// 视频库对应的存储路径
        /// 统一格式为小写
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <param name="segName">段名</param>
        /// <returns></returns>
        public static string VideoGroupNames(int serviceName, string segName)
        {
            return string.Format("video/{0}/{1}", serviceName, segName.ToLower());
        }

        /// <summary>
        /// 音频库对应的存储路径
        /// 统一格式为小写
        /// </summary>
        /// <param name="serviceName">服务</param>
        /// <param name="segName">段名</param>
        /// <returns></returns>
        public static string AudioGroupNames(int serviceName, string segName)
        {
            return string.Format("audio/{0}/{1}", serviceName, segName.ToLower());
        }
    }
}
