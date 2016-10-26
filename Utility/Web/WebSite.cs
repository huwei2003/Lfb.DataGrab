using System;
using System.Web;

namespace Comm.Tools.Utility.Web
{
    public sealed class WebSite
    {
        /// <summary>
        /// 站点根Url
        /// </summary>
        public static string Url
        {
            get
            {
                var siteUrl = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
                if (!siteUrl.EndsWith("/"))
                {
                    siteUrl += "/";
                }
                return siteUrl;
            }
        }

        /// <summary>
        /// 站点虚拟目录
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                if (HttpContext.Current.Request.ApplicationPath.Equals("/"))
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Request.ApplicationPath;
                }
            }
        }

        /// <summary>
        /// 获取基目录
        /// </summary>
        public static string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string GetUrlWithoutHttp()
        {
            var url = GetUrl();
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                url = url.Substring(7, url.Length - 7);
            }
            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = url.Substring(8, url.Length - 8);
            }
            return url;
        }

        public static string GetUrl()
        {
            var str = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath;
            if (str.EndsWith("/"))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        public static bool IsSearchUrl()
        {
            string Excp = HttpContext.Current.Request.ServerVariables.Get("Http_User_Agent");
            if (Excp.Contains("Baiduspider") || Excp.Contains("Googlebot") || Excp.Contains("Sosospider") || Excp.Contains("Sogou+web+spider"))
            {
                return true;
            }
            return false;
        }
    }
}