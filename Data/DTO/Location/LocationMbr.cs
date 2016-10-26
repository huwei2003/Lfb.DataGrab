namespace Comm.Global.DTO.Location
{
    /// <summary>
    /// 最小位置矩形
    /// 注意使用Filed,非Property
    /// </summary>
    public class LocationMbr
    {
        /// <summary>
        /// 小纬度
        /// </summary>
        public double MinLat;

        /// <summary>
        /// 大纬度
        /// </summary>
        public double MaxLat;

        /// <summary>
        /// 小经度
        /// </summary>
        public double MinLon;

        /// <summary>
        /// 大经度
        /// </summary>
        public double MaxLon;
    }
}
