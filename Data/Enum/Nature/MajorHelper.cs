using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm.Global.Enum.Nature
{

    /// <summary>
    /// 枚举帮助类
    /// </summary>
    static public class MajorHelper
    {
        // bit15-bit11 学科,自行枚举, 5bit
        // bit10-bit6 专业类,学科下枚举
        // bit5-bit0 专业,学科专业类下枚举

        /// <summary>
        /// 学科屏蔽码
        /// </summary>
        private const int ScienceMask = 0xF800;

        /// <summary>
        /// 专业类屏蔽码
        /// </summary>
        private const int MajorcategoryMask = 0x07C0;

        /// <summary>
        /// 专业屏蔽码
        /// </summary>
        public const int MajorMask = 0x003F;

        /// <summary>
        /// 学科专业类屏蔽码
        /// </summary>
        private const int FullmajorcategoryMask = ScienceMask | MajorcategoryMask;

        /// <summary>
        /// 学科专业类专业屏蔽码
        /// </summary>
        private const int FullmajorMask = ScienceMask | MajorcategoryMask | MajorMask;

    
        /// <summary>
        /// 得到专业的所有名称对应的索引
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,int> GetMajorIndexDictionary()
        {
            var dict=new Dictionary<string, int>();
            foreach (var m in GetAllMajors())
            {
                dict.Add(m.ToString(),(int)m);
            }
            return dict;
        }

        /// <summary>
        /// 得到专业的三级列表名称
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string[]>> GetMajorDictionary(out Dictionary<string, int> keySort)
        {
            keySort = new Dictionary<string, int>();
            var nameDictionary = new Dictionary<string, Dictionary<string, string[]>>();
            var ps = GetAllSciences();
            int i = 0;
            foreach (var p in ps)
            {
                keySort.Add(p.ToString(), i++);
                var subDic = new Dictionary<string, string[]>();
                var cs = p.MajorCategories(); //citys
                int j = 0;
                foreach (var c in cs)
                {
                    keySort.Add(c.ToString(), j++);
                    var ds = c.Majors();
                    var dsNames = ds.Select(d => d.ToString()).ToArray();
                    subDic.Add(c.ToString(), dsNames);
                    for (int k = 0; k < dsNames.Length; k++)
                        keySort.Add(dsNames[k], k);
                }
                nameDictionary.Add(p.ToString(), subDic);
            }

            return nameDictionary;
        }



        /// <summary>
        /// 所有专业枚举，不包括0
        /// 混合包括了学科，专业类，专业
        /// </summary>
        public static IEnumerable<Major> GetAllMajors()
        {
            return ((Major[])System.Enum.GetValues(typeof(Major)))
                .Where(d => d != Major.无);
        }
        /// <summary>
        /// 重写静态方法ToString
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static string FullName(this Major major)
        {
            var sb = new StringBuilder();
            if (major.IsScience()) //直辖专业类,学科
                return major.ToString();
            if (major.IsMajorCategory())
                return string.Format("{0}-{1}", major.Science(), major);
            if (major.IsMajor())
                return string.Format("{0}-{1}-{2}", major.Science(), major.MajorCategory(), major);
            return sb.ToString();
        }

        /// <summary>
        /// 判断是否一个数据库区域字段属于一个条件区域
        /// 自身属于自身
        /// </summary>
        /// <param name="major">数据库区域字段</param>
        /// <param name="upMajor">条件上级区域</param>
        /// <returns></returns>
        public static bool BelongTo(this Major major, Major upMajor)
        {
            //未定义不包含任何其它地区
            if (upMajor == Major.无)
                return false;

            if (upMajor.IsMajorCategory()) //专业类
            {
                return (int)major >= (int)upMajor
                       && (int)major <= ((int)upMajor | MajorMask); //专业类内所有专业
            }
            if (upMajor.IsScience()) //学科
            {
                return (int)major >= (int)upMajor
                       && (int)major <= ((int)upMajor | MajorcategoryMask | MajorMask); //学科内所有专业类专业
            }
            return major == upMajor;
        }

      
        /// <summary>
        /// 判断是否是学科,包括虚拟的"直辖专业类"这个学科
        /// 不能用于数据库字段
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static bool IsScience(this Major major)
        {
            return ((int)major & ScienceMask) != 0 && ((int)major & ~ScienceMask) == 0;
        }

        /// <summary>
        /// 得到地区对应的学科,不判断是否合法
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static Major Science(this Major major)
        {
            return (Major)((int)major & ScienceMask);
        }
       

        /// <summary>
        /// 判断是否是专业类(包括同时为直辖专业类，或重要专业类)
        /// 不能用于数据库字段
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static bool IsMajorCategory(this Major major)
        {
            return ((int)major & ScienceMask) != 0 && ((int)major & MajorcategoryMask) != 0 && ((int)major & ~FullmajorcategoryMask) == 0;
        }

        /// <summary>
        /// 得到地区对应的专业类,不判断是否合法
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static Major MajorCategory(this Major major)
        {
            return (Major)((int)major & FullmajorcategoryMask);
        }
        

        /// <summary>
        /// 判断是否是区专业
        /// 不能用于数据库字段
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static bool IsMajor(this Major major)
        {
            return ((int)major & ScienceMask) != 0 && ((int)major & MajorcategoryMask) != 0
                   && ((int)major & MajorMask) != 0 && ((int)major & ~FullmajorMask) == 0;

        }
       

        /// <summary>
        /// 得到地区对应的专业,判断是否合法
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static Major TryMajor(this Major major)
        {
            if (!major.IsMajor())
                return Major.无;
            return (Major)((int)major & FullmajorMask);
        }

       
       

        /// <summary>
        /// 得到所有学科
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Major> GetAllSciences()
        {
            return GetAllMajors().Where(d => d.IsScience());

        }

        
        /// <summary>
        /// 得到学科下所有城专业类列表,列重要城专业类
        /// </summary>
        /// <param name="science"></param>
        /// <returns></returns>
        public static IEnumerable<Major> MajorCategories(this Major science)
        {
            return GetAllMajors().Where(r => r.IsMajorCategory() && r.BelongTo(science));
        }

        /// <summary>
        /// 得到专业类下所有区专业列表,列重要区专业
        /// </summary>
        /// <param name="majorCategory"></param>
        /// <returns></returns>
        public static IEnumerable<Major> Majors(this Major majorCategory)
        {
            return GetAllMajors().Where(r => r.IsMajor() && r.BelongTo(majorCategory));
        }

    }
}