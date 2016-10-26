using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// HttpWebRequest请求
    /// </summary>
    public class HttpWebRequestHelper
    {
        /// <summary>
        /// 获取指定pc网页的内容
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <param name="encoder">网页编码格式</param>
        /// <returns>string</returns>
        public static string GetStrFromPcweb(string strUrl, Encoding encoder)
        {
            string strMsg = string.Empty;
            try
            {
                var cc = new CookieContainer();
                //WebRequest request = WebRequest.Create(strUrl);
                var request = (HttpWebRequest)WebRequest.Create(strUrl);

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

                var response = (HttpWebResponse)request.GetResponse();

                var rs = response.GetResponseStream();
                if ( rs!= null)
                {
                    //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                    var reader = new StreamReader(rs, encoder);
                    strMsg = reader.ReadToEnd();
                    // .\0为null，空字符，也是字符串结束标志
                    strMsg = strMsg.Replace("\0", "");
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                }
            }
            catch(Exception ex)
            {
                //
            }
            return strMsg;
        }

        /// <summary>
        /// 获取指定pc网页的内容
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <param name="encoder">网页编码格式</param>
        /// <returns></returns>
        public static async Task<string> GetStrFromPcwebAsync(string strUrl, Encoding encoder)
        {
            var strMsg = await Task.Run(() =>
            {
                try
                {
                    #region

                    var cc = new CookieContainer();
                    //WebRequest request = WebRequest.Create(strUrl);
                    var request = (HttpWebRequest)WebRequest.Create(strUrl);

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

                    var response = (HttpWebResponse)request.GetResponse();
                    var rs = response.GetResponseStream();
                    if (rs != null)
                    {
                        //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                        StreamReader reader = new StreamReader(rs, encoder);

                        var strcontent = reader.ReadToEnd();
                        // .\0为null，空字符，也是字符串结束标志
                        strcontent = strcontent.Replace("\0", "");
                        reader.Close();
                        reader.Dispose();
                        response.Close();
                        return strcontent;
                    }
                    return "";

                    #endregion
                }
                catch (Exception)
                {
                    return "";
                }
            });
            return strMsg;
        }
        /// <summary>
        /// 获取指定手机网页的内容
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <param name="encoder">网页编码格式</param>
        /// <returns></returns>
        public static async Task<string> GetStrFromMobilewebAsync(string strUrl, Encoding encoder)
        {
            var strMsg = await Task.Run(() =>
            {
                try
                {
                    #region

                    var cc = new CookieContainer();
                    //WebRequest request = WebRequest.Create(strUrl);
                    var request = (HttpWebRequest)WebRequest.Create(strUrl);

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

                    var response = (HttpWebResponse)request.GetResponse();
                    var rs = response.GetResponseStream();
                    if (rs != null)
                    {
                        //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                        var reader = new StreamReader(rs, encoder);

                        var strcontent = reader.ReadToEnd();
                        // .\0为null，空字符，也是字符串结束标志
                        strcontent = strcontent.Replace("\0", "");
                        reader.Close();
                        reader.Dispose();
                        response.Close();
                        return strcontent;
                    }
                    return "";

                    #endregion
                }
                catch (Exception)
                {
                    return "";
                }
            });

            return strMsg;
        }
        /// <summary>
        /// 获取指定网页的内容
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <param name="encoder">网页编码格式</param>
        /// <param name="cc">cookie container</param>
        /// <returns></returns>
        public static string GetStrPcwebByCookie(string strUrl, Encoding encoder, CookieContainer cc)
        {
            string strMsg = string.Empty;
            try
            {
                //CookieContainer cc = new CookieContainer();
                //WebRequest request = WebRequest.Create(strUrl);
                var request = (HttpWebRequest)WebRequest.Create(strUrl);

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

                var response = (HttpWebResponse)request.GetResponse();
                var rs = response.GetResponseStream();
                if (rs != null)
                {
                    //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                    var reader = new StreamReader(rs, encoder);

                    strMsg = reader.ReadToEnd();
                    // .\0为null，空字符，也是字符串结束标志
                    strMsg = strMsg.Replace("\0", "");
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                }
            }
            catch(Exception ex)
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
        public static string GetStrFromPcwebNoEncoding(string strUrl, Encoding encoder)
        {
            var strMsg = string.Empty;
            var cc = new CookieContainer();
            //WebRequest request = WebRequest.Create(strUrl);
            var request = (HttpWebRequest)WebRequest.Create(strUrl);

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

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var rs = response.GetResponseStream();
                if (rs != null)
                {
                    //从这里开始我们要无视编码了
                    if (encoder == null)
                    {
                        var stream = new MemoryStream();


                        rs.CopyTo(stream, 10240);
                        byte[] rawResponse = stream.ToArray();
                        string temp = Encoding.Default.GetString(rawResponse, 0, rawResponse.Length);
                        //<meta(.*?)charset([\s]?)=[^>](.*?)>
                        Match meta = Regex.Match(temp, "<meta([^<]*)charset=([^<]*)[\"']",
                            RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string charter = (meta.Groups.Count > 2) ? meta.Groups[2].Value : string.Empty;
                        charter = charter.Replace("\"", string.Empty)
                            .Replace("'", string.Empty)
                            .Replace(";", string.Empty);
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
                        strMsg = encoder.GetString(rawResponse);

                    }
                    else
                    {
                        //开始读取流并设置编码方式
                        using (var reader = new StreamReader(rs, encoder))
                        {
                            strMsg = reader.ReadToEnd();
                        }
                    }
                }
            }

            #endregion

            return strMsg.Replace("\0", "");
        }

        public static string PostLogin(string postData, string requestUrlString, ref CookieContainer cookie)
        {
            var encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            //向服务端请求
            var myRequest = (HttpWebRequest)WebRequest.Create(requestUrlString);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            myRequest.CookieContainer = new CookieContainer();
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            //将请求的结果发送给客户端(界面、应用)
            var myResponse = (HttpWebResponse)myRequest.GetResponse();
            cookie.Add(myResponse.Cookies);
            var rs = myResponse.GetResponseStream();
            if (rs != null)
            {
                var reader = new StreamReader(rs, Encoding.UTF8);
                return reader.ReadToEnd();
            }
            else
            {
                return "";
            }
        }
    }
}
