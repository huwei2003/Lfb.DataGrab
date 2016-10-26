using System;
using System.Configuration;
using System.Text.RegularExpressions;
using Lib.Csharp.Tools;

namespace Lfb.DataGrab
{
    /// <summary>
    /// 全局公用类
    /// </summary>
    public class Global
    {
        
        /// <summary>
        /// 图片地址前缀部分 图片部分暂由移动端自己拼接
        /// </summary>
        public static string ImgSavePrex = GetImgSavePrex();

        public static string ImgSaveSuffix = GetImgSaveSuffix();
        /// <summary>
        /// 是否开启抓取 1:抓取 0:no
        /// </summary>
        public static int IsEnableGather = GetIsEnableGather();

        /// <summary>
        /// 是否开启图片转存 1：处理 0:No
        /// </summary>
        public static int IsEnableImgDeal = GetIsEnableImgDeal();
        

        private static string GetImgSavePrex()
        {
            try
            {
                return ConfigurationManager.AppSettings["ImgSavePrex"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static string GetImgSaveSuffix()
        {
            try
            {
                return ConfigurationManager.AppSettings["ImgSaveSuffix"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static int GetIsEnableGather()
        {
            try
            {
                return StrHelper.ToInt32(ConfigurationManager.AppSettings["IsEnableGather"].ToString());
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return 0;
            }
        }

        private static int GetIsEnableImgDeal()
        {
            try
            {
                return  StrHelper.ToInt32(ConfigurationManager.AppSettings["IsEnableImgDeal"].ToString());
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return 0;
            }
        }

        

        /// <summary>
        /// 处理内容中的图片地址，如果是相对地址的则要加上地址前缀，构成完整的url
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="prexUrl"></param>
        /// <returns></returns>
        public static string DealImgUrlPrex(string contents,string prexUrl)
        {
            var str = contents;
            try
            {
                //因为内容中的图片链接是相对地址，要改成绝对地址
                var imgList = XpathHelper.GetAttrValueListByXPath(str, "//img", "src");
                if (imgList != null && imgList.Count > 0)
                {
                    foreach (var img in imgList)
                    {
                        try
                        {
                            if (!img.Contains("http"))
                            {
                                var newUrl = prexUrl + img.Replace("./", "/"); ;
                                str = str.Replace(img, newUrl);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return str;
        }

        /// <summary>
        /// 从内容中移除特殊符号
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static string RemoveSymbol(string contents)
        {
            var str = contents;
            str = str.Replace("&amp;ldquo;", "").Replace("&amp;rdquo;", "").Replace("&amp;middot;", "").Replace("&amp;lsquo;", "").Replace("&amp;rsquo;", "").Replace("&amp;mdash;", "").Replace("&amp;mdash;", "").Replace("&amp;hellip;", "").Replace("&amp;hellip;", "");

            str = str.Replace("&ldquo;", "").Replace("&rdquo;", "").Replace("&middot;", "").Replace("&lsquo;", "").Replace("&rsquo;", "").Replace("&mdash;", "").Replace("&mdash;", "").Replace("&hellip;", "").Replace("&hellip;", "");
            return str;
        }

        /// <summary>
        /// 判断是否是今日头条作者首页的url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsToutiaoAuthorUrl(string url)
        {
            //"http://toutiao.com/m3470331046/"
            return Regex.IsMatch(url, @"(https?|http)://toutiao.com/m\d{3,30}");
        }

        public static string GetToutiaoAuthorId(string url)
        {
            return url.ToLower().Replace("https://", "").Replace("http://", "").Replace("toutiao.com","").Replace("www","").Replace("/m","").Replace("/","");
            //"http://toutiao.com/m3470331046/"
        }
    }
}
