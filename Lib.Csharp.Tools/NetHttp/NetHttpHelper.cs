using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools.NetHttp
{
    public static class NetHttpHelper
    {
        public static Task<string> GetStringAsync(this INetHttp netHttp, string requestUri)
        {
            return netHttp.GetStringAsync(requestUri,null);
        }

       public static Task<Stream> GetStreamAsync(this INetHttp netHttp, string requestUri)
        {
            return netHttp.GetStreamAsync(requestUri, null);
        }


       public static Task<HttpResponseMessage> GetAsync(this INetHttp netHttp, string requestUri)
       {
           return netHttp.GetAsync(requestUri, null);
       }

       public static Task<HttpResponseMessage> DeleteAsync(this INetHttp netHttp, string requestUri)
       {
           return netHttp.DeleteAsync(requestUri, null);
       }

       public static Task<HttpResponseMessage> PostAsync(this INetHttp netHttp, string requestUri, HttpContent content)
       {
           return netHttp.PostAsync(requestUri,content, null);
       }

       public static Task<HttpResponseMessage> PutAsync(this INetHttp netHttp, string requestUri, HttpContent content)
       {
           return netHttp.PutAsync(requestUri, content, null);
       }

       public static Task<HttpResponseMessage> PatchAsync(this INetHttp netHttp, string requestUri, HttpContent content)
       {
           return netHttp.PatchAsync(requestUri, content, null);
       }

      

    }
}
