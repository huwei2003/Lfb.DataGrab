using System.Web;

namespace Comm.Tools.Utility.Web
{
    public class Application
    {
        public static void Remove(string key)
        {
            HttpContext.Current.Application.Remove(key);
        }

        public static T GetApplication<T>(string key, T defaultValue = default(T))
        {
            try
            {
                return HttpContext.Current.Application[key].ConvertTo(defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static void SetApplication(string key, object value)
        {
            try
            {
                if (value == null)
                {
                    HttpContext.Current.Application.Remove(key);
                }
                else
                {
                    HttpContext.Current.Application[key] = value;
                }
            }
            catch
            {
            }
        }
    }
}
