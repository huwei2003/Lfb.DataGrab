using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm.Global.Enum.Nature
{
#pragma warning disable 1591

    /// <summary>
    /// 招聘职位枚举帮助类
    /// </summary>
    public static class IndustryPostHelper
    {
        /// bit16-bit11 行业分类,自行枚举
        /// bit10-bit6 具体行业,分类下枚举
        /// bit5-bit0  职位

        public const int INDUSTRYCATEGORYLMOVE = 11; // 行业分类左移位数

        public const int INDUSTRYLMOVE = 6; // 行业左移位数

        /// <summary>
        /// 行业分类屏蔽码
        /// </summary>
        public const int INDUSTRYCATEGORYMASK = 0x0001F800;

        /// <summary>
        /// 行业屏蔽码
        /// </summary>
        public const int INDUSTRYMASK = 0x000007C0;

        /// <summary>
        /// 职位屏蔽码
        /// </summary>
        public const int POSTMASK = 0x0000003F;

        /// <summary>
        /// 全行业屏蔽码
        /// </summary>
        public const int FULLINDUSTRYMASK = INDUSTRYCATEGORYMASK | INDUSTRYMASK;

        /// <summary>
        /// 全行业屏蔽码
        /// </summary>
        public const int FULLPOSTMASK = INDUSTRYCATEGORYMASK | INDUSTRYMASK | POSTMASK;

        /// <summary>
        /// 得到大学的所有名称对应的索引
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> GetIndustryPostIndexDictionary()
        {
            var dict = new Dictionary<string, int>();
            foreach (var m in GetAllIndustryPosts())
            {
                dict.Add(m.ToString(), (int) m);
            }
            return dict;
        }

        public static Dictionary<string, Dictionary<string, string[]>> GetIndustryPostDictionary(
            out Dictionary<string, int> keySort)
        {
            keySort = new Dictionary<string, int>();
            var nameDictionary = new Dictionary<string, Dictionary<string, string[]>>();
            var ctgs = GetAllIndustryCategories(); //行业类
            int ii = 0;
            foreach (var c in ctgs) //枚举第一级
            {
                keySort.Add(c.ToString(), ii++);
                var subDic = new Dictionary<string, string[]>();
                var Is = c.Industries().ToList(); //行业类下行业
               
                int j = 0;
                foreach (var i in Is)//枚举第二级
                {
                    keySort.Add(i.ToString(), j++);
                    var ps = i.Posts();

                    var dsNames = ps.Select(d => d.PostName()).ToArray();
                    subDic.Add(i.ToString(), dsNames);
                    var ss = ps.Select(d => d.ToString()).ToArray();
                    for (int k = 0; k < ss.Length; k++)//枚举第三级
                        keySort.Add(ss[k], k);
                }

                /* if (c != IndustryPost.互联网软件)//添加3个职位
                {
                    Is.Clear();
                    Is.Add(IndustryPost.互联网软件_移动开发);
                    Is.Add(IndustryPost.互联网软件_前端开发);
                    Is.Add(IndustryPost.互联网软件_后端开发);

                }*/
                nameDictionary.Add(c.ToString(), subDic);
            }
            return nameDictionary;
        }

        /// <summary>
        /// js专用的没有下划线分隔的字典
        /// </summary>
        /// <param name="keySort"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string[]>> GetIndustryPostDictionaryJs()
        {
             var nameDictionary = new Dictionary<string, Dictionary<string, string[]>>();
            var ctgs = GetAllIndustryCategories(); //行业类
            int ii = 0;
            foreach (var c in ctgs) //枚举第一级
            {
               var subDic = new Dictionary<string, string[]>();
                var Is = c.Industries().ToList(); //行业类下行业

                int j = 0;
                foreach (var i in Is)//枚举第二级
                {
                    var ps = i.Posts();

                    var dsNames = ps.Select(d => d.ShortName()).ToArray();
                    subDic.Add(i.ShortName(), dsNames);
                }
               
                nameDictionary.Add(c.ToString(), subDic);
            }
            return nameDictionary;
        }

        /// <summary>
        /// 所有行业枚举，不包括0
        /// 混合包括了行业分类，行业
        /// </summary>
        private static IEnumerable<IndustryPost> GetAllIndustryPosts()
        {
            return
                ((IndustryPost[]) System.Enum.GetValues(typeof (IndustryPost)))
                    .Where(d => d != IndustryPost.无);
        }

        /// <summary>
        /// 职位的短名称
        /// 互联网_开发==》开发
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static string ShortName(this IndustryPost industryPost)
        {
            // StringBuilder sb = new StringBuilder();
            var ss = industryPost.ToString().Split('_');
            return ss[ss.Length - 1];//返回最后一段
        }

        /// <summary>
        /// 职位的'干净'名称
        /// 互联网软件_市场营销_营销总监->营销总监
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static string PostName(this IndustryPost industryPost)
        {
            var name = industryPost.ToString();
            int index = name.LastIndexOf('_');
            if (index < 0)
                return name;
            return name.Substring(index + 1);
        }

  

   

        /// <summary>
        /// 判断是否一个数据库行业属于另外一个行业
        /// 自身属于自身
        /// </summary>
        /// <param name="industryPost">数据库行业字段</param>
        /// <param name="upIndustryPost">条件上级行业</param>
        /// <returns></returns>
        public static bool BelongTo(this IndustryPost industryPost, IndustryPost upIndustryPost)
        {
            //未定义不包含任何其它地区
            if (upIndustryPost == IndustryPost.无)
                return false;

            if (upIndustryPost.IsIndustryCategory()) //行业分类
            {
                return (int) industryPost >= (int) upIndustryPost
                       && (int) industryPost <= ((int) upIndustryPost | INDUSTRYMASK);
            }
            if (upIndustryPost.IsIndustry()) //行业
            {
                return (int) industryPost >= (int) upIndustryPost
                       && (int) industryPost <= ((int) upIndustryPost | POSTMASK);
            }
            return industryPost == upIndustryPost;
        }

    
     
        /// <summary>
        /// 判断是否是分类
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static bool IsIndustryCategory(this IndustryPost industryPost)
        {
            return ((int) industryPost & INDUSTRYCATEGORYMASK) != 0 && ((int) industryPost & ~INDUSTRYCATEGORYMASK) == 0;
        }

        /// <summary>
        /// 得到行业对应的类别,不判断是否合法
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static IndustryPost IndustryCategory(this IndustryPost industryPost)
        {
            return (IndustryPost) ((int) industryPost & INDUSTRYCATEGORYMASK);
        }


        /// <summary>
        /// 判断是否是具体行业
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsIndustry(this IndustryPost industryPost)
        {
            return ((int) industryPost & INDUSTRYMASK) != 0 && ((int) industryPost & ~FULLINDUSTRYMASK) == 0;
        }

        /// <summary>
        /// 得到具体行业,不判断是否合法
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static IndustryPost Industry(this IndustryPost industryPost)
        {
            return (IndustryPost) ((int) industryPost & FULLINDUSTRYMASK);
        }


        /// <summary>
        /// 判断是否是具体行业
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static bool IsPost(this IndustryPost industryPost)
        {
            return ((int) industryPost & POSTMASK) != 0 && ((int) industryPost & ~FULLPOSTMASK) == 0;
        }

        /// <summary>
        /// 得到具体行业,不判断是否合法
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static IndustryPost Post(this IndustryPost industryPost)
        {
            return (IndustryPost) ((int) industryPost & FULLPOSTMASK);
        }

        /// <summary>
        /// 得到所有行业分类
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IndustryPost> GetAllIndustryCategories()
        {
            return GetAllIndustryPosts().Where(d => d.IsIndustryCategory());

        }



        /// <summary>
        /// 得到分类下所有行业列表
        /// </summary>
        /// <param name="industryCategory"></param>
        /// <returns></returns>
        public static IEnumerable<IndustryPost> Industries(this IndustryPost industryCategory)
        {
            return GetAllIndustryPosts().Where(r => r.IsIndustry() && r.BelongTo(industryCategory));
        }

        /// <summary>
        /// 得到行业下所有职位列表
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public static IEnumerable<IndustryPost> Posts(this IndustryPost industry)
        {
            return GetAllIndustryPosts().Where(r => r.IsPost() && r.BelongTo(industry));
        }

        /// <summary>
        /// 得到所有职位
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IndustryPost> GetAllPosts()
        {
            return GetAllIndustryPosts().Where(d => d.IsPost());

        }
        /// <summary>
        /// 将一批职位转换为连续的字符串拼接
        /// </summary>
        /// <param name="industries"></param>
        /// <returns></returns>
        public static string ToSlashString(this IEnumerable<IndustryPost> industries)
        {
            var sb = new StringBuilder();
            foreach (var post in industries)
            {
                sb.Append(post.ShortName());
                sb.Append('/');
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}