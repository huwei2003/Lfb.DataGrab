using System;
using System.Configuration;
using System.Text.RegularExpressions;
using Lib.Csharp.Tools;

namespace Lfb.DataGrabBll
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
        /// 是否开启图片转存 1：处理 0:No
        /// </summary>
        public static int IsEnableImgDeal = GetIsEnableImgDeal();

        /// <summary>
        /// 页面抓取深度，即翻多少页
        /// </summary>
        public static int PageDepth = GetPageDepth();

        /// <summary>
        /// 获取代理ip请求的url
        /// </summary>
        public static string GetProxyIpUrl = GetProxyUrl();

        /// <summary>
        /// Mysql1配置
        /// </summary>
        public static string MySql1 = GetMySql1();

        /// <summary>
        /// 是否开启抓取频道页新闻 1=抓取 0=no
        /// </summary>
        public static string IsEnableGatherChannel = GetIsEnableGatherChannel();

        /// <summary>
        /// 是否开启抓取作者列表页新闻 1=处理 0=no
        /// </summary>
        public static string IsEnableGatherAuthor = GetIsEnableGatherAuthor();

        /// <summary>
        /// 是否开启从新闻页抓取作者 1=处理 0=no
        /// </summary>
        public static string IsEnableGatherAuthorFromNews = GetIsEnableGatherAuthorFromNews();

        /// <summary>
        /// 是否开启新闻定时刷新 1=处理 0=no
        /// </summary>
        public static string IsEnableRefreshNews = GetIsEnableRefreshNews();

        /// <summary>
        /// 是否开启定时刷新代理列表 1=处理 0=no
        /// </summary>
        public static string IsEnableRefreshProxy = GetIsEnableRefreshProxy();
    
        /// <summary>
        /// 代理池的大小
        /// </summary>
        public static int ProxyPoolSize = GetProxyPoolSize();

        /// <summary>
        /// 是否开启从作者id取相关新闻 1=处理 0=no
        /// </summary>
        public static string IsEnableGatherRelationFromAuthor = GetIsEnableGatherRelationFromAuthor();

        /// <summary>
        /// 是否开启从组图抓取新闻作者信息 1=处理 0=no
        /// </summary>
        public static string IsEnableGatherZt = GetIsEnableGatherZt();

        /// <summary>
        /// 是否开启从用户订阅抓取作者信息 1=处理 0=no
        /// </summary>
        public static string IsEnableGatherUserSub = GetIsEnableGatherUserSub();

        /// <summary>
        /// 是否开启抓取头条用户信息 1=处理 0=no
        /// </summary>
        public static string IsEnableGatherUserInfo = GetIsEnableGatherUserInfo();

        /// <summary>
        /// 百家号搜索关键字列表
        /// </summary>
        public static string BjhSearchKeywords = GetBjhSearchKeywords();

        /// <summary>
        /// 是否开启百家号搜索 1=处理 0=no
        /// </summary>
        public static string IsEnableBjhSearch = GetIsEnableBjhSearch();

        /// <summary>
        /// 是否开启百家号作者主页抓取 1=处理 0=no
        /// </summary>
        public static string IsEnableBjhAuthorGather = GetIsEnableBjhAuthorGather();


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
        private static int GetPageDepth()
        {
            try
            {
                return StrHelper.ToInt32(ConfigurationManager.AppSettings["PageDepth"].ToString());
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return 10;
            }
        }
        private static string GetProxyUrl()
        {
            try
            {
                return ConfigurationManager.AppSettings["GetProxyUrl"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static string GetMySql1()
        {
            try
            {
                return ConfigurationManager.AppSettings["MySql1"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }
        private static string GetIsEnableGatherChannel()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableGatherChannel"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static string GetIsEnableGatherAuthor()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableGatherAuthor"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static string GetIsEnableGatherAuthorFromNews()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableGatherAuthorFromNews"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }
        private static string GetIsEnableRefreshNews()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableRefreshNews"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }
        private static string GetIsEnableRefreshProxy()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableRefreshProxy"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static string GetIsEnableGatherRelationFromAuthor()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableGatherRelationFromAuthor"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static string GetIsEnableGatherZt()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableGatherZt"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }
        
        private static string GetIsEnableGatherUserSub()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableGatherUserSub"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static string GetBjhSearchKeywords()
        {
            try
            {
                return ConfigurationManager.AppSettings["BjhSearchKeywords"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "";
            }
        }

        private static string GetIsEnableBjhSearch()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableBjhSearch"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "0";
            }
        }

        private static string GetIsEnableBjhAuthorGather()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableBjhAuthorGather"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "0";
            }
        }
        
        private static string GetIsEnableGatherUserInfo()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableGatherUserInfo"].ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "0";
            }
        }
        

        private static int GetProxyPoolSize()
        {
            try
            {
                return StrHelper.ToInt32( ConfigurationManager.AppSettings["ProxyPoolSize"].ToString());
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return 10;
            }
        }

        /// <summary>
        /// 是否开启特殊百家号作者新闻抓取 1=处理 0=no
        /// </summary>
        public static string IsEnableBjhNewsGatherForClient = GetIsEnableBjhNewsGatherForClient();
        private static string GetIsEnableBjhNewsGatherForClient()
        {
            try
            {
                return ConfigurationManager.AppSettings["IsEnableBjhNewsGatherForClient"];
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return "0";
            }
        }
        
        /// <summary>
        /// 文章每天任务分配数
        /// </summary>
        public static int NewsTaskNums = GetNewsTaskNums();
        private static int GetNewsTaskNums()
        {
            try
            {
                return Convert.ToInt32( ConfigurationManager.AppSettings["NewsTaskNums"].ToString());
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return 50;
            }
        }

        /// <summary>
        /// 特殊百家号作者新闻刷新时间间隔
        /// </summary>
        public static int NewsRefreshInteval = GetNewsRefreshInteval();
        private static int GetNewsRefreshInteval()
        {
            try
            {
                return Convert.ToInt32( ConfigurationManager.AppSettings["NewsRefreshInteval"].ToString());
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return 15;
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
            if (string.IsNullOrWhiteSpace(url))
                return false;
            //"http://toutiao.com/m3470331046/"
            return Regex.IsMatch(url, @"(https?|http)://toutiao.com/m\d{3,30}");
        }

        public static string GetToutiaoAuthorId(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "";
            return url.ToLower().Replace("https://", "").Replace("http://", "").Replace("toutiao.com","").Replace("www","").Replace("/m","").Replace("/","").Replace(".","");
            //"http://toutiao.com/m3470331046/"
        }

        public static string GetToutiaoGroupId(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "";
            return url.ToLower().Replace("https://", "").Replace("http://", "").Replace("toutiao.com","").Replace("www","").Replace("/group/","").Replace("/","").Replace(".","");
            //"display_url": "http://toutiao.com/group/6353545386958782977/",
        }

        public static string GetToutiaoItemId(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "";
            return url.ToLower().Replace("https://", "").Replace("http://", "").Replace("toutiao.com", "").Replace("www", "").Replace("/item/", "").Replace("/", "").Replace(".", "");
            //http://www.toutiao.com/item/6347600331160945153/
        }

        public static int ToInt(string str)
        {
            var iValue = 0;
            try
            {
                var isHaveW = str.Contains("万");
                str = StrHelper.GetStrByRegx(str, @"(-?\d+)(\.\d+)?");//"^(-?/d+)(/./d+)?$"　　//浮点数 
                var i = StrHelper.ToDouble(str);
                if (isHaveW)
                {
                    iValue = Convert.ToInt32(i*10000);
                }
                else
                {
                    iValue = StrHelper.ToInt32(i.ToString());
                }
            }
            catch
            {
            }
            return iValue;
        }
    }
}
