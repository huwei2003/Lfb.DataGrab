using System;
using System.Configuration;
using System.Web;

namespace Comm.Tools.Utility.Web
{
    public sealed class Cookie
    {
        static readonly Log Log = new Log("System");
        static readonly string SessionKey = ConfigurationManager.AppSettings["SessionKey"];
        static readonly string CookieDomain = ConfigurationManager.AppSettings["CookieDomain"];

        public static string Get(string name)
        {
            try
            {
                var cookie = HttpContext.Current.Request.Cookies.Get(name);
                if (cookie == null)
                {
                    return string.Empty;
                }
                return cookie.Value;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
            return string.Empty;
        }

        public static string GetSessionId()
        {
            return Get(SessionKey);
        }

        public static void Remove(string name)
        {
            try
            {
                var cookie = new HttpCookie(name)
                {
                    Value = "",
                    Expires = DateTime.Now.AddYears(-100),
                    HttpOnly = false
                };
                HttpContext.Current.Request.Cookies.Add(cookie);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
        }

        public static void Clear()
        {
            try
            {
                HttpContext.Current.Response.Cookies.Clear();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
        }

        public static void Set(string name, string value, bool httpOnly = false)
        {
            try
            {
                var cookie = new HttpCookie(name)
                {
                    Value = value,
                    HttpOnly = httpOnly
                };
                if (CookieDomain.HasValue())
                {
                    cookie.Domain = CookieDomain;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
        }

        public static void Set(string name, string value, int expireDays, bool httpOnly = false)
        {
            try
            {
                var cookie = new HttpCookie(name)
                {
                    Value = value,
                    HttpOnly = httpOnly,
                    Expires = DateTime.Now.AddDays(expireDays)
                };
                if (CookieDomain.HasValue())
                {
                    cookie.Domain = CookieDomain;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
        }

        public static void SetSessionId(string sessionId)
        {
            Set(SessionKey, sessionId);
        }
    }
}