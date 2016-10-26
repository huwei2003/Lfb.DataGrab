using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Lib.Csharp.Tools.NetHttp;
using Newtonsoft.Json;

namespace Comm.Global.Config
{
    /// <summary>
    /// 驱动帮助类
    /// </summary>
    internal static  class HttpHelper
    {
        static readonly INetHttp NetHttp = NetHttpFactory.GetInstanse();
        
        
        private static string GetUrlParam(object urlParam)
        {
            if (urlParam == null)
                return null;
            var t = urlParam.GetType(); //获得该类的Type

            var sb=new StringBuilder();
            //注意需要改进循环所有公共数据成员，并且对特殊内容进行url转义
            foreach (var pi in t.GetProperties())
            {
                var value = pi.GetValue(urlParam, null); //用pi.GetValue获得值
                var name = pi.Name; //获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                if (sb.Length > 0)
                    sb.Append('&');
                sb.Append(name);
                sb.Append('=');
                sb.Append(value);
            }
            if (sb.Length > 0)
                sb.Insert(0, '?');
            return sb.ToString();
        }

        private static async Task<T> ReadContent<T>(HttpResponseMessage response) where T : class
        {

            if (response.IsSuccessStatusCode)
            {
                var s = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(s);
            }

            return null;
        }

        public static string ConnectUrl(string baseUrl, string url, Int32 id = 0)
        {
            var sb = new StringBuilder(baseUrl);
            if (baseUrl.EndsWith("/") && url.StartsWith("/"))
                sb.Length -= 1;
            else if (!baseUrl.EndsWith("/") && !url.StartsWith("/"))
                sb.Append('/');
            sb.Append(url);
            if (url.EndsWith("/"))
                sb.Length -= 1;
            if (id != 0)
            {
                sb.Append('/');
                sb.Append(id);
            }
                
            return sb.ToString();
        }

        public static async Task<T> PostAsync<T>(string url, object bodyParam, object urlParam = null) where T:class 
        {
            var param = GetUrlParam(urlParam);
            if (param != null)
                url += param;

            var s = JsonConvert.SerializeObject(bodyParam);
            HttpContent content = new StringContent(s);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                var response = await NetHttp.PostAsync(url, content);
                return await ReadContent<T>(response);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
      
   
        public static async Task<T> GetAsync<T>(string url, object urlParam=null)where T:class
        {
            var param = GetUrlParam(urlParam);
            if (param!=null)
                url += param;
            try
            {
                var response = await NetHttp.GetAsync(url);
                return await ReadContent<T>(response);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static async Task DeleteAsync(string url, object urlParam=null)
        {
            var param = GetUrlParam(urlParam);
            if (param != null)
                url += param;
            var response = await NetHttp.DeleteAsync(url);
            await ReadContent<object>(response);
        }

        /*public static async Task<T> PatchAsync<T>(string url, object bodyParam, object urlParam=null) where T:class 
        {
            var s = JsonConvert.SerializeObject(bodyParam);
            HttpContent content = new StringContent(s);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
          
            var param = GetUrlParam(urlParam);
            if (param != null)
                url += param;

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) 
            { Content = content };

            try
            {
                var response = await NetHttp.SendAsync(request);
                return await ReadContent<T>(response);
            }
            catch (Exception)
            {
                return null;
            }
            
        }*/
  
    }
}