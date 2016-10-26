using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// system.web.caching操作类
    /// 用于web
    /// </summary>
    public class AppCache
    {
        public static Cache MyCache = HttpContext.Current.Cache;
        private AppCache() { }

        public static bool IsExist(string key)
        {
            //MyCache = HttpContext.Current.Cache;
            if (MyCache[key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Add(string key, object obj)
        {
            MyCache.Add(key, obj, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public static void Add(string key, object obj, string file)
        {
            MyCache.Add(key, obj, new CacheDependency(file), Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public static void Remove(string key)
        {
            MyCache.Remove(key);
        }

        public static object Get(string key)
        {
            return MyCache[key];
        }
        /// <summary>
        /// 加时间限制的cache 6 hours
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="obj">值,对象类型</param>
        public static void AddCache(string key, object obj)
        {
            MyCache.Add(key, obj, null, DateTime.Now.AddHours(6), TimeSpan.Zero, CacheItemPriority.High, null);
        }
        /// <summary>
        /// 加时间限制的cache，单位小时
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="hours"></param>
        public static void AddCache(string key, object obj, int hours)
        {
            MyCache.Add(key, obj, null, DateTime.Now.AddHours(hours), TimeSpan.Zero, CacheItemPriority.High, null);
        }
    }
}
