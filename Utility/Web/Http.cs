using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Comm.Tools.Utility.Web
{
    public static class Http
    {
        static readonly Log Log = new Log("System");

        private const string UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        private const string ContentType = "application/x-www-form-urlencoded";
        private const string Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        public static CookieContainer CookieContainer = new CookieContainer();

        static Http()
        {
            //HttpWebRequest的缺省连接只有两个
            ServicePointManager.DefaultConnectionLimit = 1000;
            ServicePointManager.SetTcpKeepAlive(true, 30000, 180000);
            //支持https的证书
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
        }
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public class TrustAllCertificatePolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
            {
                return true;
            }
        }

        /// <summary>
        /// POST提交后，获得HTML
        /// </summary>
        /// <returns>HTML码</returns>
        public static string Post(string url, string postData, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null, WebProxy proxy = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            return Post(url, e.GetBytes(postData), cookieContainer, e, userAgent, autoRedirect, timeout, referer, contentType, accept, headers);
        }
        /// <summary>
        /// POST（二进制）提交后，获得HTML
        /// </summary>
        /// <returns>HTML码</returns>
        public static string Post(string url, byte[] postData, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            HttpWebRequest request = null;
            var response = Posts(ref request, url, postData, cookieContainer, userAgent, autoRedirect, timeout, referer, contentType, accept, headers);
            try
            {
                if (response != null)
                {
                    var stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        using (stream)
                        {
                            using (var reader = new StreamReader(stream, e))
                            {
                                var content = reader.ReadToEnd();
                                return content;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("url={0} {1} {1}".Formats(url, ex.Message, ex.StackTrace));
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return string.Empty;
        }

        public static byte[] Posts(string url, string postData, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            return Posts(url, e.GetBytes(postData), cookieContainer, userAgent, autoRedirect, timeout, referer, contentType, accept, headers);
        }
        public static byte[] Posts(string url, byte[] postData, CookieContainer cookieContainer = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            HttpWebRequest request = null;
            var response = Posts(ref request, url, postData, cookieContainer, userAgent, autoRedirect, timeout, referer, contentType, accept, headers);
            try
            {
                if (response != null)
                {
                    return response.GetResponseStream().ToBytes();
                }
            }
            catch (Exception ex)
            {
                Log.Error("url={0} {1} {1}".Formats(url, ex.Message, ex.StackTrace));
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return null;
        }

        public static CookieContainer GetCooKie(string url, string postData, Encoding e = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            HttpWebRequest request = null;
            var response = Posts(ref request, url, e.GetBytes(postData), null, userAgent, autoRedirect, timeout, referer, contentType, accept, headers);
            try
            {
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                return request.CookieContainer;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public static HttpWebResponse Posts(ref HttpWebRequest request, string url, byte[] data, CookieContainer cookieContainer = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null, WebProxy proxy = null)
        {
            Stream outstream = null;
            try
            {
                if (userAgent.IsNullOrEmpty())
                {
                    userAgent = UserAgent;
                }
                if (cookieContainer != null)
                {
                    CookieContainer = cookieContainer;
                }
                if (contentType.IsNullOrEmpty())
                {
                    contentType = ContentType;
                }
                if (accept.IsNullOrEmpty())
                {
                    accept = Accept;
                }

                // 设置参数 
                request = WebRequest.Create(url) as HttpWebRequest;
                if (request != null)
                {
                    request.CookieContainer = CookieContainer;
                    request.UserAgent = userAgent;
                    request.AllowAutoRedirect = autoRedirect;
                    request.Method = "POST";
                    request.Timeout = timeout * 1000;
                    request.ReadWriteTimeout = timeout * 2000;
                    request.ContentType = contentType;
                    request.ContentLength = data.Length;
                    request.Accept = accept;
                    request.Proxy = proxy;
                    request.ServicePoint.Expect100Continue = false;
                    request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    request.Headers["Accept-Encoding"] = "gzip,deflate";
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    if (referer.NotNullOrEmpty())
                    {
                        request.Referer = referer;
                    }
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            request.Headers.Set(header.Key, header.Value);
                        }
                    }

                    outstream = request.GetRequestStream();
                    outstream.Write(data, 0, data.Length);

                    return request.GetResponse() as HttpWebResponse;
                }
            }
            catch (Exception ex)
            {
                Log.Error("{0} {1} {2}".Formats(url, ex.Message, ex.StackTrace));
            }
            finally
            {
                if (outstream != null)
                {
                    outstream.Close();
                }
            }
            return null;
        }

        public static string Upload(string url, byte[] postData, Encoding e = null, CookieContainer cookieContainer = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            HttpWebRequest request = null;
            var response = Posts(ref request, url, postData, cookieContainer);
            try
            {
                if (response != null)
                {
                    var stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        using (stream)
                        {
                            using (var reader = new StreamReader(stream, e))
                            {
                                var content = reader.ReadToEnd();
                                return content;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("url={0} {1} {1}".Formats(url, ex.Message, ex.StackTrace));
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取网页Html代码
        /// </summary>
        public static string Get(string url, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, int timeOutSeconds = 20, bool autoRedirect = true, string accept = Accept, string referer = "", string contentType = "", string xRequestedWith = null, string acceptEncoding = "", WebProxy proxy = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            HttpWebRequest request = null;
            var response = Gets(ref request, url, cookieContainer, e, userAgent, timeOutSeconds, autoRedirect, accept, referer, contentType, xRequestedWith, acceptEncoding, proxy);
            try
            {
                var content = string.Empty;
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    if (response.ContentEncoding.Contains("gzip"))
                    {
                        stream = new GZipStream(stream, CompressionMode.Decompress, false);
                    }
                    var reader = new StreamReader(stream.Copy(), e);
                    content = reader.ReadToEnd();
                    reader.Close();
                }
                return content;
            }
            catch (Exception ex)
            {
                Log.Error("{0} {1} {2}".Formats(url, ex.Message, ex.StackTrace));
                return string.Empty;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public static HttpWebResponse Gets(ref HttpWebRequest request, string url, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, int timeOutSeconds = 20, bool autoRedirect = true, string accept = Accept, string referer = "", string contentType = "", string xRequestedWith = null, string acceptEncoding = "", WebProxy proxy = null)
        {
            try
            {
                if (userAgent.IsNullOrEmpty())
                {
                    userAgent = UserAgent;
                }
                if (cookieContainer != null)
                {
                    CookieContainer = cookieContainer;
                }
                if (accept.IsNullOrEmpty())
                {
                    accept = Accept;
                }

                request = (HttpWebRequest)WebRequest.Create(url);

                request.UserAgent = userAgent;
                request.Timeout = timeOutSeconds * 1000;
                request.ReadWriteTimeout = timeOutSeconds * 2000;
                request.AllowAutoRedirect = autoRedirect;
                request.UseDefaultCredentials = true;
                request.Accept = accept;
                request.Proxy = proxy;
                request.Headers["Accept-Encoding"] = "gzip,deflate";
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.CookieContainer = CookieContainer;

                if (referer.NotNullOrEmpty())
                {
                    request.Referer = referer;
                }

                if (contentType.NotNullOrEmpty())
                {
                    request.ContentType = contentType;
                }

                if (xRequestedWith.NotNullOrEmpty())
                {
                    request.Headers.Set("x-requested-with", xRequestedWith);
                }

                if (acceptEncoding.NotNullOrEmpty())
                {
                    request.Headers.Set("Accept-Encoding", acceptEncoding);
                }

                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Found)
                {
                    return response;
                }

                Log.Error("{0} response.StatusCode:{1}".Formats(url, response.StatusCode));
                response.Close();
            }
            catch (Exception ex)
            {
                Log.Error("{0} {1}".Formats(url, ex.Message));
            }
            return null;
        }

        public static void FileDownload(string filePath)
        {
            if (File.Exists(filePath))
            {
                var info = new FileInfo(filePath);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(info.Name, Encoding.UTF8));
                HttpContext.Current.Response.AppendHeader("Content-Length", info.Length.ToString());
                try
                {
                    HttpContext.Current.Response.TransmitFile(info.FullName);
                    HttpContext.Current.Response.Flush();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
                HttpContext.Current.Response.End();
            }
        }

        public static T GetRequest<T>(string key, T defaultValue = default(T))
        {
            return HttpContext.Current.Request.Get(key, defaultValue);
        }

        public static Image GetCheckCode(string url, CookieContainer cookieContainer = null, string referer = "")
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                request.Timeout = 5000;
                request.ReadWriteTimeout = 5000;
                request.AllowAutoRedirect = true;
                if (cookieContainer != null)
                {
                    CookieContainer = cookieContainer;
                }
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                request.Accept = "image/png, image/svg+xml, image/*;q=0.8, */*;q=0.5";
                request.Proxy = null;
                if (referer.NotNullOrEmpty())
                {
                    request.Referer = referer;
                }

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        return new Bitmap(stream);
                    }
                }
                else
                {
                    Log.Error("{0} response.StatusCode:{1}".Formats(url, response.StatusCode));
                    response.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error("{0} {1}".Formats(url, ex.Message));
            }
            finally
            {

                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return null;
        }

        public static DataTable ExcelToDataTable(string url, string columns, CookieContainer cookieContainer = null, string method = "GET", Encoding e = null, string postData = "", string contentType = null, int timeout = 60)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            HttpWebResponse response;
            HttpWebRequest request = null;
            if (method.ToUpper() == "POST")
            {
                var data = e.GetBytes(postData);
                response = Posts(ref request, url, data, cookieContainer, null, true, timeout, null, contentType);
            }
            else
            {
                response = Gets(ref request, url, cookieContainer, e, null, timeout);
            }

            try
            {
                var stream = response.GetResponseStream().Copy();
                var path = DateTime.Now.Ticks.ToString() + stream.Length + ".xls";
                stream.Position = 0;
                var writer = new FileStream(path, FileMode.Create);
                int c;
                var buffer = new byte[1024 * 100];
                while ((c = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.Write(buffer, 0, c);
                }
                writer.Close();
                writer.Dispose();
                stream.Close();
                stream.Dispose();

                var dt = path.ExcelToDataTable(columns);
                File.Delete(path);

                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public static List<T> ExcelToList<T>(string url, string columns, CookieContainer cookieContainer = null, string method = "GET", Encoding e = null, string postData = "", string contentType = null, int timeout = 60)
        {
            var dt = ExcelToDataTable(url, columns, cookieContainer, method, e, postData, contentType, timeout);
            if (dt == null)
            {
                return null;
            }
            return dt.ToList<T>();
        }

        public static T BinaryRead<T>(string url, int timeout = 20, T defaultValue = default(T))
        {
            HttpWebRequest request = null;
            var response = Gets(ref request, url, null, null, null, timeout);

            try
            {
                using (var stream = response.GetResponseStream().Copy())
                {
                    return new BinaryFormatter().Deserialize(stream).ConvertTo(defaultValue);
                }
            }
            catch
            {
                return defaultValue;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public static void Download(string url, string savePath, int timeout = 20)
        {
            HttpWebRequest request = null;
            var response = Gets(ref request, url, null, null, null, timeout);

            try
            {
                var stream = response.GetResponseStream().Copy();
                stream.Position = 0;
                var writer = new FileStream(savePath, FileMode.Create);
                int c;
                var buffer = new byte[1024 * 100];
                while ((c = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.Write(buffer, 0, c);
                }
                writer.Close();
                writer.Dispose();
                stream.Close();
                stream.Dispose();
            }
            catch
            {
                //
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public static T Download<T>(string url, int timeout = 20, T defaultValue = default(T))
        {
            HttpWebRequest request = null;
            var response = Gets(ref request, url, null, null, null, timeout);

            try
            {
                using (var stream = response.GetResponseStream().Copy())
                {
                    if (typeof(T).Name == "Byte[]")
                    {
                        return stream.ToArray().ConvertTo(defaultValue);
                    }
                    else
                    {
                        var obj = new BinaryFormatter().Deserialize(stream);
                        stream.Close();
                        stream.Dispose();
                        return obj.ConvertTo(defaultValue);
                    }
                }
            }
            catch
            {
                return defaultValue;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public static string GetIp()
        {
            string ip;

            try
            {
                //网宿CDN 提供的 用户源IP
                ip = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
            }
            catch
            {
                ip = "";
            }
            if (string.IsNullOrWhiteSpace(ip))
            {
                try
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                }
                catch
                {

                    ip = "";
                }
            }
            else
            {
                if (ip.Contains("::"))
                {
                    try
                    {
                        ip = HttpContext.Current.Request.UserHostAddress;
                    }
                    catch
                    {

                        ip = "";
                    }
                }
            }

            return ip;
        }
        /**/
        /// <summary>
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址
        /// </summary>
        public static string IPAddress
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式
                        result = null;

                }

                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;

                return result;
            }
        }
        /// <summary>
        /// 查询IP地域
        /// </summary>
        public static string GetIpArea(string ip, int tryTimes = 1)
        {
            if (!ip.IsIp())
            {
                return "";
            }
            for (var i = 0; i < tryTimes; i++)
            {
                try
                {
                    var url = "https://www.baidu.com/s?wd=" + ip;
                    var html = Get(url, timeOutSeconds: 2);
                    var area = html.Substring("IP地址:&nbsp;{0}</span>".Formats(ip), "\t");
                    return area;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
                Thread.Sleep(100);
            }
            return "";
        }

        public static void Redirect301(string url)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", url);
        }

        /// <summary>
        /// 启用gzip压缩
        /// </summary>
        public static void EnableGzip(this HttpResponse response, HttpRequest request)
        {
            var acceptEncoding = request.Headers["Accept-Encoding"];
            if (acceptEncoding.NotNullOrEmpty())
            {
                if (acceptEncoding.ToLower().Contains("gzip") && response.Headers["Content-Encoding"].IsNullOrEmpty())
                {
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                    response.AppendHeader("Content-Encoding", "gzip");
                }
            }
        }

        /// <summary>
        /// 启用gzip压缩
        /// </summary>
        public static bool EnableGzip(this HttpListenerResponse response, HttpListenerRequest request, out string acceptEncoding)
        {
            acceptEncoding = request.Headers["Accept-Encoding"];
            var isGzip = false;
            if (acceptEncoding.NotNullOrEmpty())
            {
                isGzip = acceptEncoding.ToLower().Contains("gzip");
                if (isGzip && response.Headers["Content-Encoding"].IsNullOrEmpty())
                {
                    response.AppendHeader("Content-Encoding", "gzip");
                }
            }
            return isGzip;
        }


        #region == async ===
        /// <summary>
        /// POST提交后，获得HTML
        /// </summary>
        /// <returns>HTML码</returns>
        public static async Task<string> PostAsync(string url, string postData, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null, WebProxy proxy = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            return await PostAsync(url, e.GetBytes(postData), cookieContainer, e, userAgent, autoRedirect, timeout, referer, contentType, accept, headers);
        }
        /// <summary>
        /// POST（二进制）提交后，获得HTML
        /// </summary>
        /// <returns>HTML码</returns>
        public static async Task<string> PostAsync(string url, byte[] postData, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }

            var response = await PostsAsync(url, postData, cookieContainer, userAgent, autoRedirect, timeout, referer, contentType, accept, headers);
            try
            {
                var result = await Task.Run<string>(() =>
                {
                    var content = "";
                    if (response != null)
                    {
                        var stream = response.GetResponseStream();
                        if (stream != null)
                        {
                            using (stream)
                            {
                                using (var reader = new StreamReader(stream, e))
                                {
                                    content = reader.ReadToEnd();
                                    //return content;
                                }
                            }
                        }
                    }

                    return content;
                });
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("url={0} {1} {1}".Formats(url, ex.Message, ex.StackTrace));
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }

            }
            return string.Empty;
        }

        public static async Task<WebResponse> PostsAsync(string url, byte[] data, CookieContainer cookieContainer = null, string userAgent = UserAgent, bool autoRedirect = true, int timeout = 20, string referer = "", string contentType = ContentType, string accept = Accept, IEnumerable<KeyValuePair<string, string>> headers = null, WebProxy proxy = null)
        {
            Stream outstream = null;
            try
            {
                if (userAgent.IsNullOrEmpty())
                {
                    userAgent = UserAgent;
                }
                if (cookieContainer != null)
                {
                    CookieContainer = cookieContainer;
                }
                if (contentType.IsNullOrEmpty())
                {
                    contentType = ContentType;
                }
                if (accept.IsNullOrEmpty())
                {
                    accept = Accept;
                }

                // 设置参数 
                var request = WebRequest.Create(url) as HttpWebRequest;
                if (request != null)
                {
                    request.CookieContainer = CookieContainer;
                    request.UserAgent = userAgent;
                    request.AllowAutoRedirect = autoRedirect;
                    request.Method = "POST";
                    request.Timeout = timeout * 1000;
                    request.ReadWriteTimeout = timeout * 2000;
                    request.ContentType = contentType;
                    request.ContentLength = data.Length;
                    request.Accept = accept;
                    request.Proxy = proxy;
                    request.ServicePoint.Expect100Continue = false;
                    request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    request.Headers["Accept-Encoding"] = "gzip,deflate";
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    if (referer.NotNullOrEmpty())
                    {
                        request.Referer = referer;
                    }
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            request.Headers.Set(header.Key, header.Value);
                        }
                    }

                    outstream = await request.GetRequestStreamAsync();
                    outstream.Write(data, 0, data.Length);

                    //return request.GetResponse() as HttpWebResponse;
                    var resp = await request.GetResponseAsync();
                    //return resp as HttpWebResponse;
                    return resp;
                }
            }
            catch (Exception ex)
            {
                Log.Error("{0} {1} {2}".Formats(url, ex.Message, ex.StackTrace));
            }
            finally
            {
                if (outstream != null)
                {
                    outstream.Close();
                }
            }
            return null;
        }


        /// <summary>
        /// 获取网页Html代码
        /// </summary>
        public static async Task<string> GetAsync(string url, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, int timeOutSeconds = 20, bool autoRedirect = true, string accept = Accept, string referer = "", string contentType = "", string xRequestedWith = null, string acceptEncoding = "", WebProxy proxy = null)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }
            HttpWebRequest request = null;
            var response = await GetsAsync(url, cookieContainer, e, userAgent, timeOutSeconds, autoRedirect, accept, referer, contentType, xRequestedWith, acceptEncoding, proxy);
            try
            {
                var contents = "";
                contents = await Task.Run<string>(() =>
                {
                    var content = string.Empty;
                    var stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        if (response.ContentEncoding.Contains("gzip"))
                        {
                            stream = new GZipStream(stream, CompressionMode.Decompress, false);
                        }
                        var reader = new StreamReader(stream.Copy(), e);
                        content = reader.ReadToEnd();
                        reader.Close();
                    }
                    return content;
                });
                return contents;
            }
            catch (Exception ex)
            {
                Log.Error("{0} {1} {2}".Formats(url, ex.Message, ex.StackTrace));
                return string.Empty;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        public static async Task<HttpWebResponse> GetsAsync(string url, CookieContainer cookieContainer = null, Encoding e = null, string userAgent = UserAgent, int timeOutSeconds = 20, bool autoRedirect = true, string accept = Accept, string referer = "", string contentType = "", string xRequestedWith = null, string acceptEncoding = "", WebProxy proxy = null)
        {
            try
            {
                if (userAgent.IsNullOrEmpty())
                {
                    userAgent = UserAgent;
                }
                if (cookieContainer != null)
                {
                    CookieContainer = cookieContainer;
                }
                if (accept.IsNullOrEmpty())
                {
                    accept = Accept;
                }

                var request = (HttpWebRequest)WebRequest.Create(url);

                request.UserAgent = userAgent;
                request.Timeout = timeOutSeconds * 1000;
                request.ReadWriteTimeout = timeOutSeconds * 2000;
                request.AllowAutoRedirect = autoRedirect;
                request.UseDefaultCredentials = true;
                request.Accept = accept;
                request.Proxy = proxy;
                request.Headers["Accept-Encoding"] = "gzip,deflate";
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.CookieContainer = CookieContainer;

                if (referer.NotNullOrEmpty())
                {
                    request.Referer = referer;
                }

                if (contentType.NotNullOrEmpty())
                {
                    request.ContentType = contentType;
                }

                if (xRequestedWith.NotNullOrEmpty())
                {
                    request.Headers.Set("x-requested-with", xRequestedWith);
                }

                if (acceptEncoding.NotNullOrEmpty())
                {
                    request.Headers.Set("Accept-Encoding", acceptEncoding);
                }

                var response2 = await request.GetResponseAsync();
                var response = (HttpWebResponse)response2;
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Found)
                {
                    return response;
                }

                Log.Error("{0} response.StatusCode:{1}".Formats(url, response.StatusCode));
                response.Close();
            }
            catch (Exception ex)
            {
                Log.Error("{0} {1}".Formats(url, ex.Message));
            }
            return null;
        }
        #endregion
    }
}