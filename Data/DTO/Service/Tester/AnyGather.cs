using System;

namespace Comm.Global.DTO.Service.Tester
{
    /// <summary>
    /// 收集器泛型类，包装任何搜集的对象
    /// </summary>
    public class AnyGather<T> :AnyGatherCreate<T> where T : class
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
