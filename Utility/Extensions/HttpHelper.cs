using System.Web;

namespace Comm.Tools.Utility
{
    public static class HttpHelper
    {
        public static T Get<T>(this HttpRequest request, string key, T defaultValue = default(T))
        {
            var str = request[key];
            if (str == null)
            {
                str = "";
            }
            str = HttpUtility.UrlDecode(str).Trim().SqlFilter();
            return str.ConvertTo(defaultValue);
        }
    }
}
