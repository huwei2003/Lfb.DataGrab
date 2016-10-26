using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Csharp.Tools.Dto;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// 地理位置帮助类
    /// </summary>
    public static class LocationPointHelper
    {
        /// <summary>
        /// 计算两点间距离，单位公里
        /// </summary>
        /// <param name="from">Point in long/lat decimal degrees</param>
        /// <param name="to">Point in long/lat decimal degrees</param>
        /// <returns></returns>
        public static double CalcKm(this LocationPoint from, LocationPoint to)
        {

            const double rad = 6371; //Earth radius in Km

            //Convert to radians

            var p1X = from.lon / 180 * Math.PI;

            var p1Y = from.lat / 180 * Math.PI;

            var p2X = to.lon / 180 * Math.PI;

            var p2Y = to.lat / 180 * Math.PI;

            var km = Math.Acos(Math.Sin(p1Y) * Math.Sin(p2Y) +
                               Math.Cos(p1Y) * Math.Cos(p2Y) * Math.Cos(p2X - p1X)) * rad;
            return km;

        }

        /// <summary>
        /// 计算两点间距离，单位米
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static int CalcDistance(this LocationPoint from, LocationPoint to)
        {
            return (int)(CalcKm(from, to) * 1000); //km->m
        }

        /// <summary>
        /// 从一个点移动若干距离得到一个新点
        /// </summary>
        /// <param name="from"></param>
        /// <param name="lonOffset">东+西-移动，单位度</param>
        /// <param name="latOffset">北+南-移动，单位度</param>
        /// <returns></returns>
        public static LocationPoint Move(this LocationPoint from, double latOffset, double lonOffset)
        {
            return new LocationPoint { lat = from.lat + latOffset, lon = from.lon + lonOffset };
        }




        #region 坐标变换

        /** 
         * 各地图API坐标系统比较与转换; 
         * WGS84坐标系：即地球坐标系，国际上通用的坐标系。设备一般包含GPS芯片或者北斗芯片获取的经纬度为WGS84地理坐标系, 
         * 谷歌地图采用的是WGS84地理坐标系（中国范围除外）; 
         * GCJ02坐标系：即火星坐标系，是由中国国家测绘局制订的地理信息系统的坐标系统。由WGS84坐标系经加密后的坐标系。 
         * 谷歌中国地图和搜搜中国地图采用的是GCJ02地理坐标系; BD09坐标系：即百度坐标系，GCJ02坐标系经加密后的坐标系; 
         * 搜狗坐标系、图吧坐标系等，估计也是在GCJ02基础上加密而成的。 chenhua 
         */

        private const double Pi = 3.1415926535897932384626;
        private const double A = 6378245.0;
        private const double Ee = 0.00669342162296594323;


        /// <summary>
        /// 84 to 火星坐标系 (GCJ-02) World Geodetic System ==> Mars Geodetic System
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
 
        public static LocationPoint Gps84_To_Gcj02(double lat, double lon)
        {
            if (OutOfChina(lat, lon))
            {
                return null;
            }
            var dLat = TransformLat(lon - 105.0, lat - 35.0);
            var dLon = TransformLon(lon - 105.0, lat - 35.0);
            var radLat = lat / 180.0 * Pi;
            var magic = Math.Sin(radLat);
            magic = 1 - Ee * magic * magic;
            var sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((A * (1 - Ee)) / (magic * sqrtMagic) * Pi);
            dLon = (dLon * 180.0) / (A / sqrtMagic * Math.Cos(radLat) * Pi);
            var mgLat = lat + dLat;
            var mgLon = lon + dLon;
            return new LocationPoint { lat = mgLat, lon = mgLon };
        }



        /// <summary>
        /// 火星坐标系 (GCJ-02) to 84 * * @param lon * @param lat * @return
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
  
        public static LocationPoint Gcj02_To_Gps84(double lat, double lon)
        {
            var gps = Transform(lat, lon);
            var lontitude = lon * 2 - gps.lon;
            var latitude = lat * 2 - gps.lat;
            return new LocationPoint { lat = latitude, lon = lontitude };
        }


        /// <summary>
        /// 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换算法 将 GCJ-02 坐标转换成 BD-09 坐标
        /// </summary>
        /// <param name="ggLat"></param>
        /// <param name="ggLon"></param>
        /// <returns></returns>
  
        public static LocationPoint Gcj02_To_Bd09(double ggLat, double ggLon)
        {
            double x = ggLon, y = ggLat;
            var z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * Pi);
            var theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * Pi);
            var bdLon = z * Math.Cos(theta) + 0.0065;
            var bdLat = z * Math.Sin(theta) + 0.006;
            return new LocationPoint { lat = bdLat, lon = bdLon };
        }


        /// <summary>
        /// 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换算法 * * 将 BD-09 坐标转换成GCJ-02 坐标 * * @param 
        ///  bd_lat * @param bd_lon * @return
        /// </summary>
        /// <param name="bdLat"></param>
        /// <param name="bdLon"></param>
        /// <returns></returns>
  
        public static LocationPoint Bd09_To_Gcj02(double bdLat, double bdLon)
        {
            double x = bdLon - 0.0065, y = bdLat - 0.006;
            var z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * Pi);
            var theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * Pi);
            var ggLon = z * Math.Cos(theta);
            var ggLat = z * Math.Sin(theta);
            return new LocationPoint { lat = ggLat, lon = ggLon };
        }


        /// <summary>
        /// (BD-09)-->84
        /// </summary>
        /// <param name="bdLat"></param>
        /// <param name="bdLon"></param>
        /// <returns></returns>
  
        public static LocationPoint Bd09_To_Gps84(double bdLat, double bdLon)
        {
            var gcj02 = Bd09_To_Gcj02(bdLat, bdLon);
            var map84 = Gcj02_To_Gps84(gcj02.lat, gcj02.lon);
            return map84;
        }

        /// <summary>
        /// 84->(BD-09)
        /// </summary>
        /// <param name="bdLat"></param>
        /// <param name="bdLon"></param>
        /// <returns></returns>
 
        public static LocationPoint Gps84_To_Bd09(double bdLat, double bdLon)
        {
            var gcj02 = Gps84_To_Gcj02(bdLat, bdLon);
            var bd09 = Gcj02_To_Bd09(gcj02.lat, gcj02.lon);
            return bd09;
        }



        private static bool OutOfChina(double lat, double lon)
        {
            return false; //不判断出国，提高速度
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        private static LocationPoint Transform(double lat, double lon)
        {
            if (OutOfChina(lat, lon))
            {
                return new LocationPoint { lat = lat, lon = lon };
            }
            var dLat = TransformLat(lon - 105.0, lat - 35.0);
            var dLon = TransformLon(lon - 105.0, lat - 35.0);
            var radLat = lat / 180.0 * Pi;
            var magic = Math.Sin(radLat);
            magic = 1 - Ee * magic * magic;
            var sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((A * (1 - Ee)) / (magic * sqrtMagic) * Pi);
            dLon = (dLon * 180.0) / (A / sqrtMagic * Math.Cos(radLat) * Pi);
            var mgLat = lat + dLat;
            var mgLon = lon + dLon;
            return new LocationPoint { lat = mgLat, lon = mgLon };
        }

        private static double TransformLat(double x, double y)
        {
            var ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y
                         + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * Pi) + 20.0 * Math.Sin(2.0 * x * Pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * Pi) + 40.0 * Math.Sin(y / 3.0 * Pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * Pi) + 320 * Math.Sin(y * Pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private static double TransformLon(double x, double y)
        {
            var ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1
                         * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * Pi) + 20.0 * Math.Sin(2.0 * x * Pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * Pi) + 40.0 * Math.Sin(x / 3.0 * Pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * Pi) + 300.0 * Math.Sin(x / 30.0 * Pi)) * 2.0 / 3.0;
            return ret;
        }
        #endregion
    }
}
