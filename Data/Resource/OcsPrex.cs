namespace Comm.Global.Resource
{
    /// <summary>
    /// 服务命名
    /// 注意一旦确立，不能更存储对应值
    /// </summary>
    public static class OcsPrex
    {

        /// <summary>
        /// dto全局缓存,跨服务保持命名唯一
        /// 统一格式为 小写
        /// </summary>
        /// <param name="dtoName">服务</param>
        /// <returns></returns>
        public static string DtoNames(string dtoName)
        {
            return string.Format("{0}", dtoName.ToLower());
        }

    }
}
