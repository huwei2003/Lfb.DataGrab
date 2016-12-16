using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lib.Csharp.Tools;
using System.Threading;

namespace Lfb.DataGrabBll
{
    public class HttpHelper
    {
        /// <summary>
        /// 是否用代理
        /// </summary>
        public static bool IsUseProxy = true;

        /// <summary>
        /// 获取指定网页的内容
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <param name="encoder">网页编码格式</param>
        /// <returns>string</returns>
        public static string GetContent(string strUrl, Encoding encoder)
        {
            string strMsg = string.Empty;
            try
            {
                // 这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                //CookieContainer cc = new CookieContainer();
                //WebRequest request = WebRequest.Create(strUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);

                //set request args
                request.Method = "Get";
                //request.CookieContainer = cc;
                request.KeepAlive = true;

                //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentType = "text/html";

                //模拟goole浏览器访问
                request.UserAgent =
                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
                //request.Referer = strUrl;
                //request.Headers.Add("x-requested-with:XMLHttpRequest");
                request.Headers.Add("x-requested-with:com.android.browser");
                
                
                //request.Host = "baidu.com";
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
                //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                                 DecompressionMethods.None;
                //支持跳转页面，查询结果将是跳转后的页面
                ////request.AllowAutoRedirect = true;

                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                if (request.Method == "POST")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                StreamReader reader = new StreamReader(response.GetResponseStream(), encoder);

                strMsg = reader.ReadToEnd();
                // .\0为null，空字符，也是字符串结束标志
                strMsg = strMsg.Replace("\0", "");
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch(Exception ex)
            {
                var str = ex.Message;
            }
            return strMsg;
        }

        public static Stream GetStream(string strUrl, Encoding encoder)
        {

            try
            {
                CookieContainer cc = new CookieContainer();
                //WebRequest request = WebRequest.Create(strUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);

                //set request args
                request.Method = "Get";
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentType = "text/html";

                //模拟goole浏览器访问
                request.UserAgent =
                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
                //request.Referer = strUrl;
                //request.Headers.Add("x-requested-with:XMLHttpRequest");
                request.Headers.Add("x-requested-with:com.android.browser");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
                //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                                 DecompressionMethods.None;
                //支持跳转页面，查询结果将是跳转后的页面
                ////request.AllowAutoRedirect = true;

                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                if (request.Method == "POST")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                var stream = response.GetResponseStream();
                ;
                //response.Close();

                return stream;
            }
            catch
            {
            }
            return null;
        }

        public static async Task<string> GetContentAsync(string strUrl, Encoding encoder)
        {
            var strMsg = await Task.Run(() =>
            {
                try
                {
                    #region

                    CookieContainer cc = new CookieContainer();
                    //WebRequest request = WebRequest.Create(strUrl);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);

                    //set request args
                    request.Method = "Get";
                    request.CookieContainer = cc;
                    request.KeepAlive = true;

                    //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    request.ContentType = "text/html";

                    //模拟goole浏览器访问
                    request.UserAgent =
                        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
                    //request.Referer = strUrl;
                    //request.Headers.Add("x-requested-with:XMLHttpRequest");
                    request.Headers.Add("x-requested-with:com.android.browser");

                    request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
                    //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                                     DecompressionMethods.None;
                    //支持跳转页面，查询结果将是跳转后的页面
                    ////request.AllowAutoRedirect = true;

                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    if (request.Method == "POST")
                    {
                        request.ContentType = "application/x-www-form-urlencoded";
                    }

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                    StreamReader reader = new StreamReader(response.GetResponseStream(), encoder);

                    var strcontent = reader.ReadToEnd();
                    // .\0为null，空字符，也是字符串结束标志
                    strcontent = strcontent.Replace("\0", "");
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                    return strcontent;

                    #endregion
                }
                catch (Exception)
                {
                    return "";
                }
            });

            return strMsg;
        }
        public static async Task<string> GetContentByMobileAgentAsync(string strUrl, Encoding encoder)
        {
            var strMsg = await Task.Run(() =>
            {
                try
                {
                    #region

                    CookieContainer cc = new CookieContainer();
                    //WebRequest request = WebRequest.Create(strUrl);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);

                    //set request args
                    request.Method = "Get";
                    request.CookieContainer = cc;
                    request.KeepAlive = true;

                    //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    request.ContentType = "text/html";

                    //模拟goole浏览器访问
                    request.UserAgent =
                        "CoolPad8750_CMCC_TD/1.0 Linux/3.4.5 Android/4.2.1 Release/06.31.2013 Browser/1.0 Profile/MIDP-1.0 Configuration/CLDC-1.0";
                    //request.Referer = strUrl;
                    //request.Headers.Add("x-requested-with:XMLHttpRequest");
                    request.Headers.Add("x-requested-with:com.android.browser");

                    request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
                    //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                                     DecompressionMethods.None;
                    //支持跳转页面，查询结果将是跳转后的页面
                    ////request.AllowAutoRedirect = true;

                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    if (request.Method == "POST")
                    {
                        request.ContentType = "application/x-www-form-urlencoded";
                    }

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                    StreamReader reader = new StreamReader(response.GetResponseStream(), encoder);

                    var strcontent = reader.ReadToEnd();
                    // .\0为null，空字符，也是字符串结束标志
                    strcontent = strcontent.Replace("\0", "");
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                    return strcontent;

                    #endregion
                }
                catch (Exception)
                {
                    return "";
                }
            });

            return strMsg;
        }

        public static string GetContentByMobileAgentForTestProxy(string strUrl, Encoding encoder, string ipItem)
        {

            try
            {
                #region

                CookieContainer cc = new CookieContainer();
                //WebRequest request = WebRequest.Create(strUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);


                //set request args
                request.Method = "Get";
                request.CookieContainer = cc;
                //request.KeepAlive = true;
                request.Timeout = 30*1000;
                //request.ReadWriteTimeout = 30*1000;
                //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentType = "text/html";

                //模拟goole浏览器访问
                request.UserAgent =
                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
                //request.Referer = strUrl;
                //request.Headers.Add("x-requested-with:XMLHttpRequest");
                request.Headers.Add("x-requested-with:com.android.browser");

                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
                //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                                 DecompressionMethods.None;
                //支持跳转页面，查询结果将是跳转后的页面
                //request.AllowAutoRedirect = true;

                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                if (request.Method == "POST")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }


                var ip = ipItem.Split(':')[0];
                var port = StrHelper.ToInt32(ipItem.Split(':')[1]);
                WebProxy proxy = new WebProxy(ip, port);
                request.Proxy = proxy;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();

                //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                StreamReader reader = new StreamReader(stream, encoder);
                
                var strcontent = reader.ReadToEnd();
                // .\0为null，空字符，也是字符串结束标志
                strcontent = strcontent.Replace("\0", "");
                reader.Close();
                reader.Dispose();
                response.Close();
                return strcontent;

                #endregion
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string GetContentByMobileAgent(string strUrl, Encoding encoder)
        {

            try
            {
                #region

                CookieContainer cc = new CookieContainer();
                //WebRequest request = WebRequest.Create(strUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);


                //set request args
                request.Method = "Get";
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentType = "text/html";

                //模拟goole浏览器访问
                request.UserAgent =
                    "CoolPad8750_CMCC_TD/1.0 Linux/3.4.5 Android/4.2.1 Release/06.31.2013 Browser/1.0 Profile/MIDP-1.0 Configuration/CLDC-1.0";
                //request.Referer = strUrl;
                //request.Headers.Add("x-requested-with:XMLHttpRequest");
                request.Headers.Add("x-requested-with:com.android.browser");

                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
                //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                                 DecompressionMethods.None;
                //支持跳转页面，查询结果将是跳转后的页面
                ////request.AllowAutoRedirect = true;

                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                if (request.Method == "POST")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
                var ipItem = "";
                if (IsUseProxy)
                {
                    //set proxy
                    //var ipList = ProxyDeal.ProxyList;
                    //if (ipList != null && ipList.Count > 0)
                    //{
                    //    Random rnd = new Random();
                    //    var i = rnd.Next(0, ipList.Count);
                    //    var ipItem = ipList[i];
                    //    var ip = ipItem.Split(':')[0];
                    //    var port = StrHelper.ToInt32(ipItem.Split(':')[1]);
                    //    WebProxy proxy = new WebProxy(ip, port);
                    //    request.Proxy = proxy;
                    //}
                    //set end
                    try
                    {
                        try
                        {
                            if (ProxyDeal.CurrProxyListIndex < 0 ||
                                ProxyDeal.CurrProxyListIndex > ProxyDeal.ProxyList.Count)
                            {
                                ProxyDeal.CurrProxyListIndex = 0;
                            }
                            ipItem = ProxyDeal.ProxyList[ProxyDeal.CurrProxyListIndex];

                            Interlocked.Increment(ref ProxyDeal.CurrProxyListIndex);
                        }
                        catch
                        {
                            Random rnd = new Random();
                            var i = rnd.Next(0, ProxyDeal.ProxyList.Count);
                            ipItem = ProxyDeal.ProxyList[i];
                        }
                        var ip = ipItem.Split(':')[0];
                        var port = StrHelper.ToInt32(ipItem.Split(':')[1]);
                        WebProxy proxy = new WebProxy(ip, port);
                        request.Proxy = proxy;
                    }
                    catch
                    {
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                StreamReader reader = new StreamReader(response.GetResponseStream(), encoder);

                var strcontent = reader.ReadToEnd();
                // .\0为null，空字符，也是字符串结束标志
                strcontent = strcontent.Replace("\0", "");
                if (string.IsNullOrWhiteSpace(strcontent))
                {
                    lock ((ProxyDeal.lockObj))
                    {
                        ProxyDeal.ProxyList.Remove(ipItem);
                        ProxyDeal.ProxyListRemove.Add(ipItem);
                    }
                }

                reader.Close();
                reader.Dispose();
                response.Close();
                return strcontent;

                #endregion
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetContentByAgent(string strUrl, Encoding encoder)
        {

            try
            {
                #region
                // 这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                //CookieContainer cc = new CookieContainer();
                //WebRequest request = WebRequest.Create(strUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);


                //set request args
                request.Method = "Get";
                //request.CookieContainer = cc;
                request.KeepAlive = true;

                //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentType = "text/html";

                //模拟goole浏览器访问
                request.UserAgent =
                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";

                //request.Referer = strUrl;
                //request.Headers.Add("x-requested-with:XMLHttpRequest");
                //request.Headers.Add("x-requested-with:com.android.browser");

                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
                //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                                 DecompressionMethods.None;
                //支持跳转页面，查询结果将是跳转后的页面
                ////request.AllowAutoRedirect = true;

                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                if (request.Method == "POST")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
                var ipItem = "";
                if (IsUseProxy)
                {
                    //set proxy
                    //var ipList = ProxyDeal.ProxyList;
                    //if (ipList != null && ipList.Count > 0)
                    //{
                    //    Random rnd = new Random();
                    //    var i = rnd.Next(0, ipList.Count);
                    //    var ipItem = ipList[i];
                    //    var ip = ipItem.Split(':')[0];
                    //    var port = StrHelper.ToInt32(ipItem.Split(':')[1]);
                    //    WebProxy proxy = new WebProxy(ip, port);
                    //    request.Proxy = proxy;
                    //}
                    //set end
                    try
                    {
                        try
                        {
                            if (ProxyDeal.CurrProxyListIndex < 0 ||
                                ProxyDeal.CurrProxyListIndex > ProxyDeal.ProxyList.Count)
                            {
                                ProxyDeal.CurrProxyListIndex = 0;
                            }
                            ipItem = ProxyDeal.ProxyList[ProxyDeal.CurrProxyListIndex];

                            Interlocked.Increment(ref ProxyDeal.CurrProxyListIndex);
                        }
                        catch
                        {
                            Random rnd = new Random();
                            var i = rnd.Next(0, ProxyDeal.ProxyList.Count);
                            ipItem = ProxyDeal.ProxyList[i];
                        }
                        var ip = ipItem.Split(':')[0];
                        var port = StrHelper.ToInt32(ipItem.Split(':')[1]);
                        WebProxy proxy = new WebProxy(ip, port);
                        request.Proxy = proxy;
                    }
                    catch
                    {
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                StreamReader reader = new StreamReader(response.GetResponseStream(), encoder);

                var strcontent = reader.ReadToEnd();
                // .\0为null，空字符，也是字符串结束标志
                strcontent = strcontent.Replace("\0", "");
                if (string.IsNullOrWhiteSpace(strcontent))
                {
                    lock ((ProxyDeal.lockObj))
                    {
                        ProxyDeal.ProxyList.Remove(ipItem);
                        ProxyDeal.ProxyListRemove.Add(ipItem);
                    }
                }

                reader.Close();
                reader.Dispose();
                response.Close();
                return strcontent;

                #endregion
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetContent(string strUrl, Encoding encoder, CookieContainer cc)
        {
            string strMsg = string.Empty;
            try
            {
                //CookieContainer cc = new CookieContainer();
                //WebRequest request = WebRequest.Create(strUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);

                //set request args
                request.Method = "Get";
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentType = "text/html";

                //模拟goole浏览器访问
                request.UserAgent =
                    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
                //request.Referer = strUrl;
                request.Headers.Add("x-requested-with:XMLHttpRequest");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
                //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                                 DecompressionMethods.None;
                //支持跳转页面，查询结果将是跳转后的页面
                ////request.AllowAutoRedirect = true;

                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                if (request.Method == "POST")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                StreamReader reader = new StreamReader(response.GetResponseStream(), encoder);

                strMsg = reader.ReadToEnd();
                // .\0为null，空字符，也是字符串结束标志
                strMsg = strMsg.Replace("\0", "");
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch
            {
            }
            return strMsg;
        }
        /// <summary>
        /// 获取指定网页的内容
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <param name="encoder">网页编码格式,不指定null时将自动获取网页编码格式</param>
        /// <returns>string</returns>
        public static string GetContent2(string strUrl, Encoding encoder)
        {
            string strMsg = string.Empty;
            CookieContainer cc = new CookieContainer();
            //WebRequest request = WebRequest.Create(strUrl);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);

            //set request args
            request.Method = "Get";
            request.CookieContainer = cc;
            request.KeepAlive = true;
            //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.ContentType = "text/html";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
            request.UserAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
            request.Referer = strUrl;
            request.Headers.Add("x-requested-with:XMLHttpRequest");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en;q=0.6,nl;q=0.4,zh-TW;q=0.2");
            //request.ContentLength = postdataByte.Length;  text/html; charset=utf-8
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip |
                                             DecompressionMethods.None;
            //支持跳转页面，查询结果将是跳转后的页面
            request.AllowAutoRedirect = true;

            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            if (request.Method == "POST")
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            #region 获取数据

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //从这里开始我们要无视编码了
                if (encoder == null)
                {
                    MemoryStream _stream = new MemoryStream();
                    response.GetResponseStream().CopyTo(_stream, 10240);
                    byte[] RawResponse = _stream.ToArray();
                    string temp = Encoding.Default.GetString(RawResponse, 0, RawResponse.Length);
                    //<meta(.*?)charset([\s]?)=[^>](.*?)>
                    Match meta = Regex.Match(temp, "<meta([^<]*)charset=([^<]*)[\"']",
                        RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    string charter = (meta.Groups.Count > 2) ? meta.Groups[2].Value : string.Empty;
                    charter = charter.Replace("\"", string.Empty).Replace("'", string.Empty).Replace(";", string.Empty);
                    if (charter.Length > 0)
                    {
                        encoder = Encoding.GetEncoding(charter);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(response.CharacterSet))
                        {
                            encoder = Encoding.UTF8;
                        }
                        else
                        {
                            encoder = Encoding.GetEncoding(response.CharacterSet);
                        }
                    }
                    strMsg = encoder.GetString(RawResponse);
                }
                else
                {
                    //开始读取流并设置编码方式
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoder))
                    {
                        strMsg = reader.ReadToEnd();
                    }
                }
            }

            #endregion

            return strMsg.Replace("\0", "");
        }

        public static string PostLogin(string postData, string requestUrlString, ref CookieContainer cookie)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            //向服务端请求
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(requestUrlString);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            myRequest.CookieContainer = new CookieContainer();
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            //将请求的结果发送给客户端(界面、应用)
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            cookie.Add(myResponse.Cookies);
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }

        //回调验证证书问题
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            
            
            // 总是接受    
            return true;
        }
    }
}
