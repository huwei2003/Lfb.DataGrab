using Comm.Global.Enum.Nature;

namespace Comm.Global.DTO.Location
{
    /// <summary>
    /// 位置
    /// </summary>
    public class Location : LocationPoint
    {
        /// <summary>
        /// 位置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 位置所属地区，精确到区
        /// </summary>
        public Region District { get; set; }
    }
}
