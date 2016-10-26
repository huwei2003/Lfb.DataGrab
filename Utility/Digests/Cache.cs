using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

namespace Comm.Tools.Utility
{
    [Serializable]
    public class Cache
    {
        private static readonly Log Log = new Log("System");

        static Cache()
        {
            new Thread(ExpiredClear).StartAsync();
        }

        private static readonly ConcurrentDictionary<string, KeyValuePair<DateTime, object>> Caches = new ConcurrentDictionary<string, KeyValuePair<DateTime, object>>();
        private static readonly ConcurrentDictionary<string, int> ReloadFuncLocker = new ConcurrentDictionary<string, int>();

        public static bool RemoveCache(string key)
        {
            return Caches.Remove(key);
        }

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">唯一索引key</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="keepSeconds">最后次访问缓存后多少秒清空缓存</param>
        /// <param name="reloadFunc">重新加载缓存代理函数（触发型异步更新）</param>
        /// <param name="cacheSeconds"></param>
        private static T GetCache<T>(string key, T defaultValue, int keepSeconds, Func<T> reloadFunc, int cacheSeconds)
        {
            if (key == null)
            {
                return defaultValue;
            }
            var result = defaultValue;
            try
            {
                KeyValuePair<DateTime, object> value;
                if (Caches.TryGetValue(key, out value))//有缓存
                {
                    result = value.Value.ConvertTo(defaultValue);
                    //未过期，直接返回
                    if (value.Key >= DateTime.Now)
                    {
                        if (keepSeconds > 0)
                        {
                            Caches[key] = new KeyValuePair<DateTime, object>(DateTime.Now.AddSeconds(keepSeconds), value.Value);
                        }
                        return result;
                    }
                    if (reloadFunc == null)
                    {
                        result = defaultValue;
                    }
                    else
                    {
                        //已过期,直接返回并重新加载
                        new Thread(() =>
                        {
                            try
                            {
                                int locker;
                                ReloadFuncLocker.TryGetValue(key, out locker);
                                if (locker > 0)
                                {
                                    return;
                                }
                                ReloadFuncLocker[key] = 1;
                                SetCache(key, reloadFunc(), cacheSeconds);
                            }
                            catch (Exception e)
                            {
                                Log.Error(e.Message + e.StackTrace);
                            }
                            finally
                            {
                                ReloadFuncLocker[key] = 0;
                            }
                        }).StartAsync();
                        return result;
                    }
                }
                //同步执行返回
                if (reloadFunc != null)
                {
                    result = reloadFunc();
                    SetCache(key, result, cacheSeconds);
                }
            }
            catch
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">唯一索引key</param>
        /// <param name="defaultValue">默认值</param>
        public static T GetCache<T>(string key, T defaultValue = default(T))
        {
            return GetCache(key, defaultValue, 0, null, 0);
        }
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">唯一索引key</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="keepSeconds">最后次访问缓存后多少秒清空缓存</param>
        public static T GetCache<T>(string key, int keepSeconds, T defaultValue = default(T))
        {
            return GetCache(key, defaultValue, keepSeconds, null, 0);
        }
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">唯一索引key</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="reloadFunc">重新加载缓存代理函数（触发型异步更新）</param>
        /// <param name="cacheSeconds">重新加载缓存缓存时间</param>
        public static T GetCache<T>(string key, Func<T> reloadFunc, int cacheSeconds, T defaultValue = default(T))
        {
            return GetCache(key, defaultValue, 0, reloadFunc, cacheSeconds);
        }
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="defaultValue">默认值</param>
        /// <param name="reloadFunc">重新加载缓存代理函数（触发型异步更新）</param>
        /// <param name="cacheSeconds">重新加载缓存缓存时间</param>
        public static T GetCache<T>(Func<T> reloadFunc, int cacheSeconds, T defaultValue = default(T))
        {
            var key = reloadFunc.Method.MetadataToken.ToString();
            var target = reloadFunc.Target;
            if (target != null)
            {
                var type = target.GetType();
                var fields = type.GetFields();
                var result = new Dictionary<string, string>();
                foreach (var field in fields)
                {
                    result[field.Name] = field.GetValue(target).ConvertTo("");
                }
                key += result.ToJsonString();
            }
            return GetCache(key, reloadFunc, cacheSeconds, defaultValue);
        }

        public static bool SetCache(string key, object value, int cacheSeconds = 0)
        {
            if (key == null)
            {
                return false;
            }
            if (cacheSeconds > 0 && value != null)
            {
                Caches[key] = new KeyValuePair<DateTime, object>(DateTime.Now.AddSeconds(cacheSeconds), value);
                return true;
            }
            RemoveCache(key);
            return false;
        }

        public static bool SetCache(string key, object value, DateTime until)
        {
            var cacheSeconds = (until - DateTime.Now).TotalSeconds.ToString("0").ToInt32();
            return SetCache(key, value, cacheSeconds);
        }

        public static T GetApplication<T>(string key, T defaultValue = default(T))
        {
            if (key == null)
            {
                return defaultValue;
            }
            try
            {
                return AppDomain.CurrentDomain.GetData(key).ConvertTo(defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool SetApplication(string key, object value)
        {
            if (key == null)
            {
                return false;
            }
            try
            {
                AppDomain.CurrentDomain.SetData(key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool RemoveApplication(string key)
        {
            return SetApplication(key, null);
        }

        private static void ExpiredClear()
        {
            while (true)
            {
                try
                {
                    //清理过期5分钟以上的缓存，5分钟之内的过期缓存留作异步更新
                    var expiredKeys = Caches.Where(a => a.Value.Key < DateTime.Now.AddMinutes(-5)).Select(a => a.Key).ToArray();
                    if (expiredKeys.Length > 0)
                    {
                        foreach (var key in expiredKeys)
                        {
                            RemoveCache(key);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + e.StackTrace);
                }
                Thread.Sleep(60000);
            }
        }
    }
}