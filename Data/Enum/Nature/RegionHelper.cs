using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm.Global.Enum.Nature
{
#pragma warning disable 1591

    /// <summary>
    /// 枚举帮助类
    /// </summary>
    static public class RegionHelper 
    {
        /// <summary>
        /// 行政区域的枚举
        /// bit31-bit24 //国家/地区
        /// bit23-bit19 //bit15-bit11 省,自行枚举, 5bit,1表示直辖市,31表示其它
        /// bit18-bit14 //bit10-bit6 市,省下枚举
        /// bit13-bit8 //bit5-bit0 区县,省市下枚举
        /// bit7-bit0 学校,市下统一枚举,上级可没有区县
        /// </summary>

        public const int Provincelmove = 19;// 省左移位数

        public const int Citylmove = 14;// 市左移位数

        public const int Districtlmove = 8;// 区县左移位数

        
        /// <summary>
        /// 省屏蔽码
        /// </summary>
        public const int Provincemask = 0xF80000;

        /// <summary>
        /// 市屏蔽码
        /// </summary>
        public const int Citymask = 0x07C000;

        /// <summary>
        /// 县屏蔽码
        /// </summary>
        public const int Districtmask = 0x003F00;

       
        /// <summary>
        /// 省市屏蔽码
        /// </summary>
        public const int Fullcitymask = Provincemask | Citymask;

        /// <summary>
        /// 省市县屏蔽码
        /// </summary>
        public const int Fulldistrictmask = Provincemask | Citymask | Districtmask;

         
        #region 字典
        
        /// <summary>
        /// 所有地区数据库
        /// </summary>
        public static Dictionary<string, Dictionary<string, string[]>> GetRegionDictionary()
        {
            var regionDictionary = new Dictionary<string, Dictionary<string, string[]>>();
            var ps = GetAllProvinces(); //省
            int i = 0;
            foreach (var p in ps)
            {
                var subDic = new Dictionary<string, string[]>();
                var cs = p.Cities(); //省下市
                int j = 0;
                foreach (var c in cs)
                {
                    var ds = c.Districts(); //市下区县
                    var dsNames = ds.Select(d => d.ToString()).ToArray();
                    subDic.Add(c.ToString(), dsNames);
                }
                regionDictionary.Add(p.ToString(), subDic);
            }

            return regionDictionary;

        }

        #endregion

        /// <summary>
        /// 所有地区学校枚举，不包括0
        /// 混合包括了省市县
        /// </summary>
        public static IEnumerable<Region> GetAllRegions ()
        {
            return ((Region[])System.Enum.GetValues(typeof(Region)))
                .Where(d => d != Region.无);
        }
        /// <summary>
        /// 重写静态方法ToString
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string FullName(this Region region)
        {
            var sb = new StringBuilder();
            if (region.IsCCity() ) //直辖市
                return region.ToString();
            if (region.IsCity()) //普通市
                return string.Format("{0}-{1}", region.Province(), region);
            if (region.IsDistrict())//区县
                return string.Format("{0}-{1}-{2}", region.Province(), region.City(), region);
            return sb.ToString();
        }

        #region 简单判断

        /// <summary>
        /// 判断是否是省,包括虚拟的"直辖市"这个省和大陆外,基于region是合法值
        /// 不能用于数据库字段
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsProvince(this Region region)
        {
            return ((int)region & Provincemask) != 0 && ((int)region & ~Provincemask) == 0;
        }

        /// <summary>
        /// 得到地区对应的省,不判断是否合法
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static Region Province(this Region region)
        {
            return (Region)((int)region & Provincemask);
        }

       

        /// <summary>
        /// 判断是否是市(包括同时为直辖市，或重要市),只基于Region属于合法值
        /// 不能用于数据库字段
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsCity(this Region region)
        {
            return ((int)region & Citymask) != 0 && ((int)region & ~Fullcitymask) == 0;
        }

        /// <summary>
        /// 判断是否是直辖市
        /// 不能用于数据库字段
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsCCity(this Region region)
        {
            return region.IsCity() && region.Province() == Region.直辖市;
        }

        /// <summary>
        /// 得到地区对应的市,不判断是否合法
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static Region City(this Region region)
        {
            return (Region)((int)region & Fullcitymask);
        }

       

        /// <summary>
        /// 判断是否是区县,只基于Region属于合法值
        /// 不能用于数据库字段
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsDistrict(this Region region)
        {
            return ((int)region & Districtmask) != 0 && ((int)region & ~Fulldistrictmask) == 0;
          
        }
        /// <summary>
        /// 得到地区对应的县,不判断是否合法
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static Region District(this Region region)
        {
            return (Region)((int)region & Fulldistrictmask);
        }
        
        /// <summary>
        /// 得到所有省
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Region> GetAllProvinces()
        {
            //重新排序
            return new[]
            {
                Region.直辖市, 
                Region.广东,
                Region.浙江,
                Region.江苏,
                Region.福建,
                Region.山东,
                Region.四川,
                Region.湖南,
                Region.湖北,
                Region.江西,
                Region.安徽,
                Region.广西,
                Region.海南,
                Region.河南,
                Region.河北,
                Region.陕西,
                Region.山西,
                Region.云南,
                Region.贵州,
                Region.辽宁,
                Region.黑龙江,
                Region.吉林,
                Region.内蒙古,
                Region.甘肃,
                Region.新疆,
                Region.宁夏,
                Region.青海,
                Region.西藏,
                Region.大陆外
            };
            //return GetAllRegions().Where(d => d.IsProvince());
        
        }

       
        

        /// <summary>
        /// 得到所有直辖市,包括重要市
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Region> GetAllCCities()
        {
            return GetAllRegions().Where(d => d.IsCCity());                
        }

        

        /// <summary>
        /// 得到所有城市列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Region> GetAllCities()
        {
            return GetAllRegions().Where(r => r.IsCity());
        }

        public static IEnumerable<Region> GetAllDistricts()
        {
            return GetAllRegions().Where(r => r.IsDistrict());
        }

       

        /// <summary>
        /// 得到省下所有城市列表,列重要城市
        /// </summary>
        /// <param name="province"></param>
        /// <returns></returns>
        public static IEnumerable<Region> Cities(this Region province)
        {
            return GetAllRegions().Where(r => r.IsCity() && r.BelongTo(province));
        }

        /// <summary>
        /// 得到地区下所有区县列表
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IEnumerable<Region> Districts(this Region region)
        {
            return GetAllRegions().Where(r => r.IsDistrict() && r.BelongTo(region));
        }

      

        #endregion

        #region 数据库快速匹配

        /// <summary>
        /// 判断是否一个数据库区域字段属于一个条件区域
        /// 自身属于自身
        /// </summary>
        /// <param name="region">数据库区域字段</param>
        /// <param name="upRegion">条件上级区域</param>
        /// <returns></returns>
        public static bool BelongTo(this Region region, Region upRegion)
        {
            //未定义不包含任何其它地区
            if (upRegion == Region.无)
                return false;
            //总是属于全国
            //if (upRegion == Region.全国)
            //    return region != Region.无;

            if (upRegion.IsProvince()) //省
            {
                return (int)region >= (int)upRegion
                       && (int)region <= ((int)upRegion | Citymask | Districtmask ); //省内所有市县
            }
            if (upRegion.IsCity()) //市
            {
                return (int)region >= (int)upRegion
                       && (int)region <= ((int)upRegion | Districtmask ); //市内所有县
            }
            return region == upRegion;
        }

      

        /// <summary>
        /// 判断是否一个数据库区域字段和另外一个条件区域有交集
        /// 相等或上下级
        /// </summary>
        /// <param name="region"></param>
        /// <param name="interRegion"></param>
        /// <returns></returns>
        public static bool Intersect(this Region region, Region interRegion)
        {
            if (interRegion == Region.无)
                return false;

            var district = interRegion.District();
            var city = interRegion.City();
            var procince = interRegion.Province();

            if (interRegion.IsProvince()) //省
            {
                return (int)region >= (int)interRegion
                       && (int)region <= ((int)interRegion | Citymask | Districtmask ); //省内所有市县
            }
            if (interRegion.IsCity()) //市
            {
                return region == procince
                       || (int)region >= (int)interRegion
                       && (int)region <= ((int)interRegion | Districtmask ); //市内所有县
            }
            return region == interRegion
                   || region == district
                   || region == city
                   || region == procince;
        }
        #endregion 

       
    }
}