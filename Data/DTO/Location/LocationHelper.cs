using System;

namespace Comm.Global.DTO.Location
{
    public static class LocationHelper
    {
       
        /// <summary>
        /// 根据指定坐标获取一个最小外包矩形
        /// (Minimum bounding rectangle，简称MBR)
        /// 注意在经纬度边界处会出现大误差
        /// </summary>
        /// <param name="lat">纬度 (-90,90]</param>
        /// <param name="lon">经度 [-180,180)</param>
        /// <param name="distance">距离(米)</param>
        /// <returns></returns>
        public static LocationMbr GetMbr(double lat, double lon, int distance)
        {
            var mbr= new LocationMbr();
            GetRectRange(lat, lon, distance, out mbr.MaxLat, out mbr.MinLat, out mbr.MaxLon, out mbr.MinLon);
            return mbr;
        }

        public const double Ea = 6378137;     //   赤道半径  

        public const double Eb = 6356725;     //   极半径 



        private static void GetlatLon(double lat, double lon, double distance, double angle, out double newLon, out double newLat)
        {

            var dx = distance * Math.Sin(angle * Math.PI / 180.0);

            var dy = distance * Math.Cos(angle * Math.PI / 180.0);

            var ec = Eb + (Ea - Eb) * (90.0 - lat) / 90.0;

            var ed = ec * Math.Cos(lat * Math.PI / 180);

            newLon = (dx / ed + lon * Math.PI / 180.0) * 180.0 / Math.PI;

            newLat = (dy / ec + lat * Math.PI / 180.0) * 180.0 / Math.PI;
        }



        private static void GetRectRange(double lat, double lon, double distance, 
            out double maxLat, out double minLat, out double maxLon, out double minLon)
        {

            var temp = 0.0;

            GetlatLon(lat, lon, distance, 0, out temp, out maxLat);

            GetlatLon(lat, lon, distance, 180, out temp, out minLat);

            GetlatLon(lat, lon, distance, 90, out maxLon, out temp);

            GetlatLon(lat, lon, distance, 270, out minLon, out temp);

        }
    }
}
