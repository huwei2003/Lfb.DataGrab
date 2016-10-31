using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lfb.DataGrab
{
    public class HttpHelper
    {
        /// <summary>
        /// 是否用代理
        /// </summary>
        public static bool IsUseProxy = true;
        public static List<string> GetProxyList()
        {
            //var ProxyList = (List<string>)Lib.Csharp.Tools.AppCache.Get("ProxyIpListForHttp");
            try
            {
                var ProxyList = Comm.Tools.Utility.Cache.GetCache<List<string>>("ProxyIpListForHttp");

                if (ProxyList == null || ProxyList.Count == 0)
                {
                    ProxyList = new List<string>();
                    string strContent = GetContent(Global.GetProxyIpUrl, Encoding.UTF8);
                    if (!string.IsNullOrWhiteSpace(strContent))
                    {
                        var strArr = strContent.Replace("\r\n", ";").Split(';');
                        if (strArr != null && strArr.Length > 0)
                        {
                            foreach (var item in strArr)
                            {
                                if (!string.IsNullOrWhiteSpace(item))
                                {
                                    if (!ProxyList.Contains(item))
                                    {
                                        ProxyList.Add(item);
                                    }      
                                }
                            }
                        }
                        Comm.Tools.Utility.Cache.SetCache("ProxyIpListForHttp", ProxyList, 3600);
                        //Lib.Csharp.Tools.AppCache.AddCache("ProxyIpListForHttp", ProxyList,1);
                    }
                }
                return ProxyList;
            }
            catch
            {
            }
            return null;
        }
        
         

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

                    if (IsUseProxy)
                    {
                        //set proxy
                        var ipList = GetProxyList();
                        if (ipList != null && ipList.Count > 0)
                        {
                            Random rnd = new Random();
                            var i = rnd.Next(0, ipList.Count);
                            var ipItem = ipList[i];
                            var ip = ipItem.Split(':')[0];
                            var port = Lib.Csharp.Tools.StrHelper.ToInt32(ipItem.Split(':')[1]);
                            System.Net.WebProxy proxy = new WebProxy(ip, port);
                            request.Proxy = proxy;
                        }
                        //set end
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
    }
}
