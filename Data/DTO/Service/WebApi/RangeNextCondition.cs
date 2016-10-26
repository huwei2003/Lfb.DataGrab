// ReSharper disable InconsistentNaming

namespace Comm.Global.DTO.Service.WebApi
{
    /// <summary>
    /// 查询下一页使用的条件
    /// </summary>
    public class RangeNextCondition
    {
        /// <summary>
        /// 下一个结果的主键
        /// </summary>
        public string NextKey { get; set; }

        /// <summary>
        /// 查询截止的条件主键,不会到达
        /// </summary>
        public string EndKey { get; set; }
    }
}