using System.Collections.Generic;
using System.Linq;

namespace Comm.Global.Enum.Nature
{
#pragma warning disable 1591

    /// <summary>
    /// 行业枚举帮助类
    /// </summary>
    public static class IndustryHelper
    {
        /// bit11-bit6 行业分类,自行枚举
        /// bit5-bit0  具体行业,分类下枚举


        public const int IndustryCategoryMove = 6; // 行业分类左移位数

       

        /// <summary>
        /// 行业屏蔽码
        /// </summary>
        private const int IndustryCategoryMask = 0x00000FC0;

        /// <summary>
        /// 职位屏蔽码
        /// </summary>
        private const int IndustryMask = 0x0000003F;

       

        /// <summary>
        /// 全行业屏蔽码
        /// </summary>
        private const int FullIndustryMask = IndustryCategoryMask | IndustryMask;

       
        public static Dictionary<string, string[]> GetIndustryDictionary(
         out Dictionary<string, int> keySort)
        {
            keySort = new Dictionary<string, int>();

            var subDic = new Dictionary<string, string[]>();
            var Is = GetAllIndustryCategories(); //行业类

            int j = 0;
            foreach (var i in Is) //枚举第二级
            {
                keySort.Add(i.ToString(), j++);
                var ps = i.Industries();

                var dsNames = ps;//.Select(d => d.ToString()).ToArray();
                subDic.Add(i.ToString(), dsNames);
                var ss = ps;//.Select(d => d.ToString()).ToArray();
                for (int k = 0; k < ss.Length; k++) //枚举第三级
                    keySort.Add(ss[k], k);
            }
            return subDic;
        }

        /// <summary>
        /// js专用的没有下划线分隔的字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string[]> GetIndustryDictionaryJs()
        {
           
            var subDic = new Dictionary<string, string[]>();
            var Is = GetAllIndustryCategories(); //行业类

            int j = 0;
            foreach (var i in Is) //枚举第1级
            {
                var ps = i.Industrys();

                var dsNames = ps.Select(d => d.ShortName()).ToArray();
                subDic.Add(i.ToString(), dsNames);
            }
            return subDic;
        }

        /// <summary>
        /// 所有行业枚举，不包括0
        /// 混合包括了行业分类，行业
        /// </summary>
        private static IEnumerable<Industry> GetAllBrandIndustrys()
        {
            return
                ((Industry[]) System.Enum.GetValues(typeof (Industry)))
                    .Where(d => d != Industry.无);
        }

        /// <summary>
        /// 职位的短名称
        /// 互联网_开发==》开发
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public static string ShortName(this Industry industry)
        {
            // StringBuilder sb = new StringBuilder();
            var ss = industry.ToString().Split('_');
            return ss[ss.Length - 1];//返回最后一段
        }
  
      

        /// <summary>
        /// 判断是否是具体行业类别
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public static bool IsIndustryCategory(this Industry industry)
        {
            return ((int)industry & IndustryCategoryMask) != 0 && ((int)industry & ~IndustryCategoryMask) == 0;
      
       }

        /// <summary>
        /// 判断是否是具体行业
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public static bool IsIndustry(this Industry industry)
        {
            return ((int)industry & IndustryMask) != 0 && ((int)industry & ~FullIndustryMask) == 0;
        }

       

        /// <summary>
        /// 得到所有行业分类
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Industry> GetAllIndustryCategories()
        {
            return GetAllBrandIndustrys().Where(d => d.IsIndustryCategory());

        }
        public static bool BelongTo(this Industry industry, Industry upIndustry)
        {
            //未定义不包含任何其它地区
            if (upIndustry == Industry.无)
                return false;

            if (upIndustry.IsIndustryCategory()) //行业分类
            {
                return (int)industry >= (int)upIndustry
                       && (int)industry <= ((int)upIndustry | IndustryMask);
            }
            return industry == upIndustry;
        }


        public static IEnumerable<Industry> Industrys(this Industry industryCategory)
        {
            return GetAllBrandIndustrys().Where(r => r.IsIndustry() && r.BelongTo(industryCategory));
        }
       
        /// <summary>
        /// 得到分类下所有行业列表
        /// </summary>
        /// <param name="industryCategory"></param>
        /// <returns></returns>
        public static string[] Industries(this Industry industryCategory)
        {
            var categoryName = industryCategory.ToString();
            var names = System.Enum.GetNames(typeof(Industry));
            return names.Where(industry =>
            {
                var ss = industry.Split('_');
                return (ss.Length == 2 && ss[0] == categoryName);
            }).ToArray();
        }
      }
}