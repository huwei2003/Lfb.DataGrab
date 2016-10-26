namespace Comm.Global.DTO.Location
{
    public class LocationPoint
    {
        /// <summary>
        /// 纬度,必须小写,用于elasticsearch 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public double lat { get; set; }

        /// <summary>
        /// 经度,必须小写,用于elasticsearch 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public double lon { get; set; }
    }
}
