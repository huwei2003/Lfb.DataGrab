namespace Comm.Global.DTO.Service.Tester
{
    /// <summary>
    /// 收集器泛型类，包装任何搜集的对象,创建时使用
    /// </summary>
    public class AnyGatherCreate<T> where T : class
    {
        /// <summary>
        /// 内容类型，例如'短信'
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 内容索引，用于搜索内容，例如'13900000000'
        /// </summary>
        public string Index { get; set; }
        /// <summary>
        /// 内容本身,例如{Id:1,Mobile:"13900000000",sms:"123456"}
        /// </summary>
        public T Content { get; set; }
    }
}
